using FastTechKitchen.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FastTechKitchen.Infraestructure.Data.Configurations;

public class PedidoConfiguration : BaseEntityConfiguration<Pedido>
{
    public override void Configure(EntityTypeBuilder<Pedido> builder)
    {
        base.Configure(builder);

        builder.ToTable("Pedido");

        builder.Property(u => u.FormaDeEntrega).IsRequired();
        builder.Property(u => u.Ativo).IsRequired();

    }
}