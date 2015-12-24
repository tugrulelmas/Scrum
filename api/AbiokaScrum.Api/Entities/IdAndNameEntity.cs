using AbiokaScrum.Api.Entitites.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbiokaScrum.Api.Entities
{
    public abstract class IdAndNameEntity : DeletableEntity
    {
        public virtual int Id { get; set; }

        public virtual string Name { get; set; }

        public override ValidationResult Validate(ActionType actionType) {
            var collection = new ValidationMessageCollection();

            if (actionType != ActionType.Delete) {
                collection.AddEmptyMessage(Name, "Name");
            }

            return collection.ToValidationResult();
        }
    }
}