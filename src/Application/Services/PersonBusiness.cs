using Application.Constants;
using Application.Dtos;
using Application.Interfaces;
using Domain.Entities;
using Domain.Repository;
using Mapster;

namespace Application.Services
{
    public class PersonBusiness : BaseService<Person, PersonDto>, IPersonBusiness
    {
        private readonly IPersonRepository _personRepository;
        private readonly IRepository<Person> _repositorio;
        private readonly ILinkServices _linkServices;
        public PersonBusiness(IRepository<Person> repositorio,
                              ILinkServices linkServices,
                              IPersonRepository personRepository) : base(repositorio, linkServices)
        {
            _repositorio = repositorio;
            _personRepository = personRepository;
            _linkServices = linkServices;
        }

        public async Task<PersonDto> ChageStatus(long id, bool status)
        {
            var person = await _repositorio.SelectAsync(id);
            if (person is null) return null;

            await _personRepository.AlterarStatus(person, status);

            return person.Adapt<PersonDto>();
        }

        public Task Create(PersonDto person) => throw new NotImplementedException();

        public async Task<(IEnumerable<PersonDto>, int count)> FindByName(string firtsName, string lastName, int page = 1, int pageSize = 10)
        {
            var (personSearchResult, count) = await _personRepository.FindByFirtNameOrLastName(firtsName, lastName, page, pageSize);

            var resultDto = personSearchResult.Adapt<List<PersonDto>>();
            resultDto.ForEach(r =>
            {
                if (r is not null)
                    AddLinks(r);
            });

            return (resultDto, count);
        }

        public Task Update(UpdatePersonDto person) => throw new NotImplementedException();

        private PersonDto AddLinks(PersonDto response)
        {
            response.Links.Add(_linkServices.Generete("GetById", new { id = response.Id }, "get-by-id", HttpActionVerb.GET.ToString()));
            response.Links.Add(_linkServices.Generete("Delete", new { id = response.Id }, "delete", HttpActionVerb.DELETE.ToString()));
            return response;
        }
    }
}
