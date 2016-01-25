using AbiokaScrum.Api.Entities;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace AbiokaScrum.Api.Data.Mock
{
    public class LabelCollection : CollectionBase<Label>
    {
        private static ConcurrentDictionary<Guid, Label> values = new ConcurrentDictionary<Guid, Label>();

        static LabelCollection() {
            var label1 = new Label { Id = Guid.NewGuid(), Name = "Pancar", Type = "success", IsDeleted = true };
            var label2 = new Label { Id = Guid.NewGuid(), Name = "Buğday", Type = "info", IsDeleted = true };
            values.TryAdd(label1.Id, label1);
            values.TryAdd(label2.Id, label2);
        }

        public override Label GetByKey(object key) {
            return values[new Guid(key.ToString())];
        }

        public override IEnumerator<Label> GetEnumerator() {
            return values.Values.GetEnumerator();
        }

        public override IEnumerable<Label> GetAll() {
            return Values.OrderBy(v => v.Name);
        }

        public static IEnumerable<Label> Values
        {
            get { return values.Values; }
        }
    }
}