using FastTechKitchen.Domain.Entities;
using FastTechKitchen.Domain.Interfaces.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace FastTechKitchen.Infraestructure.Data.Repositories;

public class PedidoItemCardapioRepository(ApplicationDBContext context) : BaseRepository<PedidoItemCardapio>(context), IPedidoItemCardapioRepository
{
    public override async Task<PedidoItemCardapio> GetById(Guid id, bool include = false, bool tracking = false)
    {
        var query = BaseQuery(tracking);

        return await query.FirstOrDefaultAsync(x => x.Id == id);
    }
}