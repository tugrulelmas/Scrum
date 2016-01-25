using AbiokaScrum.Api.Entities;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace AbiokaScrum.Api.Data.Mock
{
    public class ListCollection : CollectionBase<List>
    {
        private static ConcurrentDictionary<Guid, List> values = new ConcurrentDictionary<Guid, List>();

        static ListCollection() {
            var list1 = new List { Id = Guid.NewGuid(), Name = "To Do", Cards = new List<Card> { CardCollection.Values.ElementAt(0) }, IsDeleted = false };
            var list2 = new List { Id = Guid.NewGuid(), Name = "Doing", Cards = new List<Card> { CardCollection.Values.ElementAt(1) }, IsDeleted = false };
            var list3 = new List { Id = Guid.NewGuid(), Name = "Testing", Cards = new List<Card>(), IsDeleted = false };
            var list4 = new List { Id = Guid.NewGuid(), Name = "Done", Cards = new List<Card> { CardCollection.Values.ElementAt(2), CardCollection.Values.ElementAt(3) }, IsDeleted = false };
            values.TryAdd(list1.Id, list1);
            values.TryAdd(list2.Id, list2);
            values.TryAdd(list3.Id, list3);
            values.TryAdd(list4.Id, list4);
        }

        public override List GetByKey(object key) {
            return values[new Guid(key.ToString())];
        }

        public override IEnumerator<List> GetEnumerator() {
            return values.Values.GetEnumerator();
        }

        public override IEnumerable<List> GetAll() {
            throw new NotImplementedException();
        }

        public static IEnumerable<List> Values
        {
            get { return values.Values; }
        }
    }
}