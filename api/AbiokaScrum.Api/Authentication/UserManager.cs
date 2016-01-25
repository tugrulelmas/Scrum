using AbiokaScrum.Api.Authentication;
using AbiokaScrum.Api.Helper;


namespace AbiokaScrum.Authentication
{
    public class UserManager
    {
        public static CustomPrincipal GetUser(string token)
        {
            var payload = AbiokaToken.Decode(token);

            var user = new CustomPrincipal(payload.id.ToString())
            {
                Token = token,
                UserName = payload.id.ToString(),
                Email = payload.email,
                Id = payload.id,
                TokenExpirationDate = Util.UnixTimeStampToDateTime(payload.exp)
            };

            return user;
        }
    }
}
