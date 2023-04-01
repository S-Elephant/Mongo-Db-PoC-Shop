using Foundation;

namespace MongoDbShop;

/// <inheritdoc cref="MauiUIApplicationDelegate"/>
[Register("AppDelegate")]
public class AppDelegate : MauiUIApplicationDelegate
{
	/// <inheritdoc cref="MauiUIApplicationDelegate.CreateMauiApp"/>
	protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
}
