using UIKit;

namespace MongoDbShop;

/// <summary>
/// Program.
/// </summary>
public class Program
{
	/// <summary>
	/// Main entry point of the application.
	/// </summary>
	private static void Main(string[] args)
	{
		// if you want to use a different Application Delegate class from "AppDelegate"
		// you can specify it here.
		UIApplication.Main(args, null, typeof(AppDelegate));
	}
}