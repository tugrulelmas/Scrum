using AbiokaScrum.Api.Exceptions.Adapters;
using System.Web.Http.Filters;

namespace AbiokaScrum.Api.Filters
{
    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context) {
            IExceptionAdapter exceptionAdapter = ExceptionAdapterFactory.GetAdapter(context.Exception);
            context.Response = exceptionAdapter.GetResponseMessage(context.Request);
        }
    }
}