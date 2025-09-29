using FastTechKitchen.Domain.Entities;
using FastTechKitchen.Domain.Entities.Interfaces;
using FastTechKitchen.Domain.Interfaces;
using FastTechKitchen.Domain.Interfaces.Infrastructure;
using System.Linq.Expressions;

namespace FastTechKitchen.Domain.Services
{
    public abstract class BaseService<T>(
        IRepository<T> repository,
        UserData userData) : IBaseService<T> where T : class, IBaseEntity
    {
        protected readonly IRepository<T> _repository = repository;
        protected readonly UserData _userData = userData;

        public virtual async Task<T> Add(T entity)
        {
            entity.PrepareToInsert(_userData.Id);
            return await _repository.Add(entity);
        }

        public async Task Delete(Guid id)
        {
            await _repository.Delete(id);
        }

        public async Task Remove(T entity)
        {
            entity.PrepareToRemove(_userData.Id);
            await _repository.Update(entity);
        }

        public IEnumerable<T> GetAll()
        {
            return _repository.GetAll();
        }

        public virtual async Task<T> Update(T entity)
        {
            entity.PrepareToUpdate(_userData.Id);
            return await _repository.Update(entity);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);

            _repository.Dispose();
        }

        public IQueryable<T> FindBy(Expression<Func<T, bool>> expression)
        {
            return _repository.FindBy(expression);
        }

        public void DetachAsync(T entity)
        {
            _repository.DetachAsync(entity);
        }
    }
}