using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace AbiokaScrum.Actions {
    public class AuthenticationFailureResult : IHttpActionResult {
        private AuthenticationFailureResult(string reasonPhrase, HttpRequestMessage request) {
            this.ReasonPhrase = reasonPhrase;
            this.Request = request;
        }

        public string ReasonPhrase { get; private set; }

        public HttpRequestMessage Request { get; private set; }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken) {
            return Task.FromResult(Execute());
        }

        private HttpResponseMessage Execute() {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
            response.RequestMessage = Request;
            response.ReasonPhrase = ReasonPhrase;
            return response;
        }

        public static AuthenticationFailureResult CreateMissingCredentialsResult(HttpRequestMessage request) {
            return new AuthenticationFailureResult("Missing credentials", request);
        }

        public static AuthenticationFailureResult CreateInvalidCredentialsResult(HttpRequestMessage request) {
            return new AuthenticationFailureResult("Invalid credentials", request);
        }

        public static AuthenticationFailureResult CreateInvalidUsrPwdResult(HttpRequestMessage request) {
            return new AuthenticationFailureResult("Invalid username or password", request);
        }

        public static AuthenticationFailureResult CreateTokenExpiredResult(HttpRequestMessage request)
        {
            return new AuthenticationFailureResult("Token is expired", request);
        }
    }
}
