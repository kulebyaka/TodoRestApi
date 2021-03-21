using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using TodoApp.Infrastructure.Models;

namespace TodoApp.Infrastructure.Repositories
{
	public class InMemoryRepository<TItem> : IRepository<TItem> where TItem : IDbEntity<Guid>
	{
		private const string msgWrongTypeOfPrimaryKey = "Wrong type of primary key for in-memory collection";
		private readonly IDictionary<Guid, TItem> _entities;

		public InMemoryRepository(IList<TItem> defaultCollection)
		{
			_entities = defaultCollection.ToDictionary(a => a.Id);
		}

		public async Task<IEnumerable<TItem>> GetAllAsync(CancellationToken cancellationToken = default)
		{
			return _entities.Values;
		}

		public async Task<TItem> GetByIdAsync<TKey>(TKey id, CancellationToken cancellationToken = default)
		{
			//TODO: Error handling
			if (id is not Guid guid)
				throw new Exception(msgWrongTypeOfPrimaryKey);

			_entities.TryGetValue(guid, out TItem o);
			return o;
		}

		public async Task DeleteAsync(TItem item, CancellationToken cancellationToken = default)
		{
			_entities.Remove(item.Id);
		}

		public async Task<bool> ExistAsync(Expression<Func<TItem, bool>> prediction, CancellationToken cancellationToken = default)
		{
			return _entities.Values.Where(prediction.Compile()).Any();
		}

		public async Task AddAsync(TItem item, CancellationToken cancellationToken = default)
		{
			_entities.Add(item.Id, item);
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