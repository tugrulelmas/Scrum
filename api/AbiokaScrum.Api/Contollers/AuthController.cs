using AbiokaScrum.Api.Authentication;
using AbiokaScrum.Api.Entities;
using AbiokaScrum.Api.Exceptions;
using AbiokaScrum.Api.Helper;
using AbiokaScrum.Api.Service;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AbiokaScrum.Api.Contollers
{
    [RoutePrefix("api/Auth")]
    public class AuthController : BaseApiController
    {
        [AllowAnonymous]
        [HttpPost]
        [Route("token")]
        public HttpResponseMessage Token([FromBody]UserInfo userInfo) {
            if (userInfo == null) {
                throw new ArgumentNullException("userInfo");
            }

            IAuthProviderValidator authProviderValidator = AuthProviderValidatorFactory.GetAuthProviderValidator(userInfo.Provider);
            var isValid = authProviderValidator.IsValid(userInfo.Id, userInfo.Email, userInfo.ProviderToken);

            if (!isValid) {
                throw new DenialException(ErrorMessage.InvalidToken);
            }

            var dbUser = UserService.GetByEmail(userInfo.Email);
            if (dbUser == null) {
                dbUser = new User
                {
                    Email = userInfo.Email,
                    ImageUrl = userInfo.ImageUrl,
                    Name = userInfo.Name,
                    ProviderToken = userInfo.ProviderToken,
                    AuthProvider = userInfo.Provider
                };
                DBService.Add(dbUser);
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
