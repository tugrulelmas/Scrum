using System;

namespace AbiokaScrum.Api.Entities
{
    public interface IIdEntity : IEntity
    {
        Guid Id { get; set; }
    }
}