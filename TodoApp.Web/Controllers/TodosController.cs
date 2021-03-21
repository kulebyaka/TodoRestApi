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
		public async Task<IEnumerable<TodoOverviewViewModel>> GetTodoList()
		{
			var todos = await _todoService.GetTodoList();
			var result = _mapper.Map<IEnumerable<TodoDTO>, IEnumerable<TodoOverviewViewModel>>(todos);
			return result;
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
				return BadRequest();
			}

			return Ok(_mapper.Map<TodoViewModel>(foundTodo));
		}

		// POST: api/Todos
		[HttpPost]
		public async Task<IActionResult> CreateTodo([FromBody] TodoDTO newTodo)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var isCreated = await _todoService.CreateTodo(newTodo);

			if (isCreated == false)
			{
				return BadRequest();
			}

			return Ok();
		}

		// PUT: api/Todos/id
		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateTodo(string id, [FromBody] TodoDTO updatedTodo)
		{
			var isUpdated = await _todoService.UpdateTodo(updatedTodo);
			if (isUpdated == false)
			{
				return BadRequest();
			}

			return Ok();
		}

		// PUT: api/Todos/id
		[HttpPatch("{id}")]
		public async Task<IActionResult> PatchTodo(string id, [FromBody] TodoDTO updatedTodo)
		{
			var isUpdated = await _todoService.UpdateTodo(updatedTodo);
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