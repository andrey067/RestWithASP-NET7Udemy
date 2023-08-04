using Application.Dtos;
using Domain.Entities;
using Mapster;

namespace Infrastructure.Configuration
{
    public class MapsterConfiguration: IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.ForType<Person, PersonDto>().ConstructUsing((src) => new PersonDto(src.Id, src.FirstName, src.LastName, src.Address, src.Gender))
            .Ignore(dest => dest.Links).TwoWays();
            //config.ForType<Book, BookDto>();
        }
    }
}
