using AbiokaScrum.Api.Entitites.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbiokaScrum.Api.Entities
{
    public class Comment : Entity
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public User User { get; set; }

        public DateTime CreateDate { get; set; }

        public override ValidationResult Validate(ActionType actionType) {
            var collection = new ValidationMessageCollection();

            if (actionType != ActionType.Delete) {
                collection.AddEmptyMessage(Text, "Text");
            }

            return collection.ToValidationResult();
        }
    }
}