using AbiokaScrum.Api.Entities;
using DapperExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
    }
}