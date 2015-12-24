using AbiokaScrum.Api.Data;
using AbiokaScrum.Api.Entities;
using AbiokaScrum.Api.Service;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AbiokaScrum.Api.Contollers
{
    public class BaseDeletableRepositoryController<T> : BaseRepositoryController<T> where T : class, IDeletableEntity, new()
    {
        [HttpPut]
        [Route("delete")]
        public override HttpResponseMessage Delete([FromBody]T entity, [FromUri]string d) {
            if (entity == null) {
                throw new ArgumentNullException("entity");
            }

            entity.IsDeleted = false;
            DBService.Update<T>(entity);

            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
