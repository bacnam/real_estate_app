using Newtonsoft.Json;

namespace RealEstate.WebService
{
    public class DataResponse
    {
        public DataResponse(StatusCode statusCode) : this(statusCode, string.Empty)
        {
        }

        public DataResponse(string message) : this(StatusCode.OK, message)
        {
            Message = message;
        }

        public DataResponse(StatusCode statusCode, string message)
        {
            StatusCode = statusCode;
            Message = message;
        }

        [JsonProperty("message")]
        public string Message { get; }

        [JsonProperty("status")]
        public StatusCode StatusCode { get; set; }
    }

    public class DataResponse<T> : DataResponse
    {
        public DataResponse(StatusCode statusCode) : this(statusCode, string.Empty, default(T))
        {
        }

        public DataResponse(StatusCode statusCode, string message) : this(statusCode, message, default(T))
        {
        }

        public DataResponse(StatusCode statusCode, string message, T data) : base(statusCode, message)
        {
            Data = data;
        }

        public T Data { get; set; }
    }

    public class DataPageResponse<T> : DataResponse
    {
        public DataPageResponse(StatusCode statusCode) : base(statusCode, string.Empty)
        {
        }

        public DataPageResponse(StatusCode statusCode, string message) : base(statusCode, message)
        {
        }

        public DataPageResponse(StatusCode statusCode, string message, IEnumerable<T> data) : base(statusCode, message)
        {
            Data = data;
        }

        public IEnumerable<T> Data { get; set; }

        public long Total { get; set; }

        public long CurrentPage { get; set; }

        public long LastPage { get; set; }
    }

    public enum StatusCode
    {
        Continue = 100,
        SwitchingProtocols = 101,
        OK = 200,
        Created = 201,
        Accepted = 202,
        NonAuthoritativeInformation = 203,
        NoContent = 204,
        ResetContent = 205,
        PartialContent = 206,
        Ambiguous = 300,
        MultipleChoices = 300,
        Moved = 301,
        MovedPermanently = 301,
        Found = 302,
        Redirect = 302,
        RedirectMethod = 303,
        SeeOther = 303,
        NotModified = 304,
        UseProxy = 305,
        Unused = 306,
        RedirectKeepVerb = 307,
        TemporaryRedirect = 307,
        BadRequest = 400,
        Unauthorized = 401,
        PaymentRequired = 402,
        Forbidden = 403,
        NotFound = 404,
        MethodNotAllowed = 405,
        NotAcceptable = 406,
        ProxyAuthenticationRequired = 407,
        RequestTimeout = 408,
        Conflict = 409,
        Gone = 410,
        LengthRequired = 411,
        PreconditionFailed = 412,
        RequestEntityTooLarge = 413,
        RequestUriTooLong = 414,
        UnsupportedMediaType = 415,
        RequestedRangeNotSatisfiable = 416,
        ExpectationFailed = 417,
        UpgradeRequired = 426,
        InternalServerError = 500,
        NotImplemented = 501,
        BadGateway = 502,
        ServiceUnavailable = 503,
        GatewayTimeout = 504,
        HttpVersionNotSupported = 505
    }
}
