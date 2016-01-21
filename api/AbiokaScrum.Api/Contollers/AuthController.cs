using AbiokaScrum.Api.Authentication;
using AbiokaScrum.Api.Entities;
using AbiokaScrum.Api.Entitites.Validation;
using AbiokaScrum.Api.Exceptions;
using AbiokaScrum.Api.Helper;
using AbiokaScrum.Api.Service;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public HttpResponseMessage Token([FromBody]UserInfo userInfo)
        {
            if (userInfo == null)
            {
                throw new ArgumentNullException("userInfo");
            }

            IAuthProviderValidator authProviderValidator = AuthProviderValidatorFactory.GetAuthProviderValidator(userInfo.Provider);
            var isValid = authProviderValidator.IsValid(userInfo.Email, userInfo.ProviderToken);

            if (!isValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ErrorMessage.InvalidToken);
            }

            var token = AbiokaToken.Encode(userInfo);
            //TODO: add this token to db.
            return Request.CreateResponse(HttpStatusCode.OK, token);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("validate")]
        public HttpResponseMessage Validate([FromUri]string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                throw new ArgumentNullException("token");
            }

            var tokenPayload = AbiokaToken.Decode(token);
            var user = DBService.GetByKey<User>(tokenPayload.email);
            if (user.Token != token)
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized, "InvalidToken");
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
