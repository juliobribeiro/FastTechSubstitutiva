using FastTechKitchen.Domain.Entities.Interfaces;
using FastTechKitchen.Domain.Interfaces.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FastTechKitchen.Infraestructure.Data.Repositories;

public abstract class BaseRepository<T>(ApplicationDBContext context) : BaseExpressionService<T>(context), IRepository<T> where T : class, IBaseEntity
{
    public abstract Task<T> GetById(Guid id, bool include, bool tracking);

    public virtual async Task<T> Add(T entity)
    {
        try
        {
            Context.Set<T>().Add(entity);
            await Context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Erro no Add: " + ex.Message);
            throw;
        }

        return entity;
    }
    public void DetachAsync(T entity)
    {
        Context.Entry(entity).State = EntityState.Detached;
    }


    public virtual async Task<T> Update(T entity)
    {
        Context.Set<T>().Update(entity);
        await Context.SaveChangesAsync();

        return entity;
    }

    public virtual async Task Delete(Guid id)
    {
        Context.Remove(id);
        await Context.SaveChangesAsync();
    }

    public virtual IEnumerable<T> GetAll()
    {
        return Context.Set<T>().Where(x => x.Removed == false).ToList();
    }

    public async Task<IEnumerable<T>> AddRange(IEnumerable<T> entities)
    {
        Context.Set<T>().AddRange(entities);
        await Context.SaveChangesAsync();

        return entities;
    }

    public virtual IQueryable<T> FindBy(Expression<Func<T, bool>> expression)
    {
        return Context.Set<T>().Where(expression).Where(x => x.Removed == false).AsQueryable();
    }

    public virtual void Dispose()
    {
        Context.Dispose();
    }
}