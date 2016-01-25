using System;
using System.Collections.Generic;

namespace AbiokaScrum.Api.Entities
{
    public class List : IdAndNameEntity
    {
        public Guid BoardId { get; set; }

        public IEnumerable<Card> Cards { get; set; }
    }
}