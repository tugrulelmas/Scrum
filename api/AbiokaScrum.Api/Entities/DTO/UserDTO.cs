using System;
using System.Linq;
using System.Text;

namespace AbiokaScrum.Api.Entities.DTO
{
    public class UserDTO
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Initials { get; set; }

        public string ImageUrl { get; set; }
    }
}