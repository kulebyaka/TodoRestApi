using Autofac;
using TodoApp.Infrastructure.Models;
using TodoApp.Infrastructure.Repositories;
using TodoApp.Infrastructure.Services;

namespace TodoApp.Web.Configuration
{
	/// <summary>
	/// Default module for Autofac
	/// </summary>
	public class DefaultModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterType<TodoService>().As<ITodoService>().InstancePerLifetimeScope();
			builder.RegisterType<TodoRepository>().As<IRepository<TodoDTO>>().SingleInstance();
		}
	}
}