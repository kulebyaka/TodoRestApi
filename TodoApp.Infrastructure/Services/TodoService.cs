using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TodoApp.Infrastructure.Models;
using TodoApp.Infrastructure.Repositories;

namespace TodoApp.Infrastructure.Services
{
	public class TodoService : ITodoService
	{
		private readonly IRepository<TodoDTO> _todos;
		private readonly ILogger<TodoService> _logger;

		public TodoService(ILogger<TodoService> logger, IRepository<TodoDTO> todos)
		{
			_logger = logger;
			_todos = todos;
		}

		public async Task<List<TodoDTO>> GetTodoList()
		{
			List<TodoDTO> allTodos = null;
			try
			{
				IEnumerable<TodoDTO> result = await _todos.GetAllAsync();
				allTodos = result.ToList();
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Get all todos failed.");
			}

			return allTodos;
		}

		public async Task<TodoDTO> CreateTodo(TodoDTO newTodo)
		{
			try
			{
				// TODO: move to mappping
				if (newTodo.Id == Guid.Empty)
					newTodo.Id = Guid.NewGuid();

				await _todos.AddAsync(newTodo);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Create todo record failed.");
				throw;
			}

			return newTodo;
		}

		public async Task<TodoDTO> GetTodoById(Guid id)
		{
			TodoDTO foundTodo = null;
			try
			{
				foundTodo = await _todos.GetByIdAsync(id);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Get todo by ID failed.");
			}

			return foundTodo;
		}

		public async Task<bool> UpdateTodo(TodoDTO updatedTodoDTO)
		{
			var isUpdated = false;
			try
			{
				var foundTodo = await _todos.GetByIdAsync(updatedTodoDTO.Id);
				// TODO: mapper
				foundTodo.Title = updatedTodoDTO.Title;
				foundTodo.Priority = updatedTodoDTO.Priority;
				foundTodo.State = updatedTodoDTO.State;
				isUpdated = true;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Upadte todo failed.");
			}

			return isUpdated;
		}

		public async Task<bool> UpdateTodoState(Guid id, State newState)
		{
			var isUpdated = false;
			try
			{
				var foundTodo = await _todos.GetByIdAsync(id);
				foundTodo.State = newState;
				_todos.SaveChanges();
				isUpdated = true;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Upadte todo failed.");
			}

			return isUpdated;
		}

		public async Task<bool> DeleteTodo(Guid id)
		{
			var isDeleted = false;
			try
			{
				await _todos.DeleteAsync(new TodoDTO() {Id = id});
				isDeleted = true;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Delete todo failed.");
			}

			return isDeleted;
		}
	}
}