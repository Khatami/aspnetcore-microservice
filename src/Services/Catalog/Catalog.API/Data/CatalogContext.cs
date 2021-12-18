using Catalog.API.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Catalog.API.Data
{
	public class CatalogContext : ICatalogContext
	{
		public CatalogContext(IConfiguration configuration)
		{
			var databaseSetting = configuration.GetSection("DatabaseSettings");
			var connectionString = databaseSetting.GetValue<string>("ConnectionString");

			var client = new MongoClient(connectionString);

			//It returns the database; if it does not exist, a new database will be created
			var database = client.GetDatabase(databaseSetting.GetValue<string>("DatabaseName"));

			//It populates the table; if it does not exist, a new table will be created
			Products = database.GetCollection<Product>(databaseSetting.GetValue<string>("CollectionName"));

			CatalogContextSeed.SeedData(Products);
		}

		public IMongoCollection<Product> Products { get; }
	}
}