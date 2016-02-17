using AbiokaScrum.Api.Data;
using AbiokaScrum.Api.Entities;
using AbiokaScrum.Api.Entities.DTO;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AbiokaScrum.Api.Contollers
{
    [RoutePrefix("api/Card")]
    public class CardController : BaseRepositoryController<Card>
    {
        private readonly ICardOperation cardOperation;

        public CardController(ICardOperation cardOperation)
            : base(cardOperation) {
            this.cardOperation = cardOperation;
        }

        [HttpPost]
        [Route("{cardId}/User/{userId}")]
        public HttpResponseMessage AddUser([FromUri] Guid cardId, [FromUri]Guid userId) {
            var cardUser = cardOperation.AddUser(cardId, userId);
            return Request.CreateResponse(HttpStatusCode.Created, cardUser);
        }

        [HttpDelete]
        [Route("{cardId}/User/{userId}")]
        public HttpResponseMessage DeleteUser([FromUri] Guid cardId, [FromUri]Guid userId) {
            cardOperation.RemoveUser(cardId, userId);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpPost]
        [Route("{cardId}/Label/{labelId}")]
        public HttpResponseMessage AddLabel([FromUri] Guid cardId, [FromUri]Guid labelId) {
            var cardLabel = cardOperation.AddLabel(cardId, labelId);
            return Request.CreateResponse(HttpStatusCode.Created, cardLabel);
        }

        [HttpDelete]
        [Route("{cardId}/Label/{labelId}")]
        public HttpResponseMessage DeleteLabel([FromUri] Guid cardId, [FromUri]Guid labelId) {
            cardOperation.RemoveLabel(cardId, labelId);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpGet]
        [Route("{cardId}/Comment")]
        public HttpResponseMessage GetComments([FromUri] Guid cardId) {
            var result = cardOperation.GetComments(cardId);
            return Request.CreateResponse(HttpStatusCode.Created, result);
        }

        [HttpPost]
        [Route("{cardId}/Comment")]
        public HttpResponseMessage AddComment([FromUri] Guid cardId, [FromBody]Comment comment) {
            comment.UserId = CurrentUser.Id;
            var result = cardOperation.AddComment(cardId, comment);
            return Request.CreateResponse(HttpStatusCode.Created, result);
        }

        [HttpDelete]
        [Route("{cardId}/Comment/{commentId}")]
        public HttpResponseMessage DeleteComment([FromUri] Guid cardId, [FromUri]Guid commentId) {
            cardOperation.RemoveComment(cardId, commentId, CurrentUser.Id);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpPost]
        [Route("Move")]
        public HttpResponseMessage Move([FromBody] MoveCardRequest moveCardRequest) {
            if (moveCardRequest == null)
                throw new ArgumentNullException(nameof(moveCardRequest));

            cardOperation.SetOrders(moveCardRequest);

            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
