using System.Net;
using System.Text.Json.Serialization;

namespace BankRateAggregator.WebAPI.HttpResponses
{
    public abstract class BaseHttpResponse
    {
        public HttpStatusCode Code { get; set; }
        public bool Success { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public IList<string>? ErrorMessages { get; set; }

        protected BaseHttpResponse()
        {
            Code = HttpStatusCode.OK;
            Success = true;
            ErrorMessages = null;
        }

        protected BaseHttpResponse(HttpStatusCode code, bool success, IList<string>? errorMessages)
        {
            Code = code;
            Success = success;
            ErrorMessages = errorMessages;
        }
    }
}
