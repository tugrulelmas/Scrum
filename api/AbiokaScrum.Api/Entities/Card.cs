using AbiokaScrum.Api.Entities.DTO;
using AbiokaScrum.Api.Entitites.Validation;
using System;
using System.Collections.Generic;

namespace AbiokaScrum.Api.Entities
{
    public class Card : DeletableEntity
    {
        public string Title { get; set; }

        public short EstimatedPoints { get; set; }

        public Guid ListId { get; set; }

        public IEnumerable<Label> Labels { get; set; }

        public IEnumerable<UserDTO> Users { get; set; }

        public IEnumerable<Comment> Comments { get; set; }

        public override ValidationResult Validate(ActionType actionType) {
            var collection = new ValidationMessageCollection();

            if (actionType != ActionType.Delete) {
                collection.AddEmptyMessage(Title, "Title");
            }

            return collection.ToValidationResult();
        }
    }
}