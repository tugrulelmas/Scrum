using AbiokaScrum.Api.Entitites.Validation;
using System;

namespace AbiokaScrum.Api.Entities
{
    public class Comment : Entity
    {
        public string Text { get; set; }

        public User User { get; set; }

        public override ValidationResult Validate(ActionType actionType) {
            var collection = new ValidationMessageCollection();

            if (actionType != ActionType.Delete) {
                collection.AddEmptyMessage(Text, "Text");
            }

            return collection.ToValidationResult();
        }
    }
}