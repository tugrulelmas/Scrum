using System;
using System.Net;
using System.Net.Http;
using System.Text;

namespace AbiokaScrum.Api.Exceptions.Adapters
{
    public class AggregateExceptionAdapter : IExceptionAdapter
    {
        private AggregateException exception;

        public AggregateExceptionAdapter(AggregateException exception) {
            this.exception = exception;
        }

        public HttpResponseMessage GetResponseMessage(HttpRequestMessage request) {
            var exceptionMessage = new StringBuilder();
            foreach (var exceptionItem in exception.InnerExceptions) {
                exceptionMessage.AppendLine(exceptionItem.ToString());
            }

            var response = request.CreateResponse(HttpStatusCode.InternalServerError, exceptionMessage.ToString());
            return response;
        }
    }
}
