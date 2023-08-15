using Domain.Entities;
using Domain.Repository;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly RestFullContext _context;
        private readonly DbSet<T> _dataSet;
        public BaseRepository(RestFullContext context)
        {
            _context = context;
            _dataSet = context.Set<T>();
        }
        public async Task DeleteAsync(long id)
        {
            try
            {
                var result = await _dataSet.SingleOrDefaultAsync(p => p.Id.Equals(id));
                if (result != null)
                    _dataSet.Remove(result);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task InsertAsync(T item)
        {
            try
            {
                _dataSet.Add(item);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<T?> SelectAsync(long id)
            => await _dataSet.SingleOrDefaultAsync(p => p.Id.Equals(id));

        public async Task<IEnumerable<T>?> SelectAllAsync()
         => await _dataSet.ToListAsync();

        public async Task UpdateAsync(T item)
        {
            var itemOld = await _dataSet.AsNoTracking().SingleOrDefaultAsync(p => p.Id.Equals(item.Id));

            item.UpdateUpdateAt();

            _context.Entry(itemOld).CurrentValues.SetValues(item);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistAsync(long id) => await _dataSet.AsNoTracking().AnyAsync(p => p.Id.Equals(id));

        public DbSet<T> GetDbSet() => _dataSet;

        public async Task SaveAsync()
         => await _context.SaveChangesAsync();

        public async Task<(IEnumerable<T>?, int count)> SelectByConditionAsync(Expression<Func<T, bool>> condition, int page, int pageSize)
        {
            if (page < 1)
                throw new ArgumentException("Page number should be greater than or equal to 1.");

            if (pageSize < 1)
                throw new ArgumentException("Page size should be greater than or equal to 1.");

            IQueryable<T> query = _dataSet.AsNoTracking().Where(condition);

            int skip = (page - 1) * pageSize;

            List<T> result = await query
                .OrderBy(p => p.Id)
                .Skip(skip)
                .Take(pageSize)
                .ToListAsync();

            return (result, query.Count());
        }
    }
}
