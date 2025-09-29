using FastTech.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FastTech.Infrastructure.Data.Configurations;

public class PedidoConfiguration : BaseEntityConfiguration<Pedido>
{
    public override void Configure(EntityTypeBuilder<Pedido> builder)
    {
        base.Configure(builder);

        builder.ToTable("Pedido");

        builder.Property(u => u.ItemCardapioId).IsRequired().HasMaxLength(50);
        builder.Property(u => u.FormaDeEntrega).IsRequired();
        builder.Property(u => u.FormaDeEntrega).IsRequired();

    }
}