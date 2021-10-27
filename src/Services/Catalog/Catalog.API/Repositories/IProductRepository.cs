using Catalog.API.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalog.API.Repositories
{
	public interface IProductRepository : IRepository<Product>
	{
		Task<IEnumerable<Product>> GetProductByName(string name);

		Task<IEnumerable<Product>> GetProductByCategory(string categoryName);
	}
}
