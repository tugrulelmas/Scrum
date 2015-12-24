using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbiokaScrum.Api.Entitites.Validation
{
    public class ValidationResult
    {
        public bool IsValid { get; set; }

        public IEnumerable<ValidationMessage> Messages { get; set; }
    }
}
