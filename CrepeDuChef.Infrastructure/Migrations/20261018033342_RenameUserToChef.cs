using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CrepeDuChef.Infrastructure.Migrations
{
    /// <inheritdoc />
    /// <remarks>
    /// Hand-written renames: the scaffolded version dropped and recreated the tables, losing all data.
    /// </remarks>
    public partial class RenameUserToChef : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CrepesParty_Users_UserId",
                table: "CrepesParty");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CrepesParty",
                table: "CrepesParty");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "Chefs");

            migrationBuilder.RenameTable(
                name: "CrepesParty",
                newName: "CrepeParties");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "CrepeParties",
                newName: "ChefId");

            migrationBuilder.RenameIndex(
                name: "IX_CrepesParty_UserId",
                table: "CrepeParties",
                newName: "IX_CrepeParties_ChefId");

            migrationBuilder.RenameIndex(
                name: "IX_CrepesParty_SessionNumber_UserId_OriginDeviceId",
                table: "CrepeParties",
                newName: "IX_CrepeParties_SessionNumber_ChefId_OriginDeviceId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Chefs",
                table: "Chefs",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CrepeParties",
                table: "CrepeParties",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CrepeParties_Chefs_ChefId",
                table: "CrepeParties",
                column: "ChefId",
                principalTable: "Chefs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CrepeParties_Chefs_ChefId",
                table: "CrepeParties");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CrepeParties",
                table: "CrepeParties");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Chefs",
                table: "Chefs");

            migrationBuilder.RenameTable(
                name: "Chefs",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "CrepeParties",
                newName: "CrepesParty");

            migrationBuilder.RenameColumn(
                name: "ChefId",
                table: "CrepesParty",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_CrepeParties_ChefId",
                table: "CrepesParty",
                newName: "IX_CrepesParty_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_CrepeParties_SessionNumber_ChefId_OriginDeviceId",
                table: "CrepesParty",
                newName: "IX_CrepesParty_SessionNumber_UserId_OriginDeviceId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CrepesParty",
                table: "CrepesParty",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CrepesParty_Users_UserId",
                table: "CrepesParty",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
