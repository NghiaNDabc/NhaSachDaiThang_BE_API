using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NhaSachDaiThang_BE_API.Migrations
{
    /// <inheritdoc />
    public partial class CreateFkBook_Review : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddForeignKey(
                name: "FK__Reviews__BookID__71D1E811",
                table: "Reviews",
                column: "BookID",
                principalTable: "Books",
                principalColumn: "BookID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Reviews__BookID__71D1E811",
                table: "Reviews");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Books_BookID",
                table: "Reviews",
                column: "BookID",
                principalTable: "Books",
                principalColumn: "BookID");
        }
    }
}
