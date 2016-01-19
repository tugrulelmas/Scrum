using AbiokaScrum.Api.Entitites.Validation;
using AbiokaScrum.Api.Exceptions;
using System.Linq;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace AbiokaScrum.Api.Filters
{
    public class ValidationFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext) {
            if (!new string[] { "POST", "PUT", "DELETE" }.Contains(actionContext.Request.Method.Method))
                return;

            foreach (var actionArgumentItem in actionContext.ActionArguments) {
                if (!(actionArgumentItem.Value is IValidatableObject))
                    continue;

                var actionType = GetActionType(actionContext);
                var validationResult = ((IValidatableObject)actionArgumentItem.Value).Validate(actionType);
                if (!validationResult.IsValid) {
                    throw new ValidationException(validationResult.Messages);
                }
            }
        }

        private ActionType GetActionType(HttpActionContext actionContext) {
            ActionType result = ActionType.Add;
            var method = actionContext.Request.Method.Method;
            if (method == "PUT") {
                result = ActionType.Update;
            } else if (method == "DELETE" || actionContext.Request.RequestUri.AbsolutePath.ToLowerInvariant().EndsWith("delete")) {
                result = ActionType.Delete;
            }
            return result;
        }
    }
}