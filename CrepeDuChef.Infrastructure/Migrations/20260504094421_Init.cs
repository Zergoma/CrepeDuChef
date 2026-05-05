using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CrepeDuChef.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    OriginDeviceId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CrepesParty",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    SessionNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    OriginDeviceId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CrepesParty", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CrepesParty_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CrepesParty_SessionNumber_UserId_OriginDeviceId",
                table: "CrepesParty",
                columns: new[] { "SessionNumber", "UserId", "OriginDeviceId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CrepesParty_UserId",
                table: "CrepesParty",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CrepesParty");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
