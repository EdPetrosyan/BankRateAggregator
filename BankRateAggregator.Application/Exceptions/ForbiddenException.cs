using System.Runtime.Serialization;

namespace BankRateAggregator.Application.Exceptions;

[Serializable]
public class ForbiddenException : Exception
{
    public ForbiddenException() : base("Access Denied")
    {
    }

    protected ForbiddenException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        base.GetObjectData(info, context);
    }
}
