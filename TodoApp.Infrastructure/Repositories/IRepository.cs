using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace TodoApp.Infrastructure.Repositories
{
	public interface IRepository<TItem, TKey>
	{
		Task<IEnumerable<TItem>> GetAllAsync(CancellationToken cancellationToken = default);
		Task<TItem> GetByIdAsync(TKey id, CancellationToken cancellationToken = default);
		Task AddAsync(TItem item, CancellationToken cancellationToken = default);
		Task DeleteAsync(TKey id, CancellationToken cancellationToken = default);
		Task<bool> ExistAsync(Expression<Func<TItem, bool>> prediction, CancellationToken cancellationToken = default);
		Task UpdateAsync(TItem item, CancellationToken cancellationToken = default);
		void SaveChanges();
	}
}