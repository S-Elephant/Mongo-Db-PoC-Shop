#if DEBUG
using Microsoft.Extensions.Logging;
#endif

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

		builder.Services.AddMauiBlazorWebView();

#if DEBUG
		builder.Services.AddBlazorWebViewDeveloperTools();
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
