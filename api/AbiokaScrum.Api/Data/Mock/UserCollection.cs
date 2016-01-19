using AbiokaScrum.Api.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace AbiokaScrum.Api.Data.Mock
{
    public class UserCollection : CollectionBase<User>
    {
        private static string guid;
        static UserCollection()
        {
            guid = Guid.NewGuid().ToString();
        }

        public UserCollection()
            : base() {
            list.Add(new User { Id = 1, Name = "Tuğrul Elmas", Email = "tugrulelmas@gmail.com", Token = guid, ImageUrl = "https://lh4.googleusercontent.com/-hiP-LDMBt5s/AAAAAAAAAAI/AAAAAAAAAEo/vwOaOpb8JNk/s96-c/photo.jpg", IsDeleted = true });
            list.Add(new User { Id = 2, Name = "Cemal Süreya", Email = "c", IsDeleted = true });
            list.Add(new User { Id = 3, Name = "Orhan Veli", Email = "o", IsDeleted = false });
        }

        public override User GetByKey(object key) {
            return list.FirstOrDefault(l => l.Email == key.ToString());
        }
    }
}