using Application.Dtos;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IPersonBusiness : IBaseService<Person, PersonDto>
    {
        Task Create(PersonDto person);
        Task Update(UpdatePersonDto person);
        Task<PersonDto> ChageStatus(long id, bool status);
        Task<(IEnumerable<PersonDto>, int count)> FindByName(string firtsName, string lastName, int page = 1, int pageSize = 10);
    }
}
