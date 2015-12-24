using AbiokaScrum.Api.Filters;
using AbiokaScrum.Authentication;
using AbiokaScrum.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AbiokaScrum.Api.Contollers
{
    [CustomExceptionFilter]
    [IdentityBasicAuthentication]
    public class BaseApiController : ApiController
    {
        public ICustomPrincipal CurrentUser { get { return (ICustomPrincipal)User; } }
    }
}
