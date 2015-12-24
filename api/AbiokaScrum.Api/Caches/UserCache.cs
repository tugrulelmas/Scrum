using AbiokaScrum.Api.Entities;
using AbiokaScrum.Exceptions;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbiokaScrum.Api.Caches
{
    public class UserCache
    {
        private static ConcurrentDictionary<string, User> users;

        static UserCache() {
            users = new ConcurrentDictionary<string, User>();
        }

        public static void AddUser(User user) {
            var cacheUser = users.Values.FirstOrDefault(u => u.Email == user.Email);
            if (cacheUser != null) {
                User tmpUser;
                users.TryRemove(cacheUser.Token, out tmpUser);
            }
            users.TryAdd(user.Token, user);
        }

        public static User GetUser(string token) {
            User user;
            if (!users.TryGetValue(token, out user))
                throw new GlobalException("user cannot be found");

            return user;
        }
    }
}