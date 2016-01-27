using AbiokaScrum.Api.Entitites.Validation;
using System;

namespace AbiokaScrum.Api.Entities
{
    public abstract class Entity : IIdEntity, IValidatableObject
    {
        public virtual Guid Id { get; set; }

        public virtual DateTime CreateDate { get; set; }

        public abstract ValidationResult Validate(ActionType actionType);
    }
}