namespace TodoApp.Infrastructure.IdGenerators
{
	public interface IIdGenerator
	{
		object Next(object currentValue);
	}
}