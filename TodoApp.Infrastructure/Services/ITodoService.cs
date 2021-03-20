using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApp.Infrastructure.Models;

namespace TodoApp.Infrastructure.Services
{
	public interface ITodoService
	{
		Task<List<TodoDTO>> GetTodoList();
		Task<bool> CreateTodo(TodoDTO newTodo);
		Task<TodoDTO> GetTodoById(Guid id);
		Task<bool> UpdateTodo(TodoDTO updatedTodoDTO);
		Task<bool> UpdateTodoState(Guid id, States newState);
		Task<bool> DeleteTodo(Guid id);
	}
}