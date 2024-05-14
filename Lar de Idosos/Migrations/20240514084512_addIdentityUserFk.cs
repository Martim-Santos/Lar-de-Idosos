using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lar_de_Idosos.Migrations
{
    /// <inheritdoc />
    public partial class addIdentityUserFk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "Guardiao");

            migrationBuilder.AddColumn<string>(
                name: "IdentityUserFK",
                table: "Guardiao",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Guardiao_IdentityUserFK",
                table: "Guardiao",
                column: "IdentityUserFK");

            migrationBuilder.AddForeignKey(
                name: "FK_Guardiao_AspNetUsers_IdentityUserFK",
                table: "Guardiao",
                column: "IdentityUserFK",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Guardiao_AspNetUsers_IdentityUserFK",
                table: "Guardiao");

            migrationBuilder.DropIndex(
                name: "IX_Guardiao_IdentityUserFK",
                table: "Guardiao");

            migrationBuilder.DropColumn(
                name: "IdentityUserFK",
                table: "Guardiao");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Guardiao",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
