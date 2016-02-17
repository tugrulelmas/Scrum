using AbiokaScrum.Api.Entities;
using AbiokaScrum.Api.Entities.DTO;
using System;
using System.Collections.Generic;

namespace AbiokaScrum.Api.Data
{
    public interface ICardOperation : IOperation<Card>
    {
        IEnumerable<Comment> GetComments(Guid cardId);

        void SetOrders(MoveCardRequest moveCardRequest);

        CardUser AddUser(Guid cardId, Guid userId);

        void RemoveUser(Guid cardId, Guid userId);

        CardLabel AddLabel(Guid cardId, Guid labelId);

        void RemoveLabel(Guid cardId, Guid labelId);

        Comment AddComment(Guid cardId, Comment comment);

        void RemoveComment(Guid cardId, Guid commentId, Guid userId);
    }
}