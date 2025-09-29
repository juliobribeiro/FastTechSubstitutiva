using FastTechKitchen.Domain.Entities.Interfaces;
using FastTechKitchen.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FastTechKitchen.Infraestructure.Data.Repositories;

public abstract class BaseExpressionService<T> where T : class, IBaseEntity
{
    protected readonly ApplicationDBContext Context;

    protected BaseExpressionService(ApplicationDBContext context) : base()
    {
        Context = context;
    }

    protected virtual IQueryable<T> BaseQuery(bool tracking = false)
    {
        var condition = (Expression<Func<T, bool>>)(x => x.Removed == false);
        var query = Context.Set<T>().Where(condition);

        if (tracking)
            query = query.AsTracking();
        else
            query = query.AsNoTracking();

        return query.AsQueryable();
    }
}