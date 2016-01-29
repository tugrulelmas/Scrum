using AbiokaScrum.Api.Entities;
using AbiokaScrum.Api.Entities.DTO;
using AbiokaScrum.Api.Exceptions;
using AbiokaScrum.Api.Helper;
using AbiokaScrum.Api.Service;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AbiokaScrum.Api.Contollers
{
    [RoutePrefix("api/Card")]
    public class CardController : BaseRepositoryController<Card>
    {
        [HttpPost]
        [Route("{cardId}/User/{userId}")]
        public HttpResponseMessage AddUser([FromUri] Guid cardId, [FromUri]Guid userId) {
            var cardUser = new CardUser
            {
                CardId = cardId,
                UserId = userId
            };
            DBService.Add(cardUser);

            return Request.CreateResponse(HttpStatusCode.Created, cardUser);
        }

        [HttpDelete]
        [Route("{cardId}/User/{userId}")]
        public HttpResponseMessage DeleteUser([FromUri] Guid cardId, [FromUri]Guid userId) {
            var cardUser = new CardUser
            {
                CardId = cardId,
                UserId = userId
            };
            DBService.Remove(cardUser);

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpPost]
        [Route("{cardId}/Label/{labelId}")]
        public HttpResponseMessage AddLabel([FromUri] Guid cardId, [FromUri]Guid labelId) {
            var cardLabel = new CardLabel
            {
                CardId = cardId,
                LabelId = labelId
            };
            DBService.Add(cardLabel);

            return Request.CreateResponse(HttpStatusCode.Created, cardLabel);
        }

        [HttpDelete]
        [Route("{cardId}/Label/{labelId}")]
        public HttpResponseMessage DeleteLabel([FromUri] Guid cardId, [FromUri]Guid labelId) {
            var cardLabel = new CardLabel
            {
                CardId = cardId,
                LabelId = labelId
            };
            DBService.Remove(cardLabel);

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpGet]
        [Route("{cardId}/Comment")]
        public HttpResponseMessage GetComments([FromUri] Guid cardId) {
            var result = CardService.GetComments(cardId);

            return Request.CreateResponse(HttpStatusCode.Created, result);
        }

        [HttpPost]
        [Route("{cardId}/Comment")]
        public HttpResponseMessage AddComment([FromUri] Guid cardId, [FromBody]Comment comment) {
            comment.CardId = cardId;
            comment.UserId = CurrentUser.Id;
            DBService.Add(comment);

            return Request.CreateResponse(HttpStatusCode.Created, comment);
        }

        [HttpDelete]
        [Route("{cardId}/Comment/{commentId}")]
        public HttpResponseMessage DeleteComment([FromUri] Guid cardId, [FromUri]Guid commentId) {
            var comment = DBService.GetByKey<Comment>(commentId);
            if(comment == null) {
                throw new DenialException(ErrorMessage.NotFound);
            }
            if(comment.UserId != CurrentUser.Id) {
                throw new DenialException(ErrorMessage.AccessDenied);
            }
            DBService.Remove(comment);

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpPost]
        [Route("Move")]
        public HttpResponseMessage Move([FromBody] MoveCardRequest moveCardRequest) {
            if (moveCardRequest == null)
                throw new ArgumentNullException(nameof(moveCardRequest));

            CardService.SetOrders(moveCardRequest);

            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
