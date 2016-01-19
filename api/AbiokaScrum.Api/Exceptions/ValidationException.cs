﻿using AbiokaScrum.Api.Entitites.Validation;
using System.Collections.Generic;

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
}
