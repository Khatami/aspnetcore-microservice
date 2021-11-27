using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Catalog.API.Entities
{
	public class Product
	{
		/// <summary>
		/// MongoDB stores objects as BSON objects which are a binary JSON
		/// </summary>
		[BsonId]
		[BsonRepresentation(BsonType.ObjectId)] // 24 Characters 
		public string Id { get; set; }

		[BsonElement("Name")] // to specify element name, you can bind a different property name to another element name
		public string Name { get; set; }

		public string Category { get; set; }

		public string Summary { get; set; }

		public string Description { get; set; }

		public string ImageFile { get; set; }

		public decimal Price { get; set; }
	}
}
