using AbiokaScrum.Api.Data;
using AbiokaScrum.Api.Entities;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AbiokaScrum.Api.Contollers
{
    [RoutePrefix("api/List")]
    public class ListController : BaseRepositoryController<List>
    {
        private readonly IListOperation listOperation;

        public ListController(IListOperation listOperation)
            : base(listOperation) {
            this.listOperation = listOperation;
        }

        public override HttpResponseMessage Delete([FromUri] Guid id) {
            listOperation.Delete(id);

            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
