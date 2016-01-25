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
        public HttpResponseMessage AddUser([FromUri] Guid boardId, [FromUri]Guid userId) {
            //TODO: add to db

            var response = Request.CreateResponse(HttpStatusCode.Created, "Ok");
            return response;
        }

        [HttpDelete]
        [Route("{boardId}/DeleteUser")]
        public HttpResponseMessage DeleteUser([FromUri] Guid boardId, [FromUri]Guid userId) {
            if (userId == CurrentUser.Id) {
                throw new ValidationException(ErrorMessage.YouCannotRemoveYourselfFromBoard);
            }

            //TODO: delete from db

            var response = Request.CreateResponse(HttpStatusCode.OK, "Ok");
            return response;
        }
    }
}
