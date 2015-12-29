using AbiokaScrum.Api.Entitites.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbiokaScrum.Api.Entities
{
    public class Label : IdAndNameEntity
    {
        public string Type { get; set; }

        public override ValidationResult Validate(ActionType actionType) {
            var validationResult = base.Validate(actionType);
            if (actionType == ActionType.Delete) {
                return validationResult;
            }

            var collection = new ValidationMessageCollection();
            collection.AddRange(validationResult.Messages);

            collection.AddEmptyMessage(Type, "Type");

            return collection.ToValidationResult();
        }
    }
}