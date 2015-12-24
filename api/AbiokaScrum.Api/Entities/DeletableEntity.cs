using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbiokaScrum.Api.Entities
{
    public abstract class DeletableEntity : Entity, IDeletableEntity
    {
        public virtual bool IsDeleted { get; set; }
    }
}