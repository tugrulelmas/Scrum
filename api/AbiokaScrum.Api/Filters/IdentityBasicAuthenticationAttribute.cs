using AbiokaScrum.Actions;
using AbiokaScrum.Authentication;
using System;
using System.Net.Http;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;

namespace AbiokaScrum.Filters
{
    public class IdentityBasicAuthenticationAttribute : BasicAuthenticationAttribute
    {
        protected override async Task<IPrincipal> AuthenticateAsync(string token, HttpRequestMessage request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested(); // Unfortunately, UserManager doesn't support CancellationTokens.
            CustomPrincipal user = null;
            await Task.Run(() => { user = UserManager.GetUser(token); });

            if (user == null)
                AuthenticationFailureResult.CreateInvalidCredentialsResult(request);

            if (user.TokenExpirationDate.CompareTo(DateTime.Now) < 0)
            {
                AuthenticationFailureResult.CreateTokenExpiredResult(request);
            }

            return user;
        }
    }
}
