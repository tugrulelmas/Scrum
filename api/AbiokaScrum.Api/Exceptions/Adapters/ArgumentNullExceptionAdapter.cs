using System;
using System.Net;
using System.Net.Http;
using System.Text;

namespace AbiokaScrum.Api.Exceptions.Adapters
{
    public class ArgumentNullExceptionAdapter : IExceptionAdapter
    {
        private ArgumentNullException exception;

        public ArgumentNullExceptionAdapter(ArgumentNullException exception) {
            this.exception = exception;
        }

        public HttpResponseMessage GetResponseMessage(HttpRequestMessage request) {
            var response = request.CreateResponse(HttpStatusCode.BadRequest, exception.Message);
            return response;
        }
    }
}
