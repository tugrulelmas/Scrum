using AbiokaScrum.Api.Entities;
using AbiokaScrum.Api.Exceptions;
using AbiokaScrum.Api.Helper;
using AbiokaScrum.Api.Service;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AbiokaScrum.Api.Contollers
{
    [RoutePrefix("api/User")]
    public class UserController : BaseApiController
    {
        [Route("")]
        public HttpResponseMessage Get() {
            var users = DBService.Get<User>().ToDTO();
            return Request.CreateResponse(HttpStatusCode.OK, users);
        }


        [Route("Params")]
        public HttpResponseMessage Get([FromUri]bool loadAllUsers) {
            var users = DBService.Get<User>().ToList();
            if (!loadAllUsers) {
                users.Remove(users.FirstOrDefault(u => u.Id == CurrentUser.Id));
            }
            var result = users.ToDTO();
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public HttpResponseMessage Login([FromBody]UserPassword userPassword) {
            if (userPassword == null) {
                throw new ArgumentNullException("user");
            }

            var dbUser = UserService.GetByEmail(userPassword.Email);
            if (dbUser == null) {
                throw new DenialException(HttpStatusCode.NotFound, ErrorMessage.UserNotFound);
            }

            if (dbUser.Password != userPassword.Password) {
                throw new DenialException(ErrorMessage.InvalidPassword);
            }

            var localToken = Guid.NewGuid().ToString();
            var userInfo = new UserInfo
            {
                Email = userPassword.Email,
                Id = dbUser.Id,
                Name = dbUser.Name,
                Provider = AuthProvider.Local,
                ProviderToken = localToken
            };
            dbUser.ProviderToken = localToken;
            if (!DBService.Update(dbUser)) {
                throw new ValidationException(ErrorMessage.PleaseTryAgain);
            }

            return Request.CreateResponse(HttpStatusCode.OK, userInfo);
        }
    }
}
