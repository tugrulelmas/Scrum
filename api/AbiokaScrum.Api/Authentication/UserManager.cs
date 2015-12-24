using AbiokaScrum.Api.Caches;
using AbiokaScrum.Api.Entities;
using AbiokaScrum.Api.Service;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AbiokaScrum.Authentication
{
    public class UserManager
    {
        public static CustomPrincipal GetUser(UserInfo userInfo) {
            if (userInfo == null)
                return null;

            var dbUser = UserCache.GetUser(userInfo.Token);
            var user = new CustomPrincipal(dbUser.Email) {
                Token = userInfo.Token,
                CultureInfo = CultureInfo.GetCultureInfo(userInfo.Language),
                UserName = dbUser.Email,
                Email = dbUser.Email
            };

            return user;
        }
    }
}
