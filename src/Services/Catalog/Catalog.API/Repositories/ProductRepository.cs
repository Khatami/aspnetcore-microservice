using Catalog.API.Data;
using Catalog.API.Entities;
using MongoDB.Driver;

namespace Catalog.API.Repositories
{
	public class ProductRepository : IProductRepository
	{
		public ProductRepository(ICatalogContext catalogContext)
		{
			_catalogContext = catalogContext ?? throw new ArgumentNullException(nameof(catalogContext));
		}

		private readonly ICatalogContext _catalogContext;

		public async Task<IEnumerable<Product>> Get()
		{
			return await _catalogContext.Products.Find(p => true).ToListAsync();
		}

		public async Task<Product> GetById(string id)
		{
			return await _catalogContext.Products.Find(p => p.Id == id).FirstOrDefaultAsync();
		}

		public async Task<IEnumerable<Product>> GetProductByName(string name)
		{
			FilterDefinition<Product> filter = Builders<Product>
				.Filter
				.Eq(p => p.Name, name);

			return await _catalogContext.Products.Find(filter).ToListAsync();
		}

		public async Task<IEnumerable<Product>> GetProductByCategory(string categoryName)
		{
			FilterDefinition<Product> filter = Builders<Product>
				.Filter
				.Eq(p => p.Category, categoryName);

			return await _catalogContext.Products.Find(filter).ToListAsync();
		}

		public async Task Create(Product entity)
		{
			await _catalogContext.Products.InsertOneAsync(entity);
		}

		public async Task<bool> Update(Product entity)
		{
			var updateResult = await _catalogContext.Products
				.ReplaceOneAsync(filter: q => q.Id == entity.Id, replacement: entity);

			return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
		}

		public async Task<bool> Delete(string id)
		{
			var filter = Builders<Product>
				.Filter
				.Eq(p => p.Id, id);

			DeleteResult deleteResult = await _catalogContext.Products.DeleteOneAsync(filter);

			return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
		}
	}
}
