using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TodoApp.Infrastructure.Models;
using TodoApp.Infrastructure.Repositories;

namespace TodoApp.Infrastructure.Services
{
	public class TodosService : ITodoService
	{
		private readonly IRepository<TodoDTO> todos;
		private readonly ILogger<TodosService> logger;

		public TodosService(ILogger<TodosService> logger)
		{
			this.logger = logger;
		}

		public async Task<List<TodoDTO>> GetTodoList()
		{
			List<TodoDTO> allTodos = null;
			try
			{
				IEnumerable<TodoDTO> result = await todos.GetAllAsync();
				allTodos = result.ToList();
			}
			catch (Exception ex)
			{
				logger.LogError(ex, "Get all todos failed.");
			}

			return allTodos;
		}

		public async Task<bool> CreateTodo(TodoDTO newTodo)
		{
			var isCreated = false;
			try
			{
				await todos.AddAsync(newTodo);
				isCreated = true;
			}
			catch (Exception ex)
			{
				logger.LogError(ex, "Create todo record failed.");
			}

			return isCreated;
		}

		public async Task<TodoDTO> GetTodoById(Guid id)
		{
			TodoDTO foundTodo = null;
			try
			{
				foundTodo = await todos.GetByIdAsync(id);
			}
			catch (Exception ex)
			{
				logger.LogError(ex, "Get todo by ID failed.");
			}

			return foundTodo;
		}

		public async Task<bool> UpdateTodo(TodoDTO updatedTodoDTO)
		{
			var isUpdated = false;
			try
			{
				var foundTodo = await todos.GetByIdAsync(updatedTodoDTO.Id);
				foundTodo.Title = updatedTodoDTO.Title;
				foundTodo.Description = updatedTodoDTO.Description;
				foundTodo.Priority = updatedTodoDTO.Priority;
				foundTodo.State = updatedTodoDTO.State;
				foundTodo.Estimate = updatedTodoDTO.Estimate;
				isUpdated = true;
			}
			catch (Exception ex)
			{
				logger.LogError(ex, "Upadte todo failed.");
			}

			return isUpdated;
		}

		public async Task<bool> UpdateTodoState(Guid id, State newState)
		{
			var isUpdated = false;
			try
			{
				var foundTodo = await todos.GetByIdAsync(id);
				foundTodo.State = newState;
			}
			catch (Exception ex)
			{
				logger.LogError(ex, "Upadte todo failed.");
			}

			return isUpdated;
		}

		public async Task<bool> DeleteTodo(Guid id)
		{
			var isDeleted = false;
			try
			{
				await todos.DeleteAsync(id);
				isDeleted = true;
			}
			catch (Exception ex)
			{
				logger.LogError(ex, "Delete todo failed.");
			}

			return isDeleted;
		}
	}
}