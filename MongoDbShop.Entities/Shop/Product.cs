using Elephant.Database.MongoDb.Types;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDbShop.Entities.Shop
{
	/// <summary>
	/// Product.
	/// </summary>
	[BsonIgnoreExtraElements]
	public class Product : BaseId
	{
		/// <summary>
		/// Name.
		/// </summary>
		public string Name { get; set; } = string.Empty;

		/// <summary>
		/// Quantity in store.
		/// </summary>
		public int Quantity { get; set; } = 0;

		/// <summary>
		/// Price per entity.
		/// </summary>
		public decimal Price { get; set; } = 1.25M;

		/// <summary>
		/// Description.
		/// </summary>
		public string Description { get; set; } = string.Empty;

		/// <summary>
		/// Constructor.
		/// </summary>
		public Product()
		{
		}

		/// <summary>
		/// Constructor with initializers.
		/// </summary>
		public Product(string name, int quantity, decimal price, string description)
		{
			Name = name;
			Quantity = quantity;
			Price = price;
			Description = description;
		}
	}
}
