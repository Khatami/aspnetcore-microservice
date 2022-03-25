using Shopping.Aggregator.Extensions;
using Shopping.Aggregator.Models;

namespace Shopping.Aggregator.Services
{
	public class OrderService : IOrderService
	{
		private readonly HttpClient _httpClient;

		public OrderService(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		public async Task<IEnumerable<OrderModel>> GetOrdersByUserName(string userName)
		{
			var result = await _httpClient.GetAsync($"/api/v1/Order/{userName}");

			return await result.ReadContentAs<List<OrderModel>>();
		}
	}
}
