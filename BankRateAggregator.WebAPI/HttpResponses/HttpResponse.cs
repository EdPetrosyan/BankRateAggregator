using System.Net;

namespace BankRateAggregator.WebAPI.HttpResponses;

public class Ok<T> : BaseHttpResponse
{
    public T Data { get; set; }
    public Ok(T data) : base(HttpStatusCode.OK, true, null)
    {
        Data = data;
    }
}

public class Ok : BaseHttpResponse
{
    public Ok() : base() { }

}

public class NotFound : BaseHttpResponse
{
    public NotFound(string errorMessage) : base(HttpStatusCode.NotFound, false, new List<string> { errorMessage }) { }
}

public class BadRequest : BaseHttpResponse
{
    public BadRequest(IList<string>? errorMessages) : base(HttpStatusCode.BadRequest, false, errorMessages) { }
    public BadRequest(string errorMessage) : base(HttpStatusCode.BadRequest, false, new List<string> { errorMessage }) { }
}

public class Forbidden : BaseHttpResponse
{
    public Forbidden(string errorMessage) : base(HttpStatusCode.Forbidden, false, new List<string> { errorMessage }) { }
}

public class CancelledRequest : BadRequest
{
    public CancelledRequest() : base("Request Was Cancelled.") { }
}