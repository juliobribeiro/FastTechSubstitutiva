#nullable disable
using Microsoft.EntityFrameworkCore.Migrations;


namespace FastTechKitchen.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Alter_Pedido_Add_Ativo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "Pedido",
                type: "bit",
                nullable: false,
                defaultValue: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "Pedido");
        }
    }
}
