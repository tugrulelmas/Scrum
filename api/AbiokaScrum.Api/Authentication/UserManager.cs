using AbiokaScrum.Api.Authentication;
using AbiokaScrum.Api.Helper;
using System.Globalization;


namespace AbiokaScrum.Authentication
{
    public class UserManager
    {
        public static CustomPrincipal GetUser(string token)
        {
            var payload = AbiokaToken.Decode(token);

            var user = new CustomPrincipal(payload.email)
            {
                Token = token,
                UserName = payload.email,
                Email = payload.email,
                TokenExpirationDate = Util.UnixTimeStampToDateTime(payload.exp)
            };

            return user;
        }
    }
}
