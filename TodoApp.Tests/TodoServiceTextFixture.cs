using System.Collections.Generic;
using DryIoc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using TodoApp.Infrastructure.IdGenerators;
using TodoApp.Infrastructure.Models;
using TodoApp.Infrastructure.Repositories;
using TodoApp.Infrastructure.Services;

namespace TodoApp.Tests
{
	[TestFixture]
	[Parallelizable(ParallelScope.Self)]
	public class TodoServiceTextFixture : TestBase
	{
		private List<TodoDTO> _todoStorage = new();

		[SetUp]
		public override void Setup()
		{
			base.Setup();

			Container.Register<ITodoService, TodoService>();
			Container.Register<IRepository<TodoDTO>, TodoRepository>(Reuse.Singleton);

			IRepository<TodoDTO> todoRepository = CreateRepository<IRepository<TodoDTO>, TodoDTO>(() => _todoStorage, new GuidIdGenerator(), ramec => ramec.Id);
			Container.Use(todoRepository);
			Container.RegisterInstance(todoRepository);

			Mock<ILogger<TodoService>> cartServiceLogger = CreateLogger<TodoService>(a => logMessages.Add(a));
			Container.Use(cartServiceLogger.Object);
			Container.RegisterInstance(cartServiceLogger.Object);
		}

		[Test]
		public void Test()
		{
		}
	}
}