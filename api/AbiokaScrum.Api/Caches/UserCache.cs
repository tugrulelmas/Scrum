using AbiokaScrum.Api.Entities;
using AbiokaScrum.Api.Exceptions;
using System.Collections.Concurrent;
using System.Linq;

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
    }
}