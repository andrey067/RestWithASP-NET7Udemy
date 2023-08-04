namespace Application.Dtos
{
    public record TokenDto(bool Authenticated, DateTime Created, DateTime Expiration, string AcessToken, string RefreshToken);

}
