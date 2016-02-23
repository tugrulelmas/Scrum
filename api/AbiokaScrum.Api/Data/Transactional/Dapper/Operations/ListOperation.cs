using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AbiokaScrum.Api.Entities;
using DapperExtensions;
using AbiokaScrum.Api.Entities.DTO;

namespace AbiokaScrum.Api.Data.Transactional.Dapper.Operations
{
    public class ListOperation : Operation<List>, IListOperation
    {
        private readonly IOperation<CardLabel> cardLabelOperation;
        private readonly IOperation<CardUser> cardUserOperation;
        private readonly ICardOperation cardOperation;
        private readonly IUserOperation userOperation;
        private readonly ILabelOperation labelOperation;

        public ListOperation(IOperation<CardLabel> cardLabelOperation, ICardOperation cardOperation, 
            IUserOperation userOperation, IOperation<CardUser> cardUserOperation, ILabelOperation labelOperation) {
            this.cardLabelOperation = cardLabelOperation;
            this.cardUserOperation = cardUserOperation;
            this.cardOperation = cardOperation;
            this.userOperation = userOperation;
            this.labelOperation = labelOperation;
        }

        public IEnumerable<List> GetByBoardId(Guid boardId) {
            var predicate = Predicates.Field<List>(l => l.BoardId, Operator.Eq, boardId);
            var list = GetBy(predicate);
            foreach (var listItem in list)
            {
                var sort = new List<ISort>
                {
                    Predicates.Sort<Card>(b => b.Order)
                };
                listItem.Cards = ((Operation<Card>)cardOperation).GetBy(Predicates.Field<Card>(c => c.ListId, Operator.Eq, listItem.Id), sort);
                foreach (var cardItem in listItem.Cards)
                {
                    cardItem.Users = GetUsers(cardItem);
                    cardItem.Labels = GetLabels(cardItem);
                }
            }
            return list;
        }

        private IEnumerable<UserDTO> GetUsers(Card card) {
            var userIds = ((Operation<CardUser>)cardUserOperation).GetBy(Predicates.Field<CardUser>(c => c.CardId, Operator.Eq, card.Id)).Select(u => u.UserId);
            if (userIds == null || userIds.Count() == 0)
            {
                return new List<UserDTO>();
            }

            return ((Operation<User>)userOperation).GetBy(Predicates.Field<User>(b => b.Id, Operator.Eq, userIds)).ToDTO();
        }

        private IEnumerable<Label> GetLabels(Card card) {
            var labelIds = ((Operation<CardLabel>)cardLabelOperation).GetBy(Predicates.Field<CardLabel>(c => c.CardId, Operator.Eq, card.Id)).Select(u => u.LabelId);
            if (labelIds == null || labelIds.Count() == 0)
            {
                return new List<Label>();
            }

            return ((Operation<Label>)labelOperation).GetBy(Predicates.Field<Label>(b => b.Id, Operator.Eq, labelIds));
        }

        public void Delete(Guid id) {
            var list = GetByKey(id);
            Execute((repository) =>
            {
                Delete(list, repository);
            });
        }
        
        internal void Delete(List list, IRepository repository) {
            list.Cards = repository.GetBy<Card>(Predicates.Field<Card>(c => c.ListId, Operator.Eq, list.Id));

            if (list.Cards != null)
            {
                foreach (var cardItem in list.Cards)
                {
                    cardItem.IsDeleted = true;
                    repository.Update(cardItem);
                }
            }

            list.IsDeleted = true;
            repository.Update(list);
        }
    }
}