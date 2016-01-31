using AbiokaScrum.Api.Entitites.Validation;
using System;

namespace AbiokaScrum.Api.Entities.DTO
{
    public class UpdateUserRequest : IValidatableObject
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Initials { get; set; }

        public ValidationResult Validate(ActionType actionType) {
            var collection = new ValidationMessageCollection();

            collection.AddEmptyMessage(Name, "Name");
            collection.AddEmptyMessage(Initials, "Initials");

            return collection.ToValidationResult();
        }
    }
}