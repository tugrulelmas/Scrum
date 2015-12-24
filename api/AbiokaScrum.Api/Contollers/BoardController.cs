﻿using AbiokaScrum.Api.Entities;
using AbiokaScrum.Api.Service;
using AbiokaScrum.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AbiokaScrum.Api.Contollers
{
    [RoutePrefix("api/Board")]
    public class BoardController : BaseDeletableRepositoryController<Board>
    {
        [HttpPost]
        [Route("Add")]
        public override HttpResponseMessage Add(Board entity) {
            if (entity == null) {
                throw new ArgumentNullException("entity");
            }

            BoardService.Add(entity);

            var response = Request.CreateResponse(HttpStatusCode.Created, entity);
            return response;
        }

        public override HttpResponseMessage Delete(Board entity, string d) {
            if (entity == null) {
                throw new ArgumentNullException("entity");
            }

            BoardService.Delete(entity);

            var response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
        }

        [HttpPost]
        [Route("{boardId}/AddUser")]
        public HttpResponseMessage AddUser([FromUri] int boardId, [FromUri]string userEmail) {
            if (boardId <= 0) {
                throw new ArgumentNullException("boardId");
            }

            if (string.IsNullOrEmpty(userEmail)) {
                throw new ArgumentNullException("userEmail");
            }

            //TODO: add to db

            var response = Request.CreateResponse(HttpStatusCode.Created, "Ok");
            return response;
        }

        [HttpDelete]
        [Route("{boardId}/DeleteUser")]
        public HttpResponseMessage DeleteUser([FromUri] int boardId, [FromUri]string userEmail) {
            if (boardId <= 0) {
                throw new ArgumentNullException("boardId");
            }

            if (string.IsNullOrEmpty(userEmail)) {
                throw new ArgumentNullException("userEmail");
            }

            if (userEmail.ToLowerInvariant() == CurrentUser.Email.ToLowerInvariant()) {
                throw new GlobalException("You cannot remove yourself from a board");
            }

            //TODO: delete from db

            var response = Request.CreateResponse(HttpStatusCode.OK, "Ok");
            return response;
        }
    }
}