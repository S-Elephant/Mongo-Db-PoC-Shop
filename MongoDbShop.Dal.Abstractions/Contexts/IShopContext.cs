using Elephant.Database.MongoDb.Abstractions.Contexts;
using Elephant.Database.MongoDb.DbSets;
using MongoDbShop.Entities.Shop;

namespace MongoDbShop.Dal.Contexts
{
	/// <summary>
	/// Context.
	/// </summary>
	public interface IShopContext : IMongoContext
	{
		/// <summary>
		/// <see cref="Product"/> <see cref="DbSet{TEntity}"/>.
		/// </summary>
		public DbSet<Product> Products { get; set; }
	}
}
