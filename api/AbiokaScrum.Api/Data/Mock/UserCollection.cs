using AbiokaScrum.Api.Entities;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace AbiokaScrum.Api.Data.Mock
{
    public class UserCollection : CollectionBase<User>
    {
        protected static ConcurrentDictionary<Guid, User> values = new ConcurrentDictionary<Guid, User>();

        static UserCollection() {
            var user1 = new User { Id = Guid.NewGuid(), Name = "Tuğrul Elmas", Email = "tugrulelmas@gmail.com", Token = Guid.NewGuid().ToString(), ImageUrl = "https://lh4.googleusercontent.com/-hiP-LDMBt5s/AAAAAAAAAAI/AAAAAAAAAEo/vwOaOpb8JNk/s96-c/photo.jpg", IsDeleted = true };
            var user2 = new User { Id = Guid.NewGuid(), Name = "Cemal Süreya", Email = "c", IsDeleted = true };
            var user3 = new User { Id = Guid.NewGuid(), Name = "Orhan Veli", Email = "o", IsDeleted = false };
            values.TryAdd(user1.Id, user1);
            values.TryAdd(user2.Id, user2);
            values.TryAdd(user3.Id, user3);
        }

        public override User GetByKey(object key) {
            return values[new Guid(key.ToString())];
        }

        public override IEnumerator<User> GetEnumerator() {
            return values.Values.GetEnumerator();
        }

        public override IEnumerable<User> GetAll() {
            return Values.OrderBy(v => v.Name);
        }

        public static IEnumerable<User> Values{
            get { return values.Values; }
        }
    }
}