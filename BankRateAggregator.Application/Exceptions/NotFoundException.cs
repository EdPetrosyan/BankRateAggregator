using System.Runtime.Serialization;


namespace BankRateAggregator.Application.Exceptions;

[Serializable]
public class NotFoundException : Exception
{
    public NotFoundException() : base("Resource was not found") { }
    protected NotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        base.GetObjectData(info, context);
    }
}
