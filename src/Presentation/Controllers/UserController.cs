using Application.Dtos;
using Application.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Presentation.Controllers
{
    public static class UserController
    {
        public static IEndpointRouteBuilder MapUserController(this IEndpointRouteBuilder routers)
        {
            routers.MapPost("/", Post);
            routers.MapPost("/refreshToken", RefreshToken);
            routers.MapPost("/revokeToken", RevokeToken).RequireAuthorization();
            return routers;
        }

        public static async Task<IResult> Post([FromServices] ILoginServices service, [FromBody] UserLogingDto userLoging)
        {
            if (userLoging == null) return Results.BadRequest();
            var token = await service.Login(userLoging);
            if (token is null) return Results.Unauthorized();
            return Results.Ok(token);
        }

        //TODO - alterar para receber o token e o refreshToken somente
        public static async Task<IResult> RefreshToken([FromServices] ILoginServices service, [FromBody] TokenDto tokenDto)
        {
            if (tokenDto == null) return Results.BadRequest();
            var token = await service.RefreshToken(tokenDto);
            if (token is null) return Results.BadRequest();
            return Results.Ok(token);
        }

        public static async Task<IResult> RevokeToken([FromServices] ILoginServices service, [FromBody] string username)
        {
            if (string.IsNullOrEmpty(username)) return Results.BadRequest();
            return Results.Ok(await service.RevokeToken(username));
        }
    }
}
