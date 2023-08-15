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
        private readonly TokenConfigurationOptions _tokenConfiguration;
        private readonly ITokenService _tokenService;
        private readonly IUserRepository _userRepository;
        private readonly IRepository<User> _repository;

        public LoginServices(IOptions<TokenConfigurationOptions> tokenConfiguration, ITokenService tokenService, IRepository<User> repository, IUserRepository userRepository)
        {
            _tokenConfiguration = tokenConfiguration.Value;
            _tokenService = tokenService;
            _repository = repository;
            _userRepository = userRepository;
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

            await _repository.UpdateAsync(user);

            return new TokenDto(true, createDate, expirationDate, acessToken, refreshToken);
        }

        public async Task<TokenDto> RefreshToken(TokenDto token)
        {
            var accssToken = token.AcessToken;
            var refreshToken = token.RefreshToken;

            var principal = _tokenService.GetPrincipalFromExpiredToken(accssToken);

            var userName = principal.Identity!.Name;

            var user = await _userRepository.GetUserByUserName(userName!);
            if (ValidateRefreshToken(refreshToken, user))
                return null;

            accssToken = _tokenService.GenerateAccessToken(principal.Claims);
            refreshToken = _tokenService.GenerateRefreshToken();

            user.UpdateRefreshToken(refreshToken, _tokenConfiguration.DaysToExpiry);
            await _repository.UpdateAsync(user!);

            DateTime createDate = DateTime.UtcNow;
            DateTime expirationDate = createDate.AddMinutes(_tokenConfiguration.Minutes);

            return new TokenDto(true, createDate, expirationDate, accssToken, refreshToken);
        }

        public async Task<bool> RevokeToken(string username)
        {
            var user = await _userRepository.GetUserByUserName(username);
            if (user is null)
                return false;

            user.RevokeToken();
            await _repository.UpdateAsync(user);
            return true;
        }

        private static bool ValidateRefreshToken(string refreshToken, User? user)
            => user is null || user.RefreshToken != refreshToken || user.RefreshTokenExpyTime <= DateTime.UtcNow;
    }
}
