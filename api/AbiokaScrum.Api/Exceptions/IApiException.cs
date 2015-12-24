using System.Collections.Generic;
using System.Net;

namespace AbiokaScrum.Exceptions
{
    public interface IApiException
    {
        object ContentValue { get; }

        HttpStatusCode StatusCode { get; }

        IDictionary<string, string> ExtraHeaders { get; }
    }
}
