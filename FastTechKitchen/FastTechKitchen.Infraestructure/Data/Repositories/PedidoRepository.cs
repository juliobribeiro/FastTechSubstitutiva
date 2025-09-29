using FastTechKitchen.Domain.Entities;
using FastTechKitchen.Domain.Interfaces.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FastTechKitchen.Infraestructure.Data.Repositories;

public class PedidoRepository(ApplicationDBContext context) : BaseRepository<Pedido>(context), IPedidoRepository
{
    public override async Task<Pedido> GetById(Guid id, bool include = false, bool tracking = false)
    {
        var query = BaseQuery(tracking);

        return await query.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<Pedido>> FindBy(Expression<Func<Pedido, bool>> expression, bool tracking = false)
    {
        var query = BaseQuery(tracking)
            .Where(expression)
            .Where(x => !x.Removed);

        return await query.ToListAsync();
    }


}