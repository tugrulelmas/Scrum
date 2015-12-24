using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace AbiokaScrum.Authentication
{
    public interface ICustomPrincipal : IPrincipal
    {
        string UserName { get; set; }

        CultureInfo CultureInfo { get; set; }

        string Email { get; set; }
    }
}
