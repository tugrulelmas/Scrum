using AbiokaScrum.Api.Data;
using AbiokaScrum.Api.Data.Transactional;
using AbiokaScrum.Api.Entities;
using AbiokaScrum.Api.Exceptions;
using AbiokaScrum.Api.Helper;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AbiokaScrum.Api.Contollers
{
    public class BaseRepositoryController<T> : BaseApiController where T : class, IIdEntity, new()
    {
        protected readonly IOperation<T> operation;

        public BaseRepositoryController(IOperation<T> operation) {
            this.operation = operation;
        }

        [Route("")]
        public virtual HttpResponseMessage Get() {
            IEnumerable<T> result = operation.Get();
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [Route("")]
        public virtual HttpResponseMessage Get([FromUri]Guid id) {
            T result = operation.GetByKey(id);
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

            operation.Add(entity);

            var response = Request.CreateResponse(HttpStatusCode.Created, entity);
            string uri = Url.Link("DefaultApi", new { id = entity.Id });
            response.Headers.Location = new Uri(uri);
            return response;
        }

        [HttpPut]
        [Route("update")]
        public virtual HttpResponseMessage Update([FromBody]T entity) {
            if (entity == null) {
                throw new ArgumentNullException("entity");
            }

            operation.Update(entity);

            return Request.CreateResponse(HttpStatusCode.OK);
        }


        [HttpDelete]
        [Route("delete")]
        public virtual HttpResponseMessage Delete([FromUri]Guid id) {
            var entity = operation.GetByKey(id);
            if(entity == null) {
                throw new DenialException(ErrorMessage.NotFound);
            }

            operation.Remove(entity);

            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
