using AbiokaScrum.Api.Entitites.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbiokaScrum.Api.Entities
{
    public class Card : Entity
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public short EstimatedPoints { get; set; }

        public int ListId { get; set; }

        public IEnumerable<Label> Labels { get; set; }

        public IEnumerable<User> Users { get; set; }

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