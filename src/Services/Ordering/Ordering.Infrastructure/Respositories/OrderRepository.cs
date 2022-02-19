using Ordering.Application.Contracts.Respositories;
using Ordering.Domain.Entities;
using Ordering.Infrastructure.Persistence;

namespace Ordering.Infrastructure.Respositories
{
	public class OrderRepository : AsyncRepository<Order>, IOrderRepository
	{
		public OrderRepository(OrderContext orderContext)
			: base(orderContext)
		{ }

		public async Task<IEnumerable<Order>> GetOrderByUserName(string userName)
		{
			return await GetAsync(q => q.UserName == userName);
		}
	}
}
