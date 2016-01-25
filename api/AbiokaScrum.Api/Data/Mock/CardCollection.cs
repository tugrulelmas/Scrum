using AbiokaScrum.Api.Entities;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace AbiokaScrum.Api.Data.Mock
{
    public class CardCollection : CollectionBase<Card>
    {
        private static ConcurrentDictionary<Guid, Card> values = new ConcurrentDictionary<Guid, Card>();

        static CardCollection() {
            var card1 = new Card { Id = Guid.NewGuid(), Title = "Hasat", EstimatedPoints = 8, Labels = new List<Label> { LabelCollection.Values.First() }, Users = new List<User> { UserCollection.Values.First() } };
            var card2 = new Card { Id = Guid.NewGuid(), Title = "Sulama", EstimatedPoints = 2, Labels = new List<Label> { LabelCollection.Values.ElementAt(1) }, Users = new List<User> { UserCollection.Values.ElementAt(1) } };
            var card3 = new Card { Id = Guid.NewGuid(), Title = "Gübreleme", EstimatedPoints = 3, Comments = new List<Comment> { new Comment { Text = "Dap gübre kullandım", User = UserCollection.Values.ElementAt(1), CreateDate = DateTime.Now } } };
            var card4 = new Card { Id = Guid.NewGuid(), Title = "Çapalama", EstimatedPoints = 5 };
            values.TryAdd(card1.Id, card1);
            values.TryAdd(card2.Id, card2);
            values.TryAdd(card3.Id, card3);
            values.TryAdd(card4.Id, card4);
        }

        public override Card GetByKey(object key) {
            return values[new Guid(key.ToString())];
        }

        public override IEnumerator<Card> GetEnumerator() {
            return values.Values.GetEnumerator();
        }

        public override IEnumerable<Card> GetAll() {
            throw new NotImplementedException();
        }

        public static IEnumerable<Card> Values
        {
            get { return values.Values; }
        }
    }
}