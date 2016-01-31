using AbiokaScrum.Api.Filters;
using AbiokaScrum.Authentication;
using AbiokaScrum.Filters;
using System.Web.Http;

namespace AbiokaScrum.Api.Contollers
{
    [CustomExceptionFilter]
    [IdentityBasicAuthentication]
    [ValidationFilter()]
    public class BaseApiController : ApiController
    {
        public ICustomPrincipal CurrentUser { get { return (ICustomPrincipal)User; } }
    }
}
