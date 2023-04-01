using Elephant.Types;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Driver;
using MongoDbShop.Entities;
using MongoDbShop.Shared;

namespace MongoDbShop.Pages
{
	/// <summary>
	/// Index page.
	/// </summary>
	public partial class Index : IDisposable
	{
		// Normally, these would be put inside a repository.
		private IMongoClient _mongoDbClient = null!;
		private IMongoDatabase _databaseShop = null!;
		private IMongoCollection<Product> _collectionProduct = null!;

		private ElephantCancellationTokenSource _cts = new();
		private string _output = string.Empty;
		private JsonWriterSettings _jsonWriterSettings = new();
		private List<Product> _products = new();
		private long _productCount;
		private List<Product> _productsFiltered = new();
		private string _filterPropertyName = "name";
		private string _filterValue = "Wood planks";

		/// <summary>
		/// On initialize.
		/// </summary>
		protected override void OnInitialized()
		{
			// Normally, these would be put inside a repository.
			_mongoDbClient = new MongoClient(Constants.ConnectionString);
			_databaseShop = _mongoDbClient.GetDatabase(Constants.DatabaseName);
			_collectionProduct = _databaseShop.GetCollection<Product>(Constants.CollectionNameProduct);

			_jsonWriterSettings = new JsonWriterSettings
			{
				OutputMode = JsonOutputMode.CanonicalExtendedJson,
				Indent = true,
			};

			base.OnInitialized();
		}

		/// <summary>
		/// List all databases.
		/// </summary>
		private async Task ListAllDatabases()
		{
			List<BsonDocument> databases = (await _mongoDbClient.ListDatabasesAsync(_cts.Token)).ToList();
			_output = databases.ToJson(_jsonWriterSettings);
			StateHasChanged();
		}

		/// <summary>
		/// List all <see cref="Product"/>s.
		/// </summary>
		private async Task ListAllProducts()
		{
			_products = await _collectionProduct.Find(new BsonDocument()).ToListAsync(_cts.Token);
			StateHasChanged();
		}

		/// <summary>
		/// Count all  <see cref="Product"/>s in database.
		/// </summary>
		private async Task CountAllProducts()
		{
			_productCount = await _collectionProduct.CountDocumentsAsync(_ => true, cancellationToken: _cts.Token);
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

		/// <inheritdoc cref="IDisposable.Dispose"/>
		public void Dispose()
		{
			_cts.CancelAndDispose();
			GC.SuppressFinalize(this);
		}
	}
}
