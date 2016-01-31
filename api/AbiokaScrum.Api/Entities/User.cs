using AbiokaScrum.Api.Helper;

namespace AbiokaScrum.Api.Entities
{
    public class User : IdAndNameEntity
    {
        public string Email { get; set; }

        public string ProviderToken { get; set; }

        public AuthProvider AuthProvider { get; set; }

        public string Token { get; set; }

        public string ImageUrl { get; set; }

        public string Password { get; set; }

        public string Initials { get; set; }

        public string GetHashedPassword(string password) {
            var hashedPassword = Util.GetHashText(string.Concat(Email.ToLowerInvariant(), "#", password));
            return hashedPassword;
        }
    }
}