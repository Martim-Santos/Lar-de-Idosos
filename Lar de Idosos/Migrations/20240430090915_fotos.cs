using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lar_de_Idosos.Migrations
{
    /// <inheritdoc />
    public partial class fotos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Foto",
                table: "Trabalhador",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Foto",
                table: "Idoso",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Foto",
                table: "Trabalhador");

            migrationBuilder.DropColumn(
                name: "Foto",
                table: "Idoso");
        }
    }
}
