using AbiokaScrum.Api.Entities;
using AbiokaScrum.Api.Entities.DTO;
using AbiokaScrum.Api.Exceptions;
using AbiokaScrum.Api.Helper;
using DapperExtensions;
using System;
using System.Collections.Generic;

namespace AbiokaScrum.Api.Data.Transactional.Dapper.Operations
{
    public class CardOperation : Operation<Card>, ICardOperation
    {
        private readonly IUserOperation userOperation;
        private readonly IOperation<Comment> commentOperation;
        private readonly IOperation<CardLabel> cardLabelOperation;
        private readonly IOperation<CardUser> cardUserOperation;

        public CardOperation(IUserOperation userOperation, IOperation<Comment> commentOperation, 
            IOperation<CardLabel> cardLabelOperation, IOperation<CardUser> cardUserOperation) {
            this.userOperation = userOperation;
            this.commentOperation = commentOperation;
            this.cardLabelOperation = cardLabelOperation;
            this.cardUserOperation = cardUserOperation;
        }

        public Comment AddComment(Guid cardId, Comment comment) {
            comment.CardId = cardId;
            commentOperation.Add(comment);
            return comment;
        }

        public CardLabel AddLabel(Guid cardId, Guid labelId) {
            var cardLabel = new CardLabel
            {
                CardId = cardId,
                LabelId = labelId
            };
            cardLabelOperation.Add(cardLabel);
            return cardLabel;
        }

        public CardUser AddUser(Guid cardId, Guid userId) {
            var cardUser = new CardUser
            {
                CardId = cardId,
                UserId = userId
            };
            cardUserOperation.Add(cardUser);
            return cardUser;
        }

        public IEnumerable<Comment> GetComments(Guid cardId) {
            var predicate = Predicates.Field<Comment>(l => l.CardId, Operator.Eq, cardId);
            IList<ISort> sort = new List<ISort>
            {
                Predicates.Sort<Comment>(b => b.CreateDate)
            };
            var comments = ((Operation<Comment>)commentOperation).GetBy(predicate, sort);
            foreach (var commentItem in comments)
            {
                commentItem.User = userOperation.GetByKey(commentItem.UserId).ToDTO();
            }
            return comments;
        }

        public void RemoveComment(Guid cardId, Guid commentId, Guid userId) {
            var comment = commentOperation.GetByKey(commentId);
            if (comment == null)
            {
                throw new DenialException(ErrorMessage.NotFound);
            }
            if (comment.UserId != userId)
            {
                throw new DenialException(ErrorMessage.AccessDenied);
            }
            commentOperation.Remove(comment);
        }

        public void RemoveLabel(Guid cardId, Guid labelId) {
            var cardLabel = new CardLabel
            {
                CardId = cardId,
                LabelId = labelId
            };
            cardLabelOperation.Remove(cardLabel);
        }

        public void RemoveUser(Guid cardId, Guid userId) {
            var cardUser = new CardUser
            {
                CardId = cardId,
                UserId = userId
            };
            cardUserOperation.Remove(cardUser);
        }

        public void SetOrders(MoveCardRequest moveCardRequest) {
            Execute(repository =>
            {
                var card = repository.GetByKey<Card>(moveCardRequest.CardId);
                var firstListId = card.ListId;

                //update current card
                card.Order = moveCardRequest.NewIndex;
                card.ListId = moveCardRequest.NewListId;
                repository.Update(card);

                //update cards of first list
                IPredicateGroup pg = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };
                pg.Predicates.Add(Predicates.Field<Card>(c => c.ListId, Operator.Eq, firstListId));
                pg.Predicates.Add(Predicates.Field<Card>(c => c.Order, Operator.Gt, moveCardRequest.CurrentIndex));
                pg.Predicates.Add(Predicates.Field<Card>(c => c.Id, Operator.Eq, card.Id, true));
                var cardsOfFirstList = repository.GetBy<Card>(pg);
                foreach (var cardItem in cardsOfFirstList)
                {
                    cardItem.Order--;
                    repository.Update(cardItem);
                }

                //update cards of new list
                pg = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };
                pg.Predicates.Add(Predicates.Field<Card>(c => c.ListId, Operator.Eq, moveCardRequest.NewListId));
                pg.Predicates.Add(Predicates.Field<Card>(c => c.Order, Operator.Ge, moveCardRequest.NewIndex));
                pg.Predicates.Add(Predicates.Field<Card>(c => c.Id, Operator.Eq, card.Id, true));
                var cardsOfSecondList = repository.GetBy<Card>(pg);
                foreach (var cardItem in cardsOfSecondList)
                {
                    cardItem.Order++;
                    repository.Update(cardItem);
                }
            });
        }
    }
}