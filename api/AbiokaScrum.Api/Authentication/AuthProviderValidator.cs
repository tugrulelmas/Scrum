using AbiokaScrum.Api.Entities;
using AbiokaScrum.Api.Exceptions;
using AbiokaScrum.Api.Helper;
using AbiokaScrum.Api.Service;
using System.Collections.Generic;

namespace AbiokaScrum.Api.Authentication
{
    public interface IAuthProviderValidator
    {
        bool IsValid(string code, string token);
    }

    public class LocalAuthProviderValidator : IAuthProviderValidator
    {
        public bool IsValid(string code, string token)
        {
            var dbUser = DBService.GetByKey<User>(code);
            var result = dbUser != null && dbUser.Token == token;
            return result;
        }
    }

    public class GoogleAuthProviderValidator : IAuthProviderValidator
    {
        public bool IsValid(string code, string token)
        {
            //TODO: implement google token validation
            return true;
        }
    }

    public class AuthProviderValidatorFactory
    {
        private static Dictionary<AuthProvider, IAuthProviderValidator> authProviderValidators;

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