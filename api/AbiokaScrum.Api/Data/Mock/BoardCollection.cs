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
            var listCollection = new ListCollection();
            var userCollection = new UserCollection();

            list.Add(new Board { Id = 1, Name = "Board - 1", IsDeleted = true, Users = userCollection.Where(u => new int[] { 1, 2 }.Contains(u.Id)), Lists = listCollection.Where(l => l.BoardId == 1) });
            list.Add(new Board { Id = 2, Name = "Board - 2", IsDeleted = true });
            list.Add(new Board { Id = 3, Name = "Board - 3", IsDeleted = false });
        }

        public override Board GetByKey(object key) {
            return list.FirstOrDefault(l => l.Id == Convert.ToInt32(key));
        }
    }
}