using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Management_Hotel_2025.Migrations
{
    /// <inheritdoc />
    public partial class v02 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "UQ__users__F3DBC572794F2AF0",
                table: "users",
                column: "username",
                unique: true);
        }
    }
}
