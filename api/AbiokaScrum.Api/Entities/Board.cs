using System;
using System.Collections.Generic;

namespace AbiokaScrum.Api.Entities
{
    public class Board : IdAndNameEntity
    {
        public Guid CreatedUser { get; set; }

        public IEnumerable<User> Users { get; set; }

        public IEnumerable<List> Lists { get; set; }
    }
}