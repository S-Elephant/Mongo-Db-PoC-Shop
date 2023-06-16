using Elephant.Database.MongoDb;
using Elephant.Database.MongoDb.Abstractions.Repositories;
using Elephant.Database.MongoDb.DependencyInjection;
using Elephant.Database.MongoDb.Repositories;
using MongoDbShop.Dal.Abstractions.Repositories;
#if DEBUG
using Microsoft.Extensions.Logging;
#endif
using MongoDB.Driver;
using MongoDbShop.Dal.Contexts;
using MongoDbShop.Dal.Repositories;
using MongoDbShop.Shared;

namespace MongoDbShop.Infrastructure
{
	/// <summary>
	/// Startup class.
	/// </summary>
	public class Startup
	{
		/// <summary>
		/// Add services to the dependency injection.
		/// </summary>
		public void ConfigureServices(MauiAppBuilder builder)
		{
			IServiceCollection services = builder.Services;

			services.AddMauiBlazorWebView();

			ConfigureDatabases(services);

#if DEBUG
			services.AddBlazorWebViewDeveloperTools();
			builder.Logging.AddDebug();
#endif


			services.AddScoped<IProductRepository, ProductRepository>();
			services.AddScoped<IDatabaseRepository, DatabaseRepository>(x => new DatabaseRepository(new MongoClient(Constants.ConnectionString)));
		}

		private static void ConfigureDatabases(IServiceCollection services)
		{
			ConventionPacks.EnforceGlobalCamelCase();

			services.AddMongoContext<IShopContext, ShopContext>(options =>
			{
				options.ConnectionString = Constants.ConnectionString;
				options.DatabaseName = Constants.DatabaseName;
			});
		}
	}
}
