using AbiokaScrum.Api.Data;
using AbiokaScrum.Api.Entities;
using System.Web.Http;

namespace AbiokaScrum.Api.Contollers
{
    [RoutePrefix("api/Label")]
    public class LabelController : BaseRepositoryController<Label>
    {
        public LabelController(ILabelOperation operation)
            : base(operation) {
        }
    }
}
