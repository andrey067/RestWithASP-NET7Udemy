using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Presentation.Controllers
{
    public static class PersonController
    {
        //Alterar Person para uma Dto
        public static IEndpointRouteBuilder MapPersonController(this IEndpointRouteBuilder routers)
        {
            routers.MapGet("/api/person", (IPersonBusiness personBusiness) => personBusiness.FindAll());

            routers.MapGet("/api/person/{id}", (IPersonBusiness personBusiness, long id) =>
            {
                var person = personBusiness.FindByID(id);
                if (person == null)
                {
                    return Results.NotFound();
                }
                return Results.Ok(person);
            });

            routers.MapPost("/api/person", (IPersonBusiness personBusiness, Person person) =>
            {
                if (person == null)
                {
                    return Results.BadRequest();
                }
                return Results.Ok(personBusiness.Create(person));
            });

            routers.MapPut("/api/person", (IPersonBusiness personBusiness, Person person) =>
            {
                if (person == null)
                {
                    return Results.BadRequest();
                }
                return Results.Ok(personBusiness.Update(person));
            });

            routers.MapDelete("/api/person/{id}", (IPersonBusiness personBusiness, long id) =>
            {
                personBusiness.Delete(id);
                return Results.NoContent();
            });

            return routers;
        }
    }
}