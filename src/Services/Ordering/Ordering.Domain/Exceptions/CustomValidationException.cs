using FluentValidation.Results;

namespace Ordering.Domain.Exceptions
{
	public class CustomValidationException : Exception
	{
		public CustomValidationException()
			: base("One or more validation failures have occured.")
		{
			Errors = new Dictionary<string, string[]>();
		}

		public CustomValidationException(IEnumerable<ValidationFailure> failures)
		{
			Errors = failures
				.GroupBy(e => e.PropertyName, e => e.ErrorMessage)
				.ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
		}

		public IDictionary<string, string[]> Errors { get; }
	}
}
