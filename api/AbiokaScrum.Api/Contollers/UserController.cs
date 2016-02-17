using AbiokaScrum.Api.Data;
using AbiokaScrum.Api.Data.Transactional;
using AbiokaScrum.Api.Entities;
using AbiokaScrum.Api.Entities.DTO;
using AbiokaScrum.Api.Exceptions;
using AbiokaScrum.Api.Helper;
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
        private readonly IUserOperation userOperation;

        public UserController(IUserOperation userOperation){
            this.userOperation = userOperation;
        }

        [Route("")]
        public HttpResponseMessage Get() {
            var users = userOperation.Get().ToDTO();
            return Request.CreateResponse(HttpStatusCode.OK, users);
        }


        [Route("Params")]
        public HttpResponseMessage Get([FromUri]bool loadAllUsers) {
            var users = userOperation.Get().ToList();
            if (!loadAllUsers)
            {
                users.Remove(users.FirstOrDefault(u => u.Id == CurrentUser.Id));
            }
            var result = users.ToDTO();
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public HttpResponseMessage Login([FromBody]LoginRequest loginRequest) {
            if (loginRequest == null)
            {
                throw new ArgumentNullException(nameof(loginRequest));
            }

            var dbUser = userOperation.GetByEmail(loginRequest.Email);
            if (dbUser == null)
            {
                throw new DenialException(HttpStatusCode.NotFound, ErrorMessage.UserNotFound);
            }

            var hashedPassword = dbUser.GetHashedPassword(loginRequest.Password);
            if (dbUser.Password != hashedPassword)
            {
                throw new DenialException(ErrorMessage.InvalidPassword);
            }

            var localToken = Guid.NewGuid().ToString();
            var userInfo = new UserInfo
            {
                Email = loginRequest.Email,
                Id = dbUser.Id,
                Name = dbUser.Name,
                Provider = AuthProvider.Local,
                ProviderToken = localToken
            };
            dbUser.ProviderToken = localToken;
            if (!userOperation.Update(dbUser))
            {
                throw new ValidationException(ErrorMessage.PleaseTryAgain);
            }

            return Request.CreateResponse(HttpStatusCode.OK, userInfo);
        }


        [AllowAnonymous]
        [HttpPost]
        [Route("signup")]
        public HttpResponseMessage SignIn([FromBody]SignUpRequest signUpRequest) {
            if (signUpRequest == null)
            {
                throw new ArgumentNullException(nameof(signUpRequest));
            }

            var dbUser = userOperation.GetByEmail(signUpRequest.Email);
            if (dbUser != null)
            {
                throw new DenialException(ErrorMessage.UserAlreadyRegistered);
            }

            var user = new User
            {
                Name = signUpRequest.Name,
                Email = signUpRequest.Email.ToLowerInvariant(),
                AuthProvider = AuthProvider.Local,
                ProviderToken = Guid.NewGuid().ToString()
            };
            user.Password = user.GetHashedPassword(signUpRequest.Password);
            userOperation.Add(user);

            var result = new UserInfo
            {
                Email = user.Email,
                Id = user.Id,
                Name = user.Name,
                Provider = user.AuthProvider,
                ProviderToken = user.ProviderToken
            };

            return Request.CreateResponse(HttpStatusCode.Created, result);
        }

        [HttpPut]
        [Route("update")]
        public HttpResponseMessage Update([FromBody]UpdateUserRequest updateUserRequest) {
            if (updateUserRequest == null)
            {
                throw new ArgumentNullException(nameof(updateUserRequest));
            }

            var user = userOperation.GetByKey(updateUserRequest.Id);
            if (user == null)
                throw new DenialException(HttpStatusCode.NotFound, ErrorMessage.UserNotFound);

            user.Name = updateUserRequest.Name;
            user.Initials = updateUserRequest.Initials;

            userOperation.Update(user);

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpPut]
        [Route("ChangePassword")]
        public HttpResponseMessage ChangePassword([FromBody]ChangePasswordRequest changePasswordRequest) {
            if (changePasswordRequest == null)
            {
                throw new ArgumentNullException(nameof(changePasswordRequest));
            }

            var user = userOperation.GetByKey(changePasswordRequest.Id);
            if (user == null)
                throw new DenialException(HttpStatusCode.NotFound, ErrorMessage.UserNotFound);

            if (user.AuthProvider != AuthProvider.Local)
                throw new DenialException(ErrorMessage.ChangingPasswordIsNotAvailableForThisUser);

            var hashedPassword = user.GetHashedPassword(changePasswordRequest.OldPassword);
            if (user.Password != hashedPassword)
                throw new DenialException(ErrorMessage.InvalidPassword);

            user.Password = user.GetHashedPassword(changePasswordRequest.NewPassword);

            userOperation.Update(user);

            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
