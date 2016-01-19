using System.Net.Http;

namespace AbiokaScrum.Api.Exceptions.Adapters
{
    public interface IExceptionAdapter
    {
        HttpResponseMessage GetResponseMessage(HttpRequestMessage request);
    }
}
