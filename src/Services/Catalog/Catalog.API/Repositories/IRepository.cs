using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalog.API.Repositories
{
	public interface IRepository<T>
	{
		Task<IEnumerable<T>> Get();

		Task Create(T entity);

		Task<bool> Update(T entity);

		Task<bool> Delete(string id);

		Task<T> GetById(string id);
	}
}