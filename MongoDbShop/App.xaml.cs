namespace MongoDbShop;

/// <inheritdoc cref="Application"/>
public partial class App : Application
{
	/// <summary>
	/// Constructor.
	/// </summary>
	public App()
	{
		InitializeComponent();

		MainPage = new MainPage();
	}
}
