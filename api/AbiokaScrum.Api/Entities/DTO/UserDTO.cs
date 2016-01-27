using System;
using System.Linq;
using System.Text;

namespace AbiokaScrum.Api.Entities.DTO
{
    public class UserDTO
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public string ShortName
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Name))
                    return string.Empty;

                var names = Name.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                var result = new StringBuilder();
                foreach (var nameItem in names) {
                    result.Append(nameItem.First().ToString().ToUpper());
                }
                return result.ToString();
            }
        }
    }
}