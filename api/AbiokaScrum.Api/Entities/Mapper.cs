using AbiokaScrum.Api.Entities.DTO;
using System.Collections.Generic;

namespace AbiokaScrum.Api.Entities
{
    public static class Mapper
    {
        public static UserDTO ToDTO(this User user) {
            if (user == null)
                return null;

            return new UserDTO
            {
                Id = user.Id,
                ImageUrl = user.ImageUrl,
                Name = user.Name
            };
        }

        public static IEnumerable<UserDTO> ToDTO(this IEnumerable<User> users) {
            foreach (var userItem in users) {
                yield return userItem.ToDTO();
            }
        }
    }
}