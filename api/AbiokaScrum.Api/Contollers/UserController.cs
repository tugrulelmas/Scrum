﻿using AbiokaScrum.Api.Authentication;
using AbiokaScrum.Api.Caches;
using AbiokaScrum.Api.Entities;
using AbiokaScrum.Api.Helper;
using AbiokaScrum.Api.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace AbiokaScrum.Api.Contollers
{
    [RoutePrefix("api/User")]
    public class UserController : BaseDeletableRepositoryController<User>
    {
        public override HttpResponseMessage Get()
        {
            var users = DBService.Get<User>().ToList();
            return Request.CreateResponse(HttpStatusCode.OK, users);
        }


        [Route("Params")]
        public HttpResponseMessage Get([FromUri]bool loadAllUsers)
        {
            var users = DBService.Get<User>().ToList();
            if (!loadAllUsers)
            {
                users.Remove(users.FirstOrDefault(u => u.Email == CurrentUser.Email));
            }
            return Request.CreateResponse(HttpStatusCode.OK, users);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public HttpResponseMessage Login([FromBody]UserPassword userPassword)
        {
            if (userPassword == null)
            {
                throw new ArgumentNullException("user");
            }

            var dbUser = DBService.GetByKey<User>(userPassword.Email);
            if (dbUser == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, ErrorMessage.UserNotFound);
            }

            if(dbUser.Password != userPassword.Password)
            {
                //TODO: uncomment
                //return Request.CreateResponse(HttpStatusCode.NotFound, ErrorMessage.InvalidPassword);
            }
            
            var localToken = Guid.NewGuid().ToString();
            //TODO: delete below row
            localToken = dbUser.Token;
            var userInfo = new UserInfo
            {
                Email = userPassword.Email,
                Name = dbUser.Name,
                Provider = AuthProvider.Local,
                ProviderToken = localToken
            };
            //TODO: Add the local token to db.
            
            return Request.CreateResponse(HttpStatusCode.OK, userInfo);
        }
    }
}
