using Application.Dtos;
using Application.Interfaces;
using Domain.Entities;
using Domain.Repository;

namespace Application.Services
{
    public class PersonBusiness: BaseService<Person, PersonDto>, IPersonBusiness
    {
        public PersonBusiness(IRepository<Person> repositorio,
                              ILinkServices linkServices) : base(repositorio, linkServices) { }

        public Task Create(PersonDto person) => throw new NotImplementedException();
        public Task Update(UpdatePersonDto person) => throw new NotImplementedException();
    }
}
