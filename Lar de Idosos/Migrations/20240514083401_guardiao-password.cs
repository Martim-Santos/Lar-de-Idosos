using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lar_de_Idosos.Migrations
{
    /// <inheritdoc />
    public partial class guardiaopassword : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Guardiao",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "Guardiao");
        }
    }
}
