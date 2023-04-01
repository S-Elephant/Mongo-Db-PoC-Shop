using System;
using Microsoft.Maui;
using Microsoft.Maui.Hosting;

namespace MongoDbShop;

/// <summary>
/// Program.
/// </summary>
class Program : MauiApplication
{
	protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();

	/// <summary>
	/// Main.
	/// </summary>
	static void Main(string[] args)
	{
		var app = new Program();
		app.Run(args);
	}
}
