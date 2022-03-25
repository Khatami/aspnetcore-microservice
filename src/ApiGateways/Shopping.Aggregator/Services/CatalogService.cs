using Shopping.Aggregator.Extensions;
using Shopping.Aggregator.Models;

namespace Shopping.Aggregator.Services
{
	public class CatalogService : ICatalogService
	{
		private readonly HttpClient _httpClient;

		public CatalogService(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		public async Task<IEnumerable<CatalogModel>> GetCatalog()
		{
			var result = await _httpClient.GetAsync("/api/v1/catalog");

			return await result.ReadContentAs<List<CatalogModel>>();
		}

		public async Task<CatalogModel> GetCatalog(string id)
		{
			var result = await _httpClient.GetAsync($"/api/v1/catalog/{id}");

			return await result.ReadContentAs<CatalogModel>();
		}

		public async Task<IEnumerable<CatalogModel>> GetCatalogByCategory(string category)
		{
			var result = await _httpClient.GetAsync($"/api/v1/catalog/GetProductByCatagory/{category}");

			return await result.ReadContentAs<List<CatalogModel>>();
		}
	}
}
