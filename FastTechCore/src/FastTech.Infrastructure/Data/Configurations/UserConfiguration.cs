using FastTech.Domain.Entities;
using FastTech.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FastTech.Infrastructure.Data.Configurations;

public class UserConfiguration : BaseEntityConfiguration<User>
{
    public override void Configure(EntityTypeBuilder<User> builder)
    {
        base.Configure(builder);

        builder.ToTable("User");

        builder.Property(u => u.Name).IsRequired().HasMaxLength(100);
        builder.Property(u => u.Email).IsRequired().HasMaxLength(150);
        builder.Property(u => u.Password).IsRequired(false).HasMaxLength(256);
        builder.Property(u => u.PermissionLevel).HasDefaultValue(PermissionLevel.Funcionario).HasColumnType("int");
        builder.Property(u => u.DDD).IsRequired(true);
        builder.Property(u => u.PhoneNumber).IsRequired(false).HasMaxLength(9);
    }
}