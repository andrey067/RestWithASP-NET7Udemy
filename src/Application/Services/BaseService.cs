using Application.Constants;
using Application.Dtos;
using Application.Interfaces;
using Domain.Entities;
using Domain.Repository;
using Domain.Shared;
using Mapster;

namespace Application.Services
{
    public class BaseService<TEntity, TResult>: IBaseService<TEntity, TResult>
        where TEntity : BaseEntity
        where TResult : BaseDto
    {
        private readonly IRepository<TEntity> _repositorio;
        private readonly ILinkServices _linkServices;

        public BaseService(IRepository<TEntity> repositorio, ILinkServices linkServices)
        {
            _repositorio = repositorio;
            _linkServices = linkServices;
        }

        public async Task<Result> DeleteAsync(long id)
        {
            var existe = await _repositorio.ExistAsync(id);
            if (existe)
            {
                await _repositorio.DeleteAsync(id);
                return Result.Success();
            }

            return Result.Failure();
        }

        public async Task<Result<TResult>> SelectAsync(long id)
        {
            var resultEntity = await _repositorio.SelectAsync(id);

            if (resultEntity is not null)
            {
                var resultDto = AddLinks(resultEntity.Adapt<TResult>());
                return Result<TResult>.Success(resultDto);
            }

            return Result<TResult>.Success();
        }

        public async Task<Result<IEnumerable<TResult>>> SelectAllAsync()
        {
            var result = await _repositorio.SelectAllAsync();

            if (result.Count() > 0)
            {
                var resultDto = result.Adapt<List<TResult>>();
                resultDto.ForEach(r =>
                {
                    if (r is not null)
                        AddLinks(r);
                });

                return Result<IEnumerable<TResult>>.Success(resultDto);
            }
            return Result<IEnumerable<TResult>>.Success();
        }

        private TResult AddLinks(TResult response)
        {
            response.Links.Add(_linkServices.Generete("GetById", new { id = response.Id }, "get-by-id", HttpActionVerb.GET.ToString()));
            response.Links.Add(_linkServices.Generete("Delete", new { id = response.Id }, "delete", HttpActionVerb.DELETE.ToString()));
            return response;
        }
    }
}
