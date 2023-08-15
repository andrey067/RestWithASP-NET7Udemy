using Domain.Entities;

namespace Domain.Repository
{
    public interface IPersonRepository
    {
        Task AlterarStatus(Person person, bool status);

        Task<(IEnumerable<Person>?, int Count)> FindByFirtNameOrLastName(string firtName, string lastName, int page = 1, int pageSize = 10);
    }
}
