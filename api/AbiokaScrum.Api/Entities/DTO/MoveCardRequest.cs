using System;

namespace AbiokaScrum.Api.Entities.DTO
{

    public class MoveCardRequest
    {
        public Guid CardId { get; set; }

        public int CurrentIndex { get; set; }

        public int NewIndex { get; set; }

        public Guid NewListId { get; set; }
    }
}