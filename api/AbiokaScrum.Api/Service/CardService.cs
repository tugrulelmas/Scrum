using AbiokaScrum.Api.Entities;
using AbiokaScrum.Api.Entities.DTO;
using DapperExtensions;
using System;
using System.Collections.Generic;

namespace AbiokaScrum.Api.Service
{
    public class CardService
    {
        public static IEnumerable<Comment> GetComments(Guid cardId) {
            var predicate = Predicates.Field<Comment>(l => l.CardId, Operator.Eq, cardId);
            var comments = DBService.GetBy<Comment>(predicate);
            foreach (var commentItem in comments) {
                commentItem.User = DBService.GetByKey<User>(commentItem.UserId).ToDTO();
            }
            return comments;
        }

        public static void SetOrders(MoveCardRequest moveCardRequest) {
            DBService.Execute(repository =>
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
                foreach (var cardItem in cardsOfFirstList) {
                    cardItem.Order--;
                    repository.Update(cardItem);
                }

                //update cards of new list
                pg = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };
                pg.Predicates.Add(Predicates.Field<Card>(c => c.ListId, Operator.Eq, moveCardRequest.NewListId));
                pg.Predicates.Add(Predicates.Field<Card>(c => c.Order, Operator.Ge, moveCardRequest.NewIndex));
                pg.Predicates.Add(Predicates.Field<Card>(c => c.Id, Operator.Eq, card.Id, true));
                var cardsOfSecondList = repository.GetBy<Card>(pg);
                foreach (var cardItem in cardsOfSecondList) {
                    cardItem.Order++;
                    repository.Update(cardItem);
                }
            });
        }
    }
}