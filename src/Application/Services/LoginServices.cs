using Application.Dtos;
using Application.Interfaces;
using Application.Options;
using Domain.Entities;
using Domain.Repository;
using Domain.Shared.Interfaces;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Application.Services
{
    public class LoginServices : ILoginServices
    {
        private const string DATE_FORMAT = "yyyy-MM-dd HH:mm:ss";
        private readonly TokenConfigurationOptions _tokenConfiguration;
        private readonly ITokenService _tokenService;
        private readonly IUserRepository _userRepository;

        public LoginServices(IOptions<TokenConfigurationOptions> tokenConfiguration, ITokenService tokenService)
        {
            _tokenConfiguration = tokenConfiguration.Value;
            _tokenService = tokenService;
        }
        
        public async Task<TokenDto> Login(UserLogingDto userLoging)
        {
            var (userIsValid, user) = await _userRepository.ValidateCredential(userLoging.UserName, userLoging.Password);
            if (!userIsValid)
                return null;

            var claims = new List<Claim>()
                {
                    new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                    new(JwtRegisteredClaimNames.UniqueName, userLoging.UserName)
                };

            var acessToken = _tokenService.GenerateAccessToken(claims);
            var refreshToken = _tokenService.GenerateRefreshToken();
            user.UpdateRefreshToken(refreshToken, _tokenConfiguration.DaysToExpiry);            

            DateTime createDate = DateTime.UtcNow;
            DateTime expirationDate = createDate.AddMinutes(_tokenConfiguration.Minutes);

            await _userRepository.RefreshToken(user);

            return new TokenDto(true, createDate, expirationDate, acessToken, refreshToken);
        }
        
        public async Task<TokenDto> RefreshToken(TokenDto token)
        {
            var accssToken = token.AcessToken;
            var refreshToken = token.RefreshToken;

            var principal = _tokenService.GetPrincipalFromExpiredToken(accssToken);

            var userName = principal.Identity!.Name;

            var user = await _userRepository.GetUserByUserName(userName);
            if (ValidateRefreshToken(refreshToken, user))
                return null;

            accssToken = _tokenService.GenerateAccessToken(principal.Claims);

            refreshToken = _tokenService.GenerateRefreshToken();
            DateTime createDate = DateTime.UtcNow;
            DateTime expirationDate = createDate.AddMinutes(_tokenConfiguration.Minutes);

            await _userRepository.RefreshToken(user);

            return new TokenDto(true, createDate, expirationDate, accssToken, refreshToken);

        }

        private static bool ValidateRefreshToken(string refreshToken, User? user)
            => user is null || user.RefreshToken != refreshToken || user.RefreshTokenExpyTime <= DateTime.UtcNow;
    }
}
