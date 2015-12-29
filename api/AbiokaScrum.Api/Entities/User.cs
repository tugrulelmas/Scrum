using AbiokaScrum.Api.Entitites.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace AbiokaScrum.Api.Entities
{
    public class User : IdAndNameEntity
    {
        public string Email { get; set; }

        public string Token { get; set; }

        public string ImageUrl { get; set; }

        public string ShortName {
            get {
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