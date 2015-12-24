using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbiokaScrum.Api.Entitites.Validation
{
    public class ValidationMessageCollection : IEnumerable<ValidationMessage>
    {
        private List<ValidationMessage> validationMessages;

        public ValidationMessageCollection() {
            validationMessages = new List<ValidationMessage>();
        }

        public void Add(ValidationMessage validationMessage) {
            validationMessages.Add(validationMessage);
        }

        public void AddRange(IEnumerable<ValidationMessage> collection) {
            validationMessages.AddRange(collection);
        }

        public void AddEmptyMessage(string value, string propertyName) {
            if (string.IsNullOrWhiteSpace(value)) {
                validationMessages.Add(new ValidationMessage() {
                    ErrorCode = ValidationCode.EmptyProperty,
                    PropertyName = propertyName
                });
            }
        }

        public ValidationResult ToValidationResult() {
            return new ValidationResult() {
                IsValid = validationMessages.Count == 0,
                Messages = validationMessages
            };
        }

        public IEnumerator<ValidationMessage> GetEnumerator() {
            return validationMessages.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }
    }
}
