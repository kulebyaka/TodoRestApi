using System;
using System.ComponentModel.DataAnnotations;

namespace TodoApp.Infrastructure.Models
{
	public class TodoDTO : IDbEntity<Guid>
	{
		public Guid Id { get; set; }
		[Required] [StringLength(50)] public string Title { get; set; }

		[StringLength(500)] public string Description { get; set; }

		[Required] public int Priority { get; set; }

		[Required] public State State { get; set; }

		public DateTime Estimate { get; set; }
	}
}