using System;

namespace AbiokaScrum.Api.Entities
{
    public class BoardUser : IEntity
    {
        public Guid BoardId { get; set; }

        public Guid UserId { get; set; }
    }
}