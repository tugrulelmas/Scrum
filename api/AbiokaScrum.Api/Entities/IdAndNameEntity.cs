using AbiokaScrum.Api.Entitites.Validation;

namespace AbiokaScrum.Api.Entities
{
    public abstract class IdAndNameEntity : DeletableEntity
    {
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