using AbiokaScrum.Api.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace AbiokaScrum.Api.Data.Mock
{
    public class ListCollection : CollectionBase<List>
    {
        public ListCollection()
            : base() {
            list.Add(new List { Id = 1, Name = "To Do", IsDeleted = true });
            list.Add(new List { Id = 2, Name = "Doing", IsDeleted = true });
            list.Add(new List { Id = 3, Name = "Testing", IsDeleted = false });
        }

        public override List GetByKey(object key) {
            return list.FirstOrDefault(l => l.Id == Convert.ToInt32(key));
        }
    }
}