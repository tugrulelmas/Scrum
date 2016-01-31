using AbiokaScrum.Api.Entitites.Validation;
using System;

namespace AbiokaScrum.Api.Entities.DTO
{
    public class ChangePasswordRequest : IValidatableObject
    {
        public Guid Id { get; set; }

        public string OldPassword { get; set; }

        public string NewPassword { get; set; }

        public ValidationResult Validate(ActionType actionType) {
            var collection = new ValidationMessageCollection();

            collection.AddEmptyMessage(OldPassword, "OldPassword");
            collection.AddEmptyMessage(NewPassword, "NewPassword");

            return collection.ToValidationResult();
        }
    }
}