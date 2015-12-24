using AbiokaScrum.Api.Entitites.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbiokaScrum.Api.Entities
{
    public class User : IdAndNameEntity
    {
        public string Email { get; set; }

        public string Token { get; set; }

        public string ImageUrl { get; set; }
    }
}