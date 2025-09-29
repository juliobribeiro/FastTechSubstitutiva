using FastTech.Domain.Entities;
using FastTech.Domain.Interfaces.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace FastTech.Infrastructure.Data.Repositories;

public class PedidoRepository(ApplicationDBContext context) : BaseRepository<Pedido>(context), IPedidoRepository
{
    public override async Task<Pedido> GetById(Guid id, bool include = false, bool tracking = false)
    {
        var query = BaseQuery(tracking);

        return await query.FirstOrDefaultAsync(x => x.Id == id);
    }
}