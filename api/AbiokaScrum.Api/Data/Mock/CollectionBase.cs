using System.Collections;
using System.Collections.Generic;

namespace AbiokaScrum.Api.Data.Mock
{
    public abstract class CollectionBase<T> : IEnumerable<T> where T : class, new()
    {
        public abstract T GetByKey(object key);

        public abstract IEnumerator<T> GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

        public abstract IEnumerable<T> GetAll();
    }
}