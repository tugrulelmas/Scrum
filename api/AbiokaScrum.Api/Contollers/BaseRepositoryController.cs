using AbiokaScrum.Api.Entities;
using AbiokaScrum.Api.Exceptions;
using AbiokaScrum.Api.Filters;
using AbiokaScrum.Api.Helper;
using AbiokaScrum.Api.Service;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AbiokaScrum.Api.Contollers
{
    [ValidationFilter()]
    public class BaseRepositoryController<T> : BaseApiController where T : class, IIdEntity, new()
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

            DBService.Add(entity);

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

            DBService.Update(entity);

            return Request.CreateResponse(HttpStatusCode.OK);
        }


        [HttpDelete]
        [Route("delete")]
        public virtual HttpResponseMessage Delete([FromUri]Guid id) {
            var entity = DBService.GetByKey<T>(id);
            if(entity == null) {
                throw new DenialException(ErrorMessage.NotFound);
            }

            DBService.Remove(entity);

            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
