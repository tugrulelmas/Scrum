using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AbiokaScrum.Api.Exceptions.Adapters
{
    public class ExceptionAdapter : IExceptionAdapter
    {
        private Exception exception;

        public ExceptionAdapter(Exception exception) {
            this.exception = exception;
        }

        public HttpResponseMessage GetResponseMessage(HttpRequestMessage request) {
            var response = request.CreateResponse(HttpStatusCode.InternalServerError, exception.ToString());
            return response;
        }
    }
}
