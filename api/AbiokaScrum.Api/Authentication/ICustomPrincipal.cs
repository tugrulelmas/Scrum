using System.Security.Principal;

namespace AbiokaScrum.Authentication
{
    public interface ICustomPrincipal : IPrincipal
    {
        string UserName { get; set; }

        string Email { get; set; }
    }
}
