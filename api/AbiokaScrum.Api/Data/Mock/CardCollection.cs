using AbiokaScrum.Api.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace AbiokaScrum.Api.Data.Mock
{
    public class CardCollection : CollectionBase<Card>
    {
        public CardCollection()
            : base() {
            var userCollection = new UserCollection();
            var labelCollection = new LabelCollection();

            list.Add(new Card { ListId = 1, Id = 1, Title = "Hasat", EstimatedPoints = 8, Labels = labelCollection.Where(l => l.Id == 1), Users = userCollection.Where(u => u.Id == 1) });
            list.Add(new Card { ListId = 2, Id = 2, Title = "Sulama", EstimatedPoints = 2, Labels = labelCollection.Where(l => l.Id == 2), Users = userCollection.Where(u => u.Id == 2) });
            list.Add(new Card { ListId = 4, Id = 3, Title = "Gübreleme", EstimatedPoints = 3, Comments = new List<Comment> { new Comment { Text = "Dap gübre kullandım", User = userCollection.FirstOrDefault(u => u.Id == 2), CreateDate = DateTime.Now } } });
            list.Add(new Card { ListId = 4, Id = 4, Title = "Çapalama", EstimatedPoints = 5 });
        }

        public override Card GetByKey(object key) {
            return list.FirstOrDefault(l => l.Id == Convert.ToInt32(key));
        }
    }
}