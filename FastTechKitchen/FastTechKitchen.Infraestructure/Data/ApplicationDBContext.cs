using FastTechKitchen.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FastTechKitchen.Infraestructure.Data;

public class ApplicationDBContext : DbContext
{
    public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options) { }

    //// Você precisa de DbSet para as entidades principais para que o EF as reconheça
    //public DbSet<Pedido> Pedidos { get; set; }
    //public DbSet<PedidoItemCardapio> PedidoItensCardapio { get; set; }
    //public DbSet<ItemCardapio> ItensCardapio { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDBContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}