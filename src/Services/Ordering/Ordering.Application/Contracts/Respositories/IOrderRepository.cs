using Ordering.Domain.Entities;

namespace Ordering.Application.Contracts.Respositories
{
	public interface IOrderRepository : IAsyncRepository<Order>
	{
		Task<IEnumerable<Order>> GetOrderByUserName(string userName);
	}
}