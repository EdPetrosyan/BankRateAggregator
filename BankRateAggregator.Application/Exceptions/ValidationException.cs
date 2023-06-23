using FluentValidation.Results;
using System.Runtime.Serialization;

namespace BankRateAggregator.Application.Exceptions;

[Serializable]
public class ValidationException : Exception
{
    public IList<string>? Errors { get; } = null;

    public ValidationException() : base("One or more validation failures have occurred.")
    {
    }

    public ValidationException(string failure)
    : this()
    {
        Errors = new List<string> { failure };
    }

    public ValidationException(IEnumerable<ValidationFailure> failures)
        : this()
    {
        Errors = failures.Select(x => x.ErrorMessage).ToList();
    }

    protected ValidationException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {

        if (info.GetValue(nameof(Errors), typeof(IList<string>)) is IList<string> errors)
        {
            Errors = errors;
        }
    }

    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        if (info.GetValue(nameof(Errors), typeof(IList<string>)) is IList<string> errors)
        {
            info.AddValue(nameof(Errors), errors);
        }

        base.GetObjectData(info, context);
    }
}
