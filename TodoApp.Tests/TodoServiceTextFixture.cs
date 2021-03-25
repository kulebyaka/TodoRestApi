using System.Collections.Generic;
using System.Threading.Tasks;
using Autofac;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using TodoApp.Infrastructure.IdGenerators;
using TodoApp.Infrastructure.Models;
using TodoApp.Infrastructure.Repositories;
using TodoApp.Infrastructure.Services;
using FluentAssertions;

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
			var builder = new ContainerBuilder();
			builder.RegisterType<TodoService>().As<ITodoService>().InstancePerLifetimeScope();
			builder.RegisterType<TodoRepository>().As<IRepository<TodoDTO>>().SingleInstance();

			Container = builder.Build();

			IRepository<TodoDTO> todoRepository = CreateRepository<IRepository<TodoDTO>, TodoDTO>(() => _todoStorage, new GuidIdGenerator(), ramec => ramec.Id);
			builder.RegisterInstance(todoRepository);

			Mock<ILogger<TodoService>> cartServiceLogger = CreateLogger<TodoService>(a => logMessages.Add(a));
			builder.RegisterInstance(cartServiceLogger.Object);
		}

		[Test]
		public async Task Test()
		{
			var service = Container.Resolve<ITodoService>();

			var x = await service.CreateTodo(new TodoDTO()
			{
				Title = "TestTodoTitle",
				Priority = 5,
				State = State.NotStarted
			});

			var all = await service.GetTodoList();
			all.Count.Should().BeGreaterThan(0);
		}
	}
}