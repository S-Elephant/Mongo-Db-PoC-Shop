using Elephant.Database.MongoDb.Abstractions.Contexts;
using Elephant.Database.MongoDb.Configurations;
using MongoDB.Driver;
using MongoDbShop.Entities.Shop;

namespace MongoDbShop.Dal.Configurations
{
	/// <summary>
	///<see cref="Product"/> configuration.
	/// </summary>
	// ReSharper disable once UnusedMember.Global
	public class ProductConfiguration : BaseConfiguration
	{
		/// <summary>
		/// Configure <see cref="Product"/>.
		/// </summary>
		public override void Configure(IMongoContextOptionsBuilder optionsBuilder)
		{
			const string tableNameInDatabase = "product";
			optionsBuilder.Entity<Product>(entity =>
			{
				IMongoCollection<Product> productsCollection = entity.ToCollection(tableNameInDatabase);

				IndexKeysDefinitionBuilder<Product> notificationLogBuilder = Builders<Product>.IndexKeys;
				CreateIndexModel<Product> indexModel = new(notificationLogBuilder.Ascending(x => x.MongoId));
				productsCollection.Indexes.CreateOne(indexModel);
			});
		}
	}
}
