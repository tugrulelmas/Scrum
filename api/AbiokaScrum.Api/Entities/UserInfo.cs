using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbiokaScrum.Api.Entities
{
    public class UserInfo
    {
        public string Email { get; set; }

        public string ProviderToken { get; set; }

        public AuthProvider Provider { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }
    }
}