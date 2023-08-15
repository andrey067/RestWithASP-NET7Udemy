using Application.Dtos;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Presentation.Controllers
{
    public static class PersonController
    {
        public static IEndpointRouteBuilder MapPersonController(this IEndpointRouteBuilder routers)
        {
            routers.MapBaseController<Person, PersonDto>();

            routers.MapPost("/", Post)
                   .WithName("CreatePerson");

            routers.MapPut($"/update-{typeof(Person).Name.ToLower()}", Put)
                   .WithName($"Update{nameof(Person)}");

            routers.MapPatch($"/chagestatus", Patch)
                   .WithName($"ChageStatus");

            routers.MapGet("/findbyname/{pageSize}/{page}", FindByName)
                   .WithName("FindByName");
            return routers;
        }

        public static async Task<IResult> Post([FromServices] IPersonBusiness personBusiness, [FromBody] PersonDto person)
        {
            if (person == null)
            {
                return Results.BadRequest();
            }
            await personBusiness.Create(person);
            return Results.Ok();
        }

        public static async Task<IResult> Put([FromServices] IPersonBusiness personBusiness, [FromBody] UpdatePersonDto person)
        {
            if (person == null)
            {
                return Results.BadRequest();
            }
            await personBusiness.Update(person);
            return Results.Ok();
        }

        public static async Task<IResult> Patch([FromServices] IPersonBusiness personBusiness, [FromBody] long id, bool status)
        {
            var person = await personBusiness.ChageStatus(id, status);
            return Results.Ok(person);
        }

        public static async Task<IResult> FindByName([FromServices] IPersonBusiness personBusiness, [FromQuery] string? firtsName, string? lastName, int page, int pageSize)
        {
            var (person, count) = await personBusiness.FindByName(firtsName, lastName, page, pageSize);
            return Results.Ok(new { data = person, Count = count });
        }
    }
}
