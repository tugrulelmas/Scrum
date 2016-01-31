using AbiokaScrum.Api.Entitites.Validation;
using System.Collections.Generic;
using System.Net;

namespace AbiokaScrum.Api.Exceptions
{
    public class ValidationException : ApiException
    {
        private IEnumerable<ValidationMessage> validationMessages;

        public ValidationException(string errorCode)
            : this(new ValidationMessage { ErrorCode = errorCode }) {

        }

        public ValidationException(ValidationMessage validationMessage)
            : this(new List<ValidationMessage> { validationMessage }) {

        }

        public ValidationException(IEnumerable<ValidationMessage> validationMessages)
            : base(string.Empty) {
            this.validationMessages = validationMessages;
            ContentValue = validationMessages;

            ExtraHeaders.Add("Status-Reason", "validation-failed");
        }
    }

    public class DenialException : ValidationException
    {
        public DenialException(string errorCode)
            : this(HttpStatusCode.BadRequest, errorCode) {
        }

        public DenialException(ValidationMessage validationMessage)
            : this(HttpStatusCode.BadRequest, validationMessage) {
        }

        public DenialException(HttpStatusCode statusCode, string errorCode)
            : this(statusCode, new ValidationMessage { ErrorCode = errorCode }) {
        }

        public DenialException(HttpStatusCode statusCode, ValidationMessage validationMessage)
            : base(validationMessage) {
            StatusCode = statusCode;
        }
    }
}
