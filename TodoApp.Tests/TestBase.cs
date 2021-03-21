using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using Autofac.Core;
using Microsoft.Extensions.Logging;
using Moq;
using TodoApp.Infrastructure.IdGenerators;
using TodoApp.Infrastructure.Repositories;

namespace TodoApp.Tests
{
	public abstract class TestBase
	{
		protected readonly List<string> logMessages = new();

		protected IContainer Container { get; set; }

		public virtual void Setup()
		{
			var builder = new ContainerBuilder();
			RegisterCommonServices();
			Container = builder.Build();
		}

		private void RegisterCommonServices()
		{
		}

		protected static TRepository CreateRepository<TRepository, TItem>(Func<IList<TItem>> itemListProvider, IIdGenerator idGenerator,
			Expression<Func<TItem, object>> idProviderExpression = null) where TItem : class
			where TRepository : class, IRepository<TItem>
		{
			Mock<TRepository> repositoryMock = new Mock<TRepository>();


			repositoryMock.Setup(repo => repo.GetAllAsync(It.IsAny<CancellationToken>()))
				.Returns((CancellationToken cancellation) =>
				{
					IEnumerable<TItem> items = itemListProvider();
					return Task.FromResult(items.ToList().AsEnumerable());
				});

			repositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<object>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync((object id, CancellationToken cancellationToken) =>
				{
					PropertyInfo propertyInfo = GetIdPropertyInfo(idProviderExpression);

					return itemListProvider().FirstOrDefault(item =>
					{
						object idValue = propertyInfo.GetValue(item);

						return idValue.Equals(id);
					});
				});

			repositoryMock.Setup(repo => repo.AddAsync(It.IsAny<TItem>(), It.IsAny<CancellationToken>()))
				.Returns((TItem item, CancellationToken cancellationToken) =>
				{
					itemListProvider().Add(item);

					TrySetId(item, idGenerator, idProviderExpression);

					return Task.FromResult(0);
				});

			repositoryMock.Setup(repo => repo.DeleteAsync(It.IsAny<TItem>(), It.IsAny<CancellationToken>()))
				.Returns((TItem item, CancellationToken cancellationToken) =>
				{
					itemListProvider().Remove(item);
					return Task.FromResult(0);
				});


			repositoryMock.Setup(repo => repo.ExistAsync(It.IsAny<Expression<Func<TItem, bool>>>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync((Expression<Func<TItem, bool>> predicate, CancellationToken cancellationToken) =>
				{
					return itemListProvider().Any(predicate.Compile());
				});

			repositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<TItem>(), It.IsAny<CancellationToken>()))
				.Returns((TItem item, CancellationToken cancellationToken) => { return Task.FromResult(0); });

			return repositoryMock.Object;
		}

		private static void TrySetId<TItem>(TItem item, IIdGenerator idGenerator, Expression<Func<TItem, object>> idProviderExpression) where TItem : class
		{
			PropertyInfo idProperty = GetIdPropertyInfo(idProviderExpression);

			if (idProperty == null)
				return;

			idProperty.SetValue(item, idGenerator.Next(idProperty.GetValue(item)));
		}

		private static PropertyInfo GetIdPropertyInfo<TItem>(Expression<Func<TItem, object>> idProviderExpression) where TItem : class
		{
			return idProviderExpression == null ? GetIdPropertyInfo(typeof(Type)) : GetIdPropertyInfoInternal(idProviderExpression);
		}

		private static PropertyInfo GetIdPropertyInfoInternal<TItem>(Expression<Func<TItem, object>> idProviderExpression) where TItem : class
		{
			UnaryExpression unaryExpression = idProviderExpression.Body as UnaryExpression;

			if (unaryExpression == null)
				throw new ArgumentException("Lambda should be UnaryExpression");

			MemberExpression memberExpression = unaryExpression.Operand as MemberExpression;

			if (memberExpression == null)
				throw new ArgumentException("Lambda should be return Property or field");

			PropertyInfo propertyInfo = memberExpression.Member as PropertyInfo;


			return propertyInfo ?? GetIdPropertyInfo(typeof(TItem));
		}

		private static PropertyInfo GetIdPropertyInfo(Type type)
		{
			PropertyInfo idProperty = type.GetProperty("Id");

			idProperty = idProperty ?? type.GetProperty($"{type.Name}Id");
			return idProperty;
		}

		protected static Mock<ILogger<T>> CreateLogger<T>(Action<string> logProvider)
		{
			var logger = new Mock<ILogger<T>>();
			logger.Setup(x => x.Log(
					It.IsAny<LogLevel>(),
					It.IsAny<EventId>(),
					It.IsAny<It.IsAnyType>(),
					It.IsAny<Exception>(),
					(Func<It.IsAnyType, Exception, string>) It.IsAny<object>()))
				.Callback(new InvocationAction(invocation =>
				{
					object state = invocation.Arguments[2];
					var exception = (Exception) invocation.Arguments[3];
					object formatter = invocation.Arguments[4];
					MethodInfo invokeMethod = formatter.GetType().GetMethod("Invoke");
					string logMessage = (string) invokeMethod?.Invoke(formatter, new[] {state, exception});

					logProvider.Invoke(logMessage);
				}));

			return logger;
		}
	}
}