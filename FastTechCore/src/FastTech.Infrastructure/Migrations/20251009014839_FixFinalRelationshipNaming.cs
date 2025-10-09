using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FastTech.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixFinalRelationshipNaming : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ItemCardapioId",
                table: "Pedido");

            migrationBuilder.AddColumn<Guid>(
                name: "PedidoId",
                table: "Pedido",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "PedidoItemCardapio",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PedidoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ItemCardapioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantidade = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Removed = table.Column<bool>(type: "bit", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RemovedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RemovedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PedidoItemCardapio", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PedidoItemCardapio_ItemCardapio_ItemCardapioId",
                        column: x => x.ItemCardapioId,
                        principalTable: "ItemCardapio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PedidoItemCardapio_Pedido_PedidoId",
                        column: x => x.PedidoId,
                        principalTable: "Pedido",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PedidoItemCardapio_ItemCardapioId",
                table: "PedidoItemCardapio",
                column: "ItemCardapioId");

            migrationBuilder.CreateIndex(
                name: "IX_PedidoItemCardapio_PedidoId",
                table: "PedidoItemCardapio",
                column: "PedidoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PedidoItemCardapio");

            migrationBuilder.DropColumn(
                name: "PedidoId",
                table: "Pedido");

            migrationBuilder.AddColumn<Guid>(
                name: "ItemCardapioId",
                table: "Pedido",
                type: "uniqueidentifier",
                maxLength: 50,
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
