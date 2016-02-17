using AbiokaScrum.Api.Entities;

namespace AbiokaScrum.Api.Data
{
    public interface IUserOperation : IOperation<User>
    {
        User GetByEmail(string email);
    }
}