using FastTechKitchen.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FastTechKitchen.Infraestructure.Data.Configurations;

public class PedidoItemCardapioConfiguration : BaseEntityConfiguration<PedidoItemCardapio>
{
    public override void Configure(EntityTypeBuilder<PedidoItemCardapio> builder)
    {
        base.Configure(builder);

        builder.ToTable("PedidoItemCardapio");

        // Propriedades obrigatórias
        builder.Property(u => u.ItemCardapioId).IsRequired();
        builder.Property(u => u.PedidoId).IsRequired();

        // Relacionamento com Pedido (N:1)
        builder.HasOne<Pedido>()
            .WithMany()
            .HasForeignKey(u => u.PedidoId)
            .OnDelete(DeleteBehavior.Restrict); 

        // Relacionamento com ItemCardapio (N:1)
        builder.HasOne<ItemCardapio>()
            .WithMany()
            .HasForeignKey(u => u.ItemCardapioId)
            .OnDelete(DeleteBehavior.Restrict);

        // Chave composta
        builder.HasIndex(u => new { u.PedidoId, u.ItemCardapioId }).IsUnique();

    }
}