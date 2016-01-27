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
    [RoutePrefix("api/List")]
    public class ListController : BaseRepositoryController<List>
    {
        public override HttpResponseMessage Delete([FromUri] Guid id) {
            ListService.Delete(id);

            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
