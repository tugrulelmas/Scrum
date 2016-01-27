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
    public class BoardController : BaseRepositoryController<Board>
    {
        public override HttpResponseMessage Get() {
            var result = BoardService.GetAll();
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        public override HttpResponseMessage Get([FromUri] Guid id) {
            var result = BoardService.Get(id);
            if (result == null) {
                throw new DenialException(HttpStatusCode.NotFound, ErrorMessage.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpPost]
        [Route("Add")]
        public override HttpResponseMessage Add(Board entity) {
            if (entity == null) {
                throw new ArgumentNullException("entity");
            }

            BoardService.Add(entity);

            var response = Request.CreateResponse(HttpStatusCode.Created, entity);
            response.Headers.Location = new Uri(Request.RequestUri, entity.Id.ToString());
            return response;
        }

        public override HttpResponseMessage Delete(Guid id) {
            BoardService.Delete(id);

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpPost]
        [Route("{boardId}/User/{userId}")]
        public HttpResponseMessage AddUser([FromUri] Guid boardId, [FromUri]Guid userId) {
            var boardUser = new BoardUser
            {
                BoardId = boardId,
                UserId = userId
            };
            DBService.Add(boardUser);

            var response = Request.CreateResponse(HttpStatusCode.Created, boardUser);
            return response;
        }

        [HttpDelete]
        [Route("{boardId}/User/{userId}")]
        public HttpResponseMessage DeleteUser([FromUri] Guid boardId, [FromUri]Guid userId) {
            if (userId == CurrentUser.Id) {
                throw new DenialException(ErrorMessage.YouCannotRemoveYourselfFromBoard);
            }
            var boardUser = new BoardUser
            {
                BoardId = boardId,
                UserId = userId
            };
            DBService.Remove(boardUser);

            var response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
        }

        [HttpGet]
        [Route("{boardId}/List")]
        public HttpResponseMessage GetLists([FromUri] Guid boardId) {
            var result = ListService.GetByBoardId(boardId);

            var response = Request.CreateResponse(HttpStatusCode.OK, result);
            return response;
        }
    }
}
