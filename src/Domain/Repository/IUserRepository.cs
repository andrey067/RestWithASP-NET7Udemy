using Domain.Entities;

namespace Domain.Repository
{
    public interface IUserRepository
    {
        User SaveUser(User user);
        Task<(bool, User?)> ValidateCredential(string userName, string userPassword);
        Task<User?> GetUserByUserName(string userName);
        Task RefreshToken(User user);
    }
}
