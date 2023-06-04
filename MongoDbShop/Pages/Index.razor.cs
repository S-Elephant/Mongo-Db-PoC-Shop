using Elephant.Database.MongoDb.Abstractions.Repositories;
using Elephant.Types;
using Microsoft.AspNetCore.Components;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using MongoDbShop.Dal.Abstractions.Repositories;
using MongoDbShop.Entities.Shop;
using MongoDbShop.Shared;

namespace MongoDbShop.Pages
{
	/// <summary>
	/// Index page that I abuse for this PoC.
	/// </summary>
	public partial class Index : IDisposable
	{
		[Inject] private IDatabaseRepository DatabaseRepository { get; set; } = null!;

		[Inject] private IProductRepository ProductRepository { get; set; } = null!;

		// Normally, these would be put inside a repository.
		private IMongoClient _mongoDbClient = null!;
		private IMongoDatabase _databaseShop = null!;
		private IMongoCollection<Product> _collectionProduct = null!;

		private ElephantCancellationTokenSource _cts = new();

		#region Fields used for testing

		private string? _databasesAsJson = null;
		private List<Product> _products = new();
		private Product? _productById = null;
		private long _productCount;
		private List<Product> _productsFiltered = new();
		private string _filterPropertyName = "name";
		private string _filterValue = "Orange";
		private string _insertJson = @"[{
  ""name"": ""Copper plates"",
  ""quantity"": 84,
  ""price"": 12.50
},{
  ""name"": ""Coils"",
  ""quantity"": 200,
  ""price"": 0.42,
  ""description"": ""Copper version.""
}]";

		private string _partialDeleteFilterProperty = "name";
		private string _partialDeleteFilterValue = "Coils";
		private Product _replaceProduct = new() { MongoId = string.Empty, };
		private List<string> _lastInsertedIds = new();
		private DeleteResult? _deleteResult = null;
		private string _mongoIdToFind = string.Empty;
		private string _incrementPriceProductId = string.Empty;
		private string? _updateByIdResult = null;

		#endregion

		/// <summary>
		/// On initialize.
		/// </summary>
		protected override void OnInitialized()
		{
			// Normally, these would be put inside a repository.
			_mongoDbClient = new MongoClient(Constants.ConnectionString);
			_databaseShop = _mongoDbClient.GetDatabase(Constants.DatabaseName);
			_collectionProduct = _databaseShop.GetCollection<Product>(Constants.CollectionNameProduct);

			base.OnInitialized();
		}

		/// <summary>
		/// List all databases.
		/// </summary>
		private async Task ListAllDatabases()
		{
			_databasesAsJson = await DatabaseRepository.ListAllDatabasesAsJson(cancellationToken: _cts.Token);
			StateHasChanged();
		}

		/// <summary>
		/// Hide all databases.
		/// </summary>
		private void HideAllDatabases()
		{
			_databasesAsJson = null;
			StateHasChanged();
		}

		/// <summary>
		/// Set <see cref="_productById"/> by Mongo id from database.
		/// </summary>
		private async Task ById(string id)
		{
			_productById = await ProductRepository.ById(id, _cts.Token);
		}

		/// <summary>
		/// Get <see cref="_replaceProduct"/> from database for replacing later.
		/// </summary>
		private async Task GetReplacementProduct(string id)
		{
			_replaceProduct = await ProductRepository.ById(id, cancellationToken: _cts.Token) ?? new Product();
		}

		/// <summary>
		/// Replace <paramref name="productWithUpdatedValues"/> in the database.
		/// </summary>
		private async Task Replace(Product productWithUpdatedValues)
		{
			await ProductRepository.ReplaceOne(productWithUpdatedValues.MongoId, productWithUpdatedValues, cancellationToken: CancellationToken.None);
		}

		/// <summary>
		/// Update the <see cref="Product"/> with id <paramref name="id"/> to increment its <see cref="Product.Price"/> by <paramref name="priceIncrement"/>.
		/// Does nothing if the <see cref="Product"/> wasn't found in the database.
		/// </summary>
		private async Task UpdateById(string id, decimal priceIncrement)
		{
			if (!IsValidMongoId(id))
			{
				_updateByIdResult = "Invalid Mongo id.";
				return;
			}

			Product? productToUpdate = await ProductRepository.ById(id, cancellationToken: _cts.Token);
			if (productToUpdate == null)
			{
				_updateByIdResult = $"{nameof(Product)} with id \"{id}\" not found in database.";
				return;
			}

			UpdateDefinition<Product> updateDefinition = Builders<Product>.Update.Set(product => product.Price, productToUpdate.Price + priceIncrement);
			UpdateResult result = await ProductRepository.UpdateById(id, updateDefinition, null, cancellationToken: _cts.Token);
			_updateByIdResult = $"IsAcknowledged: {result.IsAcknowledged}.";

			StateHasChanged();
		}

		private static bool IsValidMongoId(string? id)
		{
			if (id == null)
				return false;

			if (ObjectId.TryParse(id, out _))
				return true;

			return false;
		}

		/// <summary>
		/// List all <see cref="Product"/>s.
		/// </summary>
		private async Task ListAllProducts()
		{
			_products = await ProductRepository.List(_cts.Token);
			StateHasChanged();
		}

		/// <summary>
		/// Count all  <see cref="Product"/>s in database.
		/// </summary>
		private async Task CountAllProducts()
		{
			_productCount = await ProductRepository.Count(_cts.Token);
			StateHasChanged();
		}

		/// <summary>
		/// Find by property name and value (case sensitive, full matches only).
		/// </summary>
		private async Task FilterProduct(string propertyName, string propertyValue)
		{
			FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(propertyName, propertyValue);
			_productsFiltered = await _collectionProduct.Find(filter).ToListAsync(_cts.Token);
			StateHasChanged();
		}

		/// <summary>
		/// Insert document from JSON.
		/// </summary>
		private async Task InsertByJson(string json)
		{
			List<Product> documentsToInsert = BsonSerializer.Deserialize<IList<Product>>(json).ToList();
			_lastInsertedIds = (await ProductRepository.Insert(documentsToInsert, cancellationToken: _cts.Token)).ToList();
		}

		/// <summary>
		/// Delete documents by partial match (case-insensitive).
		/// </summary>
		private async Task DeleteByPartialMatch(string filterProperty, string filterValue)
		{
			// "(?i)" makes the regex case-insensitive.
			FilterDefinition<Product> filter = Builders<Product>.Filter.Regex(filterProperty, $"/(?i){filterValue}/");
			_deleteResult = await ProductRepository.Delete(filter, _cts.Token);
		}

		private async Task DeleteAllProductsAndReList()
		{
			await ProductRepository.DeleteAll(_cts.Token);
			await ListAllProducts();
		}

		/// <summary>
		/// Insert test <see cref="Product"/>s into the database and calls <see cref="ListAllProducts"/>.
		/// </summary>
		/// <exception cref="Exception">Thrown if less <see cref="Product"/>s were inserted than expected.</exception>
		private async Task SeedTestDataAndReList()
		{
			List<Product> productsToAdd = new()
			{
				new Product("Cocoa Butter", 10, 10.23m, "Theobroma oil"),
				new Product("Tomato", 530, 0.29m, "An edible berry of the plant Solanum lycopersicum."),
				new Product("Pepper", 32, 1.43m, "A popular hot spice"),
				new Product("Garlic", 100, 1.10m, "A species of bulbous flowering plant in the genus Allium."),
				new Product("Orange", 43, 2.49m, "A fruit of various citrus species in the family Rutaceae."),
				new Product("Apple", 200, 1.25m, "Fruit produced by an apple tree (Malus domestica)."),
			};

			IEnumerable<string> result = await ProductRepository.Insert(productsToAdd, _cts.Token);
			if (result.Count() != productsToAdd.Count)
#pragma warning disable S112
				throw new Exception($"Something went wrong trying to add products. Expected {productsToAdd.Count} but got: {result.Count()}.");
#pragma warning restore S112

			await ListAllProducts();
		}

		/// <inheritdoc cref="IDisposable.Dispose"/>
		public void Dispose()
		{
			_cts.CancelAndDispose();
			GC.SuppressFinalize(this);
		}
	}
}
