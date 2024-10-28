using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NhaSachDaiThang_BE_API.Migrations
{
    /// <inheritdoc />
    public partial class DropForkeyBookReview : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Reviews__BookID__71D1E811",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_BookID",
                table: "Reviews");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Reviews_BookID",
                table: "Reviews",
                column: "BookID");

            migrationBuilder.AddForeignKey(
                name: "FK__Reviews__BookID__71D1E811",
                table: "Reviews",
                column: "BookID",
                principalTable: "Books",
                principalColumn: "BookID");
        }
    }
}
