using AbiokaScrum.Api.Entities.DTO;
using AbiokaScrum.Api.Entitites.Validation;
using System;
using System.Collections.Generic;

namespace AbiokaScrum.Api.Entities
{
    public class Comment : Entity
    {
        public string Text { get; set; }

        public Guid UserId { get; set; }

        public Guid CardId { get; set; }

        public UserDTO User { get; set; }

        public override ValidationResult Validate(ActionType actionType) {
            var collection = new ValidationMessageCollection();

            if (actionType != ActionType.Delete) {
                collection.AddEmptyMessage(Text, "Text");
            }

            return collection.ToValidationResult();
        }
    }
}