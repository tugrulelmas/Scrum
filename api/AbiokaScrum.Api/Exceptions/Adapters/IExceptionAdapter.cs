using System.Net.Http;

namespace AbiokaScrum.Exceptions.Adapters
{
    public interface IExceptionAdapter
    {
        HttpResponseMessage GetResponseMessage(HttpRequestMessage request);
    }
}
