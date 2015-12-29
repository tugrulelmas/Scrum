using AbiokaScrum.Api.Caches;
using AbiokaScrum.Api.Data.Mock;
using AbiokaScrum.Api.Entities;
using AbiokaScrum.Api.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AbiokaScrum.Api.Contollers
{
    [RoutePrefix("api/Card")]
    public class CardController : BaseRepositoryController<Card>
    {

    }
}
