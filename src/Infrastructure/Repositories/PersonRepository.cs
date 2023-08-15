using Domain.Entities;
using Domain.Repository;
using Infrastructure.Context;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly RestFullContext _context;
        private readonly IRepository<Person> _repository;

        public PersonRepository(RestFullContext context, IRepository<Person> repository)
        {
            _context = context;
            _repository = repository;
        }

        public async Task AlterarStatus(Person person, bool status)
        {
            person.ChangeStatus(status);
            _context.Update(person);
            await _context.SaveChangesAsync();
        }

        public async Task<(IEnumerable<Person>?, int Count)> FindByFirtNameOrLastName(string firstName, string lastName, int page = 1, int pageSize = 10)
        {
            Expression<Func<Person, bool>> condition = p =>
            (string.IsNullOrEmpty(firstName) || p.FirstName.Contains(firstName)) &&
            (string.IsNullOrEmpty(lastName) || p.LastName.Contains(lastName));         

            return await _repository.SelectByConditionAsync(condition, page, pageSize);
        }
    }
}
