namespace TodoApp.Infrastructure.Models
{
	public interface IDbEntity<out TKey>
	{
		public TKey Id { get; }
	}
}