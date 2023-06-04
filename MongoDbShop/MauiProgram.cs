using MongoDbShop.Infrastructure;

namespace MongoDbShop;

/// <summary>
/// MAUI program.
/// </summary>
public static class MauiProgram
{
	/// <summary>
	/// Create MAUI Application.
	/// </summary>
	public static MauiApp CreateMauiApp()
	{
		MauiAppBuilder builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
			});

		new Startup().ConfigureServices(builder);

		return builder.Build();
	}
}
