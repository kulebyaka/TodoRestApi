using System;
using TodoApp.Infrastructure.Models;

namespace TodoApp.Web.ViewModels
{
	public class TodoViewModel
	{
		public Guid Id { get; set; }
		public string Title { get; set; }

		public int Priority { get; set; }

		public State State { get; set; }

		public DateTime CreateDate { get; set; }
	}
}