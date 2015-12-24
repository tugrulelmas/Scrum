using AbiokaScrum.Api.Entitites.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbiokaScrum.Api.Entities
{
    public class Board : IdAndNameEntity
    {
        public IEnumerable<User> Users { get; set; }

        public IEnumerable<List> Lists { get; set; }
    }
}