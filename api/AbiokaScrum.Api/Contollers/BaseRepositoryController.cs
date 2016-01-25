using AbiokaScrum.Api.Filters;
using AbiokaScrum.Api.Service;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AbiokaScrum.Api.Contollers
{
    [ValidationFilter()]
    public class BaseRepositoryController<T> : BaseApiController where T : class, new()
    {
        [Route("")]
        public virtual HttpResponseMessage Get() {
            IEnumerable<T> result = DBService.Get<T>();
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [Route("")]
        public virtual HttpResponseMessage Get([FromUri]Guid id) {
            T result = DBService.GetByKey<T>(id);
            if (result == null) {
                return Request.CreateResponse(HttpStatusCode.NotFound, "NotFound");
            }

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpPost]
        [Route("Add")]
        public virtual HttpResponseMessage Add([FromBody]T entity) {
            if (entity == null) {
                throw new ArgumentNullException("entity");
            }

            DBService.Add<T>(entity);

            var response = Request.CreateResponse(HttpStatusCode.Created, entity);
            return response;
        }

        [HttpPut]
        [Route("update")]
        public virtual HttpResponseMessage Update([FromBody]T entity) {
            if (entity == null) {
                throw new ArgumentNullException("entity");
            }

            DBService.Update<T>(entity);

            return Request.CreateResponse(HttpStatusCode.OK);
        }


        [HttpPost]
        [Route("delete")]
        public virtual HttpResponseMessage Delete([FromBody]T entity, [FromUri]string d) {
            if (entity == null) {
                throw new ArgumentNullException("entity");
            }

            DBService.Remove<T>(entity);

            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
