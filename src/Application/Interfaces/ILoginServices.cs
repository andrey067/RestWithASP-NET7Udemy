using Application.Dtos;

namespace Application.Interfaces
{
    public interface ILoginServices
    {
        Task<TokenDto> Login(UserLogingDto userLoging);
        Task<TokenDto> RefreshToken(TokenDto token);
        Task<bool> RevokeToken(string username);
    }
}
