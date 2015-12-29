using AbiokaScrum.Api.Caches;
using AbiokaScrum.Api.Entities;
using AbiokaScrum.Api.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AbiokaScrum.Api.Contollers
{
    [RoutePrefix("api/User")]
    public class UserController : BaseDeletableRepositoryController<User>
    {
        public override HttpResponseMessage Get() {
            var users = DBService.Get<User>().ToList();
            return Request.CreateResponse(HttpStatusCode.OK, users);
        }


        [Route("Params")]
        public HttpResponseMessage Get([FromUri]bool loadAllUsers) {
            var users = DBService.Get<User>().ToList();
            if (!loadAllUsers) {
                users.Remove(users.FirstOrDefault(u => u.Email == CurrentUser.Email));
            }
            return Request.CreateResponse(HttpStatusCode.OK, users);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public HttpResponseMessage Login([FromBody]User user) {
            if (user == null) {
                throw new ArgumentNullException("user");
            }

            //TODO: add user to db.
            UserCache.AddUser(user);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
