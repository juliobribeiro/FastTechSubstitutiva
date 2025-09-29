using FastTechKitchen.Domain.Entities;
using FastTechKitchen.Domain.Interfaces.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace FastTechKitchen.Infraestructure.Data.Repositories;

public class ItemCardapioRepository(ApplicationDBContext context) : BaseRepository<ItemCardapio>(context), IItemCardapioRepository
{
    public override async Task<ItemCardapio> GetById(Guid id, bool include = false, bool tracking = false)
    {
        var query = BaseQuery(tracking);

        return await query.FirstOrDefaultAsync(x => x.Id == id);
    }
}