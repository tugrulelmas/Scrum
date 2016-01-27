using AbiokaScrum.Api.Entities;
using DapperExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbiokaScrum.Api.Service
{
    public class UserService
    {
        public static User GetByEmail(string email) {
            var predicate = Predicates.Field<User>(u => u.Email, Operator.Eq, email);
            var result = DBService.GetBy<User>(predicate).FirstOrDefault();
            return result;
        }
    }
}