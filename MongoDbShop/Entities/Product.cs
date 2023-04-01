using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDbShop.Entities
{
    /// <summary>
    /// Product.
    /// </summary>
    [BsonIgnoreExtraElements]
    internal class Product
    {
        /// <summary>
        /// Unique Mongo DB identifier.
        /// </summary>
        [BsonElement("_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id1 { get; set; } = ObjectId.GenerateNewId().ToString();

        /// <summary>
        /// Name.
        /// </summary>
        [BsonElement("name")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Quantity in store.
        /// </summary>
        [BsonElement("quantity")]
        public int Quantity { get; set; } = 0;

        /// <summary>
        /// Price per entity.
        /// </summary>
        [BsonElement("price")]
        public decimal Price { get; set; } = 1.25M;

        /// <summary>
        /// Description.
        /// </summary>
        [BsonElement("description")]
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Constructor.
        /// </summary>
        public Product()
        {
        }
    }
}
