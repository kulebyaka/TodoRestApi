using System;

namespace TodoApp.Infrastructure.Models
{
	public class TodoDTO : IDbEntity<Guid>
	{
		public Guid Id { get; set; }
		public string Title { get; set; }

		public int Priority { get; set; }

		public State State { get; set; }
	}
}