using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

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

        Task<(IEnumerable<T>?, int count)> SelectByConditionAsync(Expression<Func<T, bool>> condition, int page, int pageSize);
    }
}
