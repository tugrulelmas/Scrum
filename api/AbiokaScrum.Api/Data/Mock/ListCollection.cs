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
            var cardCollection = new CardCollection();

            list.Add(new List { BoardId = 1, Id = 1, Name = "To Do", Cards = cardCollection.Where(c => c.ListId == 1), IsDeleted = false });
            list.Add(new List { BoardId = 1, Id = 2, Name = "Doing", Cards = cardCollection.Where(c => c.ListId == 2), IsDeleted = false });
            list.Add(new List { BoardId = 1, Id = 3, Name = "Testing", Cards = cardCollection.Where(c => c.ListId == 3), IsDeleted = false });
            list.Add(new List { BoardId = 1, Id = 4, Name = "Done", Cards = cardCollection.Where(c => c.ListId == 4), IsDeleted = false });
        }

        public override List GetByKey(object key) {
            return list.FirstOrDefault(l => l.Id == Convert.ToInt32(key));
        }
    }
}