namespace TodoApp.Infrastructure.Models
{
	public interface IDbEntity<TKey>
	{
		public TKey Id { get; set; }
	}
}