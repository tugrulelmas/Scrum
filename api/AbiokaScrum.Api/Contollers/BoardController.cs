using AbiokaScrum.Api.Data;
using AbiokaScrum.Api.Entities;
using AbiokaScrum.Api.Exceptions;
using AbiokaScrum.Api.Helper;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AbiokaScrum.Api.Contollers
{
    [RoutePrefix("api/Board")]
    public class BoardController : BaseRepositoryController<Board>
    {
        private readonly IBoardOperation boardOperation;
        private readonly IListOperation listOperation;

        public BoardController(IBoardOperation boardoperation, IListOperation listOperation)
            : base(boardoperation) {
            this.boardOperation = boardoperation;
            this.listOperation = listOperation;
        }

        public override HttpResponseMessage Get() {
            var result = boardOperation.GetAll();
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        public override HttpResponseMessage Get([FromUri] Guid id) {
            var result = boardOperation.Get(id);
            if (result == null)
            {
                throw new DenialException(HttpStatusCode.NotFound, ErrorMessage.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpPost]
        [Route("Add")]
        public override HttpResponseMessage Add(Board entity) {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            boardOperation.Add(entity);

            var response = Request.CreateResponse(HttpStatusCode.Created, entity);
            response.Headers.Location = new Uri(Request.RequestUri, entity.Id.ToString());
            return response;
        }

        [HttpGet]
        [Route("{boardId}/User")]
        public HttpResponseMessage GetUser([FromUri] Guid boardId) {
            var users = boardOperation.GetBoardUsers(boardId);

            var response = Request.CreateResponse(HttpStatusCode.Created, users);
            return response;
        }

        [HttpPost]
        [Route("{boardId}/User/{userId}")]
        public HttpResponseMessage AddUser([FromUri] Guid boardId, [FromUri]Guid userId) {
            var boardUser = boardOperation.AddUser(boardId, userId);
            return Request.CreateResponse(HttpStatusCode.Created, boardUser);
        }

        [HttpDelete]
        [Route("{boardId}/User/{userId}")]
        public HttpResponseMessage DeleteUser([FromUri] Guid boardId, [FromUri]Guid userId) {
            if (userId == CurrentUser.Id)
            {
                throw new DenialException(ErrorMessage.YouCannotRemoveYourselfFromBoard);
            }

            boardOperation.RemoveUser(boardId, userId);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpGet]
        [Route("{boardId}/List")]
        public HttpResponseMessage GetLists([FromUri] Guid boardId) {
            var result = listOperation.GetByBoardId(boardId);

            var response = Request.CreateResponse(HttpStatusCode.OK, result);
            return response;
        }
    }
}
