using AbiokaScrum.Api.Authentication;
using AbiokaScrum.Api.Entities;
using AbiokaScrum.Api.Entities.DTO;
using AbiokaScrum.Api.Exceptions;
using AbiokaScrum.Api.Helper;
using AbiokaScrum.Api.Service;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace AbiokaScrum.Api.Contollers
{
    [RoutePrefix("api/Auth")]
    public class AuthController : BaseApiController
    {
        [AllowAnonymous]
        [HttpPost]
        [Route("token")]
        public HttpResponseMessage Token([FromBody]TokenRequest tokenRequest) {
            if (tokenRequest == null) {
                throw new ArgumentNullException(nameof(tokenRequest));
            }

            IAuthProviderValidator authProviderValidator = AuthProviderValidatorFactory.GetAuthProviderValidator(tokenRequest.Provider);
            var isValid = authProviderValidator.IsValid(tokenRequest.Email, tokenRequest.ProviderToken);

            if (!isValid) {
                throw new DenialException(ErrorMessage.InvalidToken);
            }

            var userInfo = new UserInfo();
            var dbUser = UserService.GetByEmail(tokenRequest.Email);
            if (dbUser == null) {
                var initals = string.Empty;
                if (!string.IsNullOrWhiteSpace(tokenRequest.Name)) {
                    string[] names = tokenRequest.Name.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    var result = new StringBuilder();
                    foreach (var nameItem in names) {
                        result.Append(nameItem.First().ToString().ToUpper());
                    }
                    initals = result.ToString();
                }
                dbUser = new User
                {
                    Email = tokenRequest.Email,
                    ImageUrl = tokenRequest.ImageUrl,
                    Name = tokenRequest.Name,
                    Initials = initals,
                    ProviderToken = tokenRequest.ProviderToken,
                    AuthProvider = tokenRequest.Provider
                };
                DBService.Add(dbUser);

                userInfo = tokenRequest.ToUserInfo();
                userInfo.Initials = initals;
            } else {
                userInfo.Email = dbUser.Email;
                userInfo.ImageUrl = dbUser.ImageUrl;
                userInfo.Name = dbUser.Name;
                userInfo.Initials = dbUser.Initials;
                userInfo.ProviderToken = dbUser.ProviderToken;
                userInfo.Provider = dbUser.AuthProvider;
            }

            userInfo.Id = dbUser.Id;
            var token = AbiokaToken.Encode(userInfo);
            dbUser.Token = token;
            DBService.Update(dbUser);

            return Request.CreateResponse(HttpStatusCode.OK, token);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("validate")]
        public HttpResponseMessage Validate([FromUri]string token) {
            if (string.IsNullOrEmpty(token)) {
                throw new ArgumentNullException("token");
            }

            var tokenPayload = AbiokaToken.Decode(token);
            var user = DBService.GetByKey<User>(tokenPayload.id);
            if (user.Token != token) {
                return Request.CreateResponse(HttpStatusCode.Unauthorized, ErrorMessage.InvalidToken);
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
