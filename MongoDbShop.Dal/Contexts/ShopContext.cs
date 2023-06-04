using Elephant.Database.MongoDb.Abstractions.Contexts;
using Elephant.Database.MongoDb.Contexts;
using Elephant.Database.MongoDb.DbSets;
using MongoDbShop.Entities.Shop;

namespace MongoDbShop.Dal.Contexts
{
	/// <summary>
	/// Context.
	/// </summary>
	public class ShopContext : MongoContext, IShopContext
	{
		/// <inheritdoc/>
		public DbSet<Product> Products { get; set; } = null!;

		/// <summary>
		/// Constructor.
		/// </summary>
		public ShopContext(IMongoContextOptionsBuilder optionsBuilder) : base(optionsBuilder)
		{
		}

		/// <inheritdoc cref="OnConfiguring"/>
		protected override void OnConfiguring()
		{
			AutoLoadConfigurationsByAssemblyNames(true, "MongoDbShop.Dal");
		}
	}
}
