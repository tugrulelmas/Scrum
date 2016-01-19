using System.Collections.Generic;
using System.Net;

namespace AbiokaScrum.Api.Exceptions
{
    public interface IApiException
    {
        object ContentValue { get; }

        HttpStatusCode StatusCode { get; }

        IDictionary<string, string> ExtraHeaders { get; }
    }
}
