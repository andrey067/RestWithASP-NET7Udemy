using Application.Dtos;
using Domain.Entities;
using Domain.Shared;

namespace Application.Interfaces
{
    public interface IBaseService<TEntity, TResult>
        where TEntity : BaseEntity
        where TResult : BaseDto
    {
        Task<Result> DeleteAsync(long id);
        Task<Result<TResult>> SelectAsync(long id);
        Task<Result<IEnumerable<TResult>>> SelectAllAsync();       
    }
}
