using AbiokaScrum.Actions;
using AbiokaScrum.Api.Authentication;
using AbiokaScrum.Authentication;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Filters;

namespace AbiokaScrum.Filters
{
    public abstract class BasicAuthenticationAttribute : Attribute, IAuthenticationFilter
    {
        private const string abiokaToken = "ABIOKA-TOKEN";

        public string Realm { get; set; }

        public async Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken) {
            var actionAttributes = context.ActionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>(true);
            var controllerAttributes = context.ActionContext.ControllerContext.ControllerDescriptor.GetCustomAttributes<AllowAnonymousAttribute>(true);
            if ((actionAttributes != null && actionAttributes.Count > 0) || (controllerAttributes != null && controllerAttributes.Count > 0)) {
                return;
            }

            HttpRequestMessage request = context.Request;
            IEnumerable<string> tokens;
            if (!request.Headers.TryGetValues(abiokaToken, out tokens)) {
                context.ErrorResult = AuthenticationFailureResult.CreateMissingCredentialsResult(request);
                return;
            }

            if (tokens == null) {
                context.ErrorResult = AuthenticationFailureResult.CreateMissingCredentialsResult(request);
                return;
            }

            if (tokens.Count() != 1) {
                context.ErrorResult = AuthenticationFailureResult.CreateInvalidCredentialsResult(request);
                return;
            }

            var userInfo = ExtractUserNameAndPassword(tokens.First());

            if (userInfo == null) {
                // Authentication was attempted but failed. Set ErrorResult to indicate an error.
                context.ErrorResult = AuthenticationFailureResult.CreateInvalidCredentialsResult(request);
                return;
            }

            IPrincipal principal = await AuthenticateAsync(userInfo, cancellationToken);

            if (principal == null) {
                // Authentication was attempted but failed. Set ErrorResult to indicate an error.
                context.ErrorResult = AuthenticationFailureResult.CreateInvalidUsrPwdResult(request);
            } else {
                // Authentication was attempted and succeeded. Set Principal to the authenticated user.
                context.Principal = principal;
                var abiokaContext = new Context();
                abiokaContext.Principal = (ICustomPrincipal)principal;
                CultureInfo.DefaultThreadCurrentCulture = CultureInfo.DefaultThreadCurrentUICulture = abiokaContext.Principal.CultureInfo;
            }
        }

        protected abstract Task<IPrincipal> AuthenticateAsync(UserInfo userInfo,
            CancellationToken cancellationToken);

        private static UserInfo ExtractUserNameAndPassword(string authorizationParameter) {
            byte[] data = Convert.FromBase64String(authorizationParameter);
            string userInfoText = Encoding.UTF8.GetString(data);
            var userInfo = JsonConvert.DeserializeObject<UserInfo>(userInfoText);
            return userInfo;
        }

        public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken) {
            Challenge(context);
            return Task.FromResult(0);
        }

        private void Challenge(HttpAuthenticationChallengeContext context) {
            string parameter;

            if (String.IsNullOrEmpty(Realm)) {
                parameter = null;
            } else {
                // A correct implementation should verify that Realm does not contain a quote character unless properly
                // escaped (precededed by a backslash that is not itself escaped).
                parameter = "realm=\"" + Realm + "\"";
            }

            context.ChallengeWith(abiokaToken, parameter);
        }

        public virtual bool AllowMultiple {
            get { return false; }
        }
    }
}
