using Application.Dtos;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Presentation.Controllers
{
    public static class BaseController
    {
        public static IEndpointRouteBuilder MapBaseController<TEntity, TResult>(this IEndpointRouteBuilder routes)
            where TEntity : BaseEntity
            where TResult : BaseDto
        {
            string controllerName = typeof(TEntity).Name.ToLower();

            routes.MapGet($"/get-all", GetAll<TEntity, TResult>)
                  .WithName($"GetAll{controllerName}");

            routes.MapGet("/{id}", GetById<TEntity, TResult>)
                  .WithName($"GetById");

            routes.MapDelete("/{id}", Delete<TEntity, TResult>)
                  .WithName($"Delete");

            return routes;
        }

        public static async Task<IResult> GetAll<TEntity, TResult>([FromServices] IBaseService<TEntity, TResult> services)
            where TEntity : BaseEntity
            where TResult : BaseDto
        {
            var users = await services.SelectAllAsync();
            if (users.IsSuccess && users.Errors.Count() > 0) return Results.NotFound(users);

            if (!users.IsSuccess && users.Errors.Count() > 0) return Results.BadRequest(users);
            return Results.Ok(users);
        }

        public static async Task<IResult> GetById<TEntity, TResult>([FromServices] IBaseService<TEntity, TResult> services,
                                                                    [FromQuery] long id)
            where TEntity : BaseEntity
            where TResult : BaseDto
        {
            var user = await services.SelectAsync(id);
            if (!user.IsSuccess && user.Data == null)
                return Results.NotFound(user);

            return Results.Ok(user);
        }

        public static async Task<IResult> Delete<TEntity, TResult>([FromServices] IBaseService<TEntity, TResult> services,
                                                                       [FromQuery] long Id)
        where TEntity : BaseEntity
        where TResult : BaseDto
        {
            var result = await services.DeleteAsync(Id);
            if (result.IsSuccess)
                return Results.NoContent();
            else

                return Results.BadRequest(result);
        }
    }
}
