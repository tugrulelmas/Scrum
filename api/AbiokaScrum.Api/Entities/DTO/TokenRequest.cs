using AbiokaScrum.Api.Entitites.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbiokaScrum.Api.Entities.DTO
{
    public class TokenRequest : IValidatableObject
    {
        public string Email { get; set; }

        public string ProviderToken { get; set; }

        public AuthProvider Provider { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public ValidationResult Validate(ActionType actionType) {
            var collection = new ValidationMessageCollection();

            collection.AddEmptyMessage(Name, "Name");
            collection.AddEmptyMessage(Email, "Email");
            collection.AddEmptyMessage(ProviderToken, "ProviderToken");

            return collection.ToValidationResult();
        }
    }
}