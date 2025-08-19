using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Management_Hotel_2025.Migrations
{
    /// <inheritdoc />
    public partial class v31 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Token",
                columns: table => new
                {
                    IdToken = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ToketContent = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Daycre = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DayExpired = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdUser = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Token", x => x.IdToken);
                    table.ForeignKey(
                        name: "FK_Token_users_IdUser",
                        column: x => x.IdUser,
                        principalTable: "users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Token_IdUser",
                table: "Token",
                column: "IdUser");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Token");
        }
    }
}
