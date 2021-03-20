using System;
using TodoApp.Infrastructure.Models;

namespace TodoApp.Web.ViewModels
{
	public class TodoViewModel
	{
		public Guid Id { get; set; }

		public string Title { get; set; }

		public string Description { get; set; }

		public PriorityStatus Priority { get; set; }

		public States State { get; set; }

		public DateTime Estimate { get; set; }
	}
}