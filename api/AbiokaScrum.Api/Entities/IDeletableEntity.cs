using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbiokaScrum.Api.Entities
{
    public interface IDeletableEntity
    {
        bool IsDeleted { get; set; }
    }
}
