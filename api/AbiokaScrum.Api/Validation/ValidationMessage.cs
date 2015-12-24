using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbiokaScrum.Api.Entitites.Validation
{
    public class ValidationMessage
    {
        public string ErrorMessage { get; set; }

        public string ErrorCode { get; set; }

        public string PropertyName { get; set; }
    }
}
