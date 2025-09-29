using Microsoft.EntityFrameworkCore.Migrations;
using System.Text;

#nullable disable

namespace FastTech.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Add_entity_StateDDD : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StateDDD",
                columns: table => new
                {
                    DDD = table.Column<int>(type: "int", nullable: false),
                    State = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Region = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StateDDD", x => x.DDD);
                });

            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Migrations/scripts", "20241031004359_Add_entity_StateDDD.sql");
            var sqlScript = File.ReadAllText(filePath, Encoding.UTF8);
            migrationBuilder.Sql(sqlScript);

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StateDDD");
        }
    }
}
