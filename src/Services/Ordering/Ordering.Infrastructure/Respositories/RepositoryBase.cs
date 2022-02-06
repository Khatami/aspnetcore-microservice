using Ordering.Application.Contracts.Persistence;
using Ordering.Domain.Common;
using Ordering.Infrastructure.Persistence;
using System.Linq.Expressions;

namespace Ordering.Infrastructure.Respositories
{
	public class RepositoryBase<T> : IAsyncRepository<T> where T : EntityBase
	{
		private readonly OrderContext _orderContext;

		public RepositoryBase(OrderContext orderContext)
		{
			_orderContext = orderContext;
		}

		public Task<IReadOnlyList<T>> GetAllAsync()
		{
			throw new NotImplementedException();
		}

		public Task<T> AddAsync(T entity)
		{
			throw new NotImplementedException();
		}

		public Task DeleteAsync(T entity)
		{
			throw new NotImplementedException();
		}

		public Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate)
		{
			throw new NotImplementedException();
		}

		public Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeString = null, bool disableTracking = true)
		{
			throw new NotImplementedException();
		}

		public Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, List<Expression<Func<T, object>>> includes = null, bool disableTracking = true)
		{
			throw new NotImplementedException();
		}

		public Task<T> GetByIdAsync(int id)
		{
			throw new NotImplementedException();
		}

		public Task UpdateAsync(T entity)
		{
			throw new NotImplementedException();
		}
	}
}
