using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Domain.Repository
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task InsertAsync(T item);

        Task UpdateAsync(T item);

        Task DeleteAsync(long id);

        Task<T> SelectAsync(long id);

        Task<IEnumerable<T>> SelectAllAsync();

        Task<bool> ExistAsync(long id);

        DbSet<T> GetDbSet();
        Task SaveAsync();
    }
}
