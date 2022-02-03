namespace Ordering.Domain.Exceptions
{
	public class CustomNotFoundException : Exception
	{
		public CustomNotFoundException(string name, object key)
			: base($"Entity \"{name}\" ({key}) was not found")
		{

		}
	}
}
