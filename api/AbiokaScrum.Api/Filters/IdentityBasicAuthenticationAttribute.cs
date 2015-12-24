using AbiokaScrum.Authentication;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;

namespace AbiokaScrum.Filters
{
    public class IdentityBasicAuthenticationAttribute : BasicAuthenticationAttribute
    {
        protected override async Task<IPrincipal> AuthenticateAsync(UserInfo userInfo, CancellationToken cancellationToken) {
            cancellationToken.ThrowIfCancellationRequested(); // Unfortunately, UserManager doesn't support CancellationTokens.
            CustomPrincipal user = null;
            await Task.Run(() => { user = UserManager.GetUser(userInfo); });

            if (user == null) {
                // No user with userName/password exists.
                return null;
            }

            return user;
        }
    }
}
