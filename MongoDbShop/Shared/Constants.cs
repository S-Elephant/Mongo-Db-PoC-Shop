namespace MongoDbShop.Shared
{
	/// <summary>
	/// Constants. Please edit these values to match your Mongo DB configuration.
	/// </summary>
	/// <remarks>Normally, these constants would be settings.</remarks>
	public static class Constants
	{
		/// <summary>
		/// Database name.
		/// </summary>
		public const string DatabaseName = "shop";

		/// <summary>
		/// Product collection name.
		/// </summary>
		public const string CollectionNameProduct = "product";

		/// <summary>
		/// Connection string.
		/// </summary>
		public const string ConnectionString = "mongodb://localhost:27017";
	}
}
