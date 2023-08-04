using Application.Dtos;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IPersonBusiness: IBaseService<Person, PersonDto>
    {
        Task Create(PersonDto person);
        Task Update(UpdatePersonDto person);
    }
}
