using AbiokaScrum.Api.Entities;
using DapperExtensions;
using System.Linq;

namespace AbiokaScrum.Api.Data.Transactional.Dapper.Operations
{
    public class UserOperation : Operation<User>, IUserOperation
    {
        public User GetByEmail(string email) {
            var predicate = Predicates.Field<User>(u => u.Email, Operator.Eq, email);
            var result = GetBy(predicate).FirstOrDefault();
            return result;
        }
    }
}