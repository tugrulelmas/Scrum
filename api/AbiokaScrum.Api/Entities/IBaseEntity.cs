using System;

namespace AbiokaScrum.Api.Entities
{
    public interface IBaseEntity : IIdEntity
    {
        DateTime CreateDate { get; set; }
    }
}