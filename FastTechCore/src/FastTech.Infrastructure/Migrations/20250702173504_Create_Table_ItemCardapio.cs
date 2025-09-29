using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FastTech.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Create_Table_ItemCardapio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DDD",
                table: "ItemCardapio");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "ItemCardapio");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "ItemCardapio");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "ItemCardapio",
                newName: "Descricao");

            migrationBuilder.AlterColumn<int>(
                name: "PermissionLevel",
                table: "User",
                type: "int",
                nullable: false,
                defaultValue: 2,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 8);

            migrationBuilder.AddColumn<bool>(
                name: "Disponivel",
                table: "ItemCardapio",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Nome",
                table: "ItemCardapio",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "Preco",
                table: "ItemCardapio",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Disponivel",
                table: "ItemCardapio");

            migrationBuilder.DropColumn(
                name: "Nome",
                table: "ItemCardapio");

            migrationBuilder.DropColumn(
                name: "Preco",
                table: "ItemCardapio");

            migrationBuilder.RenameColumn(
                name: "Descricao",
                table: "ItemCardapio",
                newName: "Email");

            migrationBuilder.AlterColumn<int>(
                name: "PermissionLevel",
                table: "User",
                type: "int",
                nullable: false,
                defaultValue: 8,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 2);

            migrationBuilder.AddColumn<int>(
                name: "DDD",
                table: "ItemCardapio",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "ItemCardapio",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "ItemCardapio",
                type: "nvarchar(9)",
                maxLength: 9,
                nullable: true);
        }
    }
}
