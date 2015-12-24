using System.Net.Http;

namespace AbiokaScrum.Exceptions.Adapters
{
    public class ApiExceptionAdapter : IExceptionAdapter
    {
        private IApiException apiException;

        public ApiExceptionAdapter(IApiException apiException) {
            this.apiException = apiException;
        }

        public HttpResponseMessage GetResponseMessage(HttpRequestMessage request) {
            var response = request.CreateResponse(apiException.StatusCode, apiException.ContentValue);
            if (apiException.ExtraHeaders != null) {
                foreach (var headerItem in apiException.ExtraHeaders) {
                    response.Headers.Add(headerItem.Key, headerItem.Value);
                }
            }
            return response;
        }
    }
}
