using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbiokaScrum.Api.Entities
{
    public class CardLabel : IEntity
    {
        public Guid CardId { get; set; }

        public Guid LabelId { get; set; }
    }
}