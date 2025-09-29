using FastTechKitchen.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FastTechKitchen.Infraestructure.Data.Configurations;

public class ItemCardapioConfiguration : BaseEntityConfiguration<ItemCardapio>
{
    public override void Configure(EntityTypeBuilder<ItemCardapio> builder)
    {
        base.Configure(builder);

        builder.ToTable("ItemCardapio");

        builder.Property(u => u.Nome).IsRequired().HasMaxLength(50);
        builder.Property(u => u.Descricao).IsRequired().HasMaxLength(150);
        builder.Property(u => u.Preco).IsRequired(true);
        builder.Property(u => u.Disponivel).IsRequired(true);
    }
}