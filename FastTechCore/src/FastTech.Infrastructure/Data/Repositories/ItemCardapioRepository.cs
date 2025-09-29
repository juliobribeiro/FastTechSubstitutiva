using FastTech.Domain.Entities;
using FastTech.Domain.Interfaces.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace FastTech.Infrastructure.Data.Repositories;

public class ItemCardapioRepository(ApplicationDBContext context) : BaseRepository<ItemCardapio>(context), IItemCardapioRepository
{
    public override async Task<ItemCardapio> GetById(Guid id, bool include = false, bool tracking = false)
    {
        var query = BaseQuery(tracking);

        return await query.FirstOrDefaultAsync(x => x.Id == id);
    }
}