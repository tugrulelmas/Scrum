using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbiokaScrum.Api.Entitites.Validation
{
    public class ValidationMessage
    {
        public string ErrorCode { get; set; }

        public IEnumerable<ValidationArg> Args { get; set; }
    }

    public class ValidationArg
    {
        public string Name { get; set; }

        public bool IsLocalizable { get; set; }
    }
}
