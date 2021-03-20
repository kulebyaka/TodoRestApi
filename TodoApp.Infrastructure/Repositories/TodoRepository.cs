using System;
using System.Collections.Generic;
using TodoApp.Infrastructure.Models;

namespace TodoApp.Infrastructure.Repositories
{
	public class TodoRepository : InMemoryRepository<TodoDTO, Guid>
	{
		public TodoRepository(IList<TodoDTO> defaultCollection) : base(defaultCollection)
		{
		}
	}
}