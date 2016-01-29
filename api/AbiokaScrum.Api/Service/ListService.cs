using AbiokaScrum.Api.Entities;
using AbiokaScrum.Api.Entities.DTO;
using DapperExtensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AbiokaScrum.Api.Service
{
    public class ListService
    {
        public static IEnumerable<List> GetByBoardId(Guid boardId) {
            var predicate = Predicates.Field<List>(l => l.BoardId, Operator.Eq, boardId);
            var list = DBService.GetBy<List>(predicate);
            foreach (var listItem in list) {
                var sort = new List<ISort>
                {
                    Predicates.Sort<Card>(b => b.Order)
                };
                listItem.Cards = DBService.GetBy<Card>(Predicates.Field<Card>(c => c.ListId, Operator.Eq, listItem.Id), sort);
                foreach (var cardItem in listItem.Cards) {
                    cardItem.Users = GetUsers(cardItem);
                    cardItem.Labels = GetLabels(cardItem);
                }
            }
            return list;
        }

        private static IEnumerable<UserDTO> GetUsers(Card card) {
            var userIds = DBService.GetBy<CardUser>(Predicates.Field<CardUser>(c => c.CardId, Operator.Eq, card.Id)).Select(u => u.UserId);
            if (userIds == null || userIds.Count() == 0) {
                return new List<UserDTO>();
            }

            var pg = new PredicateGroup { Operator = GroupOperator.Or, Predicates = new List<IPredicate>() };
            foreach (var userIdItem in userIds) {
                pg.Predicates.Add(Predicates.Field<User>(b => b.Id, Operator.Eq, userIdItem));
            }
            return DBService.GetBy<User>(pg).ToDTO();
        }

        private static IEnumerable<Label> GetLabels(Card card) {
            var labelIds = DBService.GetBy<CardLabel>(Predicates.Field<CardLabel>(c => c.CardId, Operator.Eq, card.Id)).Select(u => u.LabelId);
            if (labelIds == null || labelIds.Count() == 0) {
                return new List<Label>();
            }

            var pg = new PredicateGroup { Operator = GroupOperator.Or, Predicates = new List<IPredicate>() };
            foreach (var labelIdItem in labelIds) {
                pg.Predicates.Add(Predicates.Field<Label>(b => b.Id, Operator.Eq, labelIdItem));
            }
            return DBService.GetBy<Label>(pg);
        }

        public static void Delete(Guid id) {
            var list = DBService.GetByKey<List>(id);
            DBService.Execute((customRepository) =>
            {
                Delete(list, customRepository);
            });
        }

        public static void Delete(List list, CustomRepository customRepository) {
            list.Cards = customRepository.GetBy<Card>(Predicates.Field<Card>(c => c.ListId, Operator.Eq, list.Id));

            if (list.Cards != null) {
                foreach (var cardItem in list.Cards) {
                    cardItem.IsDeleted = true;
                    customRepository.Update(cardItem);
                }
            }

            list.IsDeleted = true;
            customRepository.Update(list);
        }
    }
}