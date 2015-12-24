using AbiokaScrum.Api.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace AbiokaScrum.Api.Data.Mock
{
    public class BoardCollection : CollectionBase<Board>
    {
        public BoardCollection()
            : base() {
            list.Add(new Board { Id = 1, Name = "Board - 1", IsDeleted = true, Users = new List<User> { new User() { Name = "Tuğrul Elmas", Email = "tugrulelmas@gmail.com" } } });
            list.Add(new Board { Id = 2, Name = "Board - 2", IsDeleted = true });
            list.Add(new Board { Id = 3, Name = "Board - 3", IsDeleted = false });
        }

        public override Board GetByKey(object key) {
            return list.FirstOrDefault(l => l.Id == Convert.ToInt32(key));
        }
    }
}