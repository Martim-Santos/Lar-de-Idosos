using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lar_de_Idosos.Migrations
{
    /// <inheritdoc />
    public partial class ApiFotos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "estado",
                table: "Idoso",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "estado",
                table: "Idoso");
        }
    }
}
