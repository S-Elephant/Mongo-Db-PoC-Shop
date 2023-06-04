using Elephant.Database.MongoDb.Repositories;
using MongoDbShop.Dal.Abstractions.Repositories;
using MongoDbShop.Dal.Contexts;
using MongoDbShop.Entities.Shop;

namespace MongoDbShop.Dal.Repositories
{
	/// <inheritdoc cref="IProductRepository"/>
	public class ProductRepository : GenericCrudRepository<Product>, IProductRepository
	{
		/// <summary>
		/// Constructor.
		/// </summary>
		public ProductRepository(IShopContext shopContext) : base(shopContext.Products)
		{
		}
	}
}
