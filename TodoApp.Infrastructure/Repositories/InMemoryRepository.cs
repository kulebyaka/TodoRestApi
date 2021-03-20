using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using TodoApp.Infrastructure.Models;

namespace TodoApp.Infrastructure.Repositories
{
	public class InMemoryRepository<TItem, TKey> : IRepository<TItem, TKey> where TItem : IDbEntity<TKey>
	{
		private readonly IDictionary<TKey, TItem> _entities;

		public InMemoryRepository(IList<TItem> defaultCollection)
		{
			_entities = defaultCollection.ToDictionary(a => a.Id);
		}

		public async Task<IEnumerable<TItem>> GetAllAsync(CancellationToken cancellationToken = default)
		{
			return _entities.Values;
		}

		public async Task<TItem> GetByIdAsync(TKey id, CancellationToken cancellationToken = default)
		{
			_entities.TryGetValue(id, out TItem o);
			return o;
		}

		public async Task<bool> ExistAsync(Expression<Func<TItem, bool>> prediction, CancellationToken cancellationToken = default)
		{
			return _entities.Values.Where(prediction.Compile()).Any();
		}

		public async Task AddAsync(TItem item, CancellationToken cancellationToken = default)
		{
			_entities.Add(item.Id, item);
		}

		public async Task DeleteAsync(TKey id, CancellationToken cancellationToken = default)
		{
			_entities.Remove(id);
		}

		public async Task UpdateAsync(TItem item, CancellationToken cancellationToken = default)
		{
			_entities[item.Id] = item;
		}

		public void SaveChanges()
		{
		}
	}
}