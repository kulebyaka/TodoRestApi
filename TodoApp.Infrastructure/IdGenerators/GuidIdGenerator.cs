using System;

namespace TodoApp.Infrastructure.IdGenerators
{
	public class GuidIdGenerator : IIdGenerator
	{
		public object Next(object currentValue)
		{
			Guid value = (Guid) currentValue;

			if (value != default(Guid))
				return value;

			return Guid.NewGuid();
		}
	}
}