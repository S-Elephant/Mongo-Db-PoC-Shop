using Elephant.Database.MongoDb.Abstractions.Repositories;
using MongoDbShop.Entities.Shop;

namespace MongoDbShop.Dal.Abstractions.Repositories
{
	/// <summary>
	/// Product repository.
	/// </summary>
    public interface IProductRepository : IGenericCrudRepository<Product>
    {
    }
}
