using AbiokaScrum.Api.Entities;
using AbiokaScrum.Api.Exceptions;
using AbiokaScrum.Api.Helper;
using AbiokaScrum.Api.Service;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace AbiokaScrum.Api.Authentication
{
    public interface IAuthProviderValidator
    {
        bool IsValid(string email, string token);
    }

    public class LocalAuthProviderValidator : IAuthProviderValidator
    {
        public bool IsValid(string email, string token)
        {
            var dbUser = UserService.GetByEmail(email);
            var result = dbUser != null && dbUser.ProviderToken == token;
            return result;
        }
    }

    public class GoogleAuthProviderValidator : IAuthProviderValidator
    {
        private readonly static string abiokaClientId;
        private readonly string googleAddress = "accounts.google.com";

        static GoogleAuthProviderValidator()
        {
            if (!ConfigurationManager.AppSettings.AllKeys.Contains("AbiokaClientId"))
                throw new ValidationException("Google client id couldn't be found in web.config.");

            abiokaClientId = ConfigurationManager.AppSettings["AbiokaClientId"];
        }

        public bool IsValid(string email, string token)
        {
            return Task.Run(() => IsValidToken(email, token)).Result;
        }

        private async Task<bool> IsValidToken(string email, string token)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://www.googleapis.com/oauth2/v1/");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var url = string.Format("tokeninfo?id_token={0}", token);
                var tokenResult = await client.GetAsync(url);
                if (!tokenResult.IsSuccessStatusCode)
                    return false;

                var result = await tokenResult.Content.ReadAsAsync<GoogleTokenResult>();
                if (result.issuer != googleAddress && result.issuer != string.Format("https://{0}", googleAddress))
                    return false;

                if (result.expires_in < 0)
                    return false;

                if (result.audience != abiokaClientId)
                    return false;

                if (result.email.ToLowerInvariant() != email.ToLowerInvariant())
                    return false;

                return true;
            }
        }

        private class GoogleTokenResult
        {
            public string issuer { get; set; }
            public string issued_to { get; set; }
            public string audience { get; set; }
            public string user_id { get; set; }
            public int expires_in { get; set; }
            public string issued_at { get; set; }
            public string email { get; set; }
            public string email_verified { get; set; }
        }
    }

    public class AuthProviderValidatorFactory
    {
        private static IDictionary<AuthProvider, IAuthProviderValidator> authProviderValidators;

        static AuthProviderValidatorFactory()
        {
            authProviderValidators = new Dictionary<AuthProvider, IAuthProviderValidator>();
            authProviderValidators.Add(AuthProvider.Local, new LocalAuthProviderValidator());
            authProviderValidators.Add(AuthProvider.Google, new GoogleAuthProviderValidator());
        }

        public static IAuthProviderValidator GetAuthProviderValidator(AuthProvider authProvider)
        {
            if (!authProviderValidators.ContainsKey(authProvider))
                throw new ValidationException(ErrorMessage.InvalidProvider);

            return authProviderValidators[authProvider];
        }
    }
}