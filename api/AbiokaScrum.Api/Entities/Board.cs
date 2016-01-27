using AbiokaScrum.Api.Entities.DTO;
using System;
using System.Collections.Generic;

namespace AbiokaScrum.Api.Entities
{
    public class Board : IdAndNameEntity
    {
        public Guid CreatedUser { get; set; }

        public IEnumerable<UserDTO> Users { get; set; }

        public IEnumerable<List> Lists { get; set; }
    }
}