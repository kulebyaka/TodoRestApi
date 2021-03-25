using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TodoApp.Infrastructure.Models;
using TodoApp.Infrastructure.Services;
using TodoApp.Web.ViewModels;

namespace TodoApp.Web.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class TodosController : ControllerBase
	{
		private readonly ITodoService _todoService;
		private readonly ILogger<TodosController> _logger;
		private readonly IMapper _mapper;

		public TodosController(ITodoService todoService,
			IMapper mapper,
			ILogger<TodosController> logger)
		{
			this._todoService = todoService;
			this._mapper = mapper;
			this._logger = logger;
		}

		// GET: api/list
		[HttpGet("list")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> GetTodoList()
		{
			var todos = await _todoService.GetTodoList();
			var result = _mapper.Map<IEnumerable<TodoDTO>, IEnumerable<TodoViewModel>>(todos);
			return Ok(result);
		}

		// GET: api/id
		[HttpGet("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> Get(Guid id)
		{
			var foundTodo = await _todoService.GetTodoById(id);
			if (foundTodo == null)
			{
				return BadRequest(StatusCodes.Status404NotFound);
			}

			return Ok(_mapper.Map<TodoViewModel>(foundTodo));
		}

		// POST: api/Todos
		[HttpPost("add")]
		public async Task<IActionResult> CreateTodo([FromBody] TodoViewModel newTodo)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			TodoDTO isCreated = await _todoService.CreateTodo(_mapper.Map<TodoDTO>(newTodo));

			if (isCreated == null)
			{
				return BadRequest();
			}

			return Ok(_mapper.Map<TodoViewModel>(isCreated));
		}

		// PUT: api/Todos/id
		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateTodo(string id, [FromBody] TodoViewModel updatedTodo)
		{
			var isUpdated = await _todoService.UpdateTodo(_mapper.Map<TodoDTO>(updatedTodo));
			if (isUpdated == false)
			{
				return BadRequest();
			}

			return Ok();
		}

		// PUT: api/Todos/id
		[HttpPatch("{id}/complete")]
		public async Task<IActionResult> CompleteTodo([Required] Guid id)
		{
			bool isUpdated = await _todoService.UpdateTodoState(id, State.Completed);
			if (isUpdated == false)
			{
				return BadRequest();
			}

			return Ok();
		}

		// DELETE: api/id
		[HttpDelete("{id}")]
		public async Task<ActionResult> DeleteTodo([Required] Guid id)
		{
			var isDeleted = await _todoService.DeleteTodo(id);
			if (isDeleted == false)
			{
				return BadRequest();
			}

			return Ok();
		}
	}
}