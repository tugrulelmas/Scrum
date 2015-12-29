using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace AbiokaScrum.Api.Data.Mock
{
    public abstract class CollectionBase<T> : IList<T> where T : class, new()
    {
        protected List<T> list;

        public CollectionBase() {
            list = new List<T>();
        }

        public int IndexOf(T item) {
            return list.IndexOf(item);
        }

        public void Insert(int index, T item) {
            list.Insert(index, item);
        }

        public void RemoveAt(int index) {
            list.RemoveAt(index);
        }

        public T this[int index] {
            get {
                return list[index];
            }
            set {
                list[index] = value;
            }
        }

        public void Add(T item) {
            list.Add(item);
        }

        public void Clear() {
            list.Clear();
        }

        public bool Contains(T item) {
            return list.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex) {
            list.CopyTo(array, arrayIndex);
        }

        public int Count {
            get { return list.Count; }
        }

        public bool IsReadOnly {
            get { return false; }
        }

        public bool Remove(T item) {
            return list.Remove(item);
        }

        public IEnumerator<T> GetEnumerator() {
            return list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

        public abstract T GetByKey(object key);

        public IEnumerable<T> GetBy(Expression<Func<T, bool>> predicate, object order = null) {
            return list.Where(predicate.Compile());
        }
    }
}