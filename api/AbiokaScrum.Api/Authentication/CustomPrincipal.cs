using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace AbiokaScrum.Authentication
{
    public class CustomPrincipal : ICustomPrincipal
    {
        public CustomPrincipal(string userName) {
            this.Identity = new GenericIdentity(userName);
        }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string Token { get; set; }

        public IIdentity Identity { get; private set; }

        public DateTime TokenExpirationDate { get; set; }

        public bool IsInRole(string role) {
            return Roles.Where(r => r == role).Any();
        }

        public string[] Roles { get; set; }

        public Guid Id { get; set; }
    }
}
