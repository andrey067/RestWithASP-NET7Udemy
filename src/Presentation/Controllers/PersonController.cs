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

            routers.MapPost("/", Post).WithName("CreatePerson");

            routers.MapPut($"/update-{typeof(Person).Name.ToLower()}", Put)
                   .WithName($"Update{nameof(Person)}");

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
    }
}
