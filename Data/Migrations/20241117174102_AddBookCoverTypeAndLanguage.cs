using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NhaSachDaiThang_BE_API.Migrations
{
    /// <inheritdoc />
    public partial class AddBookCoverTypeAndLanguage2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BookCoverTypeId",
                table: "Book",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LanguageId",
                table: "Book",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BookCoverType",
                columns: table => new
                {
                    BookCoverTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookCoverType", x => x.BookCoverTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Language",
                columns: table => new
                {
                    LanguageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Language", x => x.LanguageId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Book_BookCoverTypeId",
                table: "Book",
                column: "BookCoverTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Book_LanguageId",
                table: "Book",
                column: "LanguageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Book_BookCoverType_BookCoverTypeId",
                table: "Book",
                column: "BookCoverTypeId",
                principalTable: "BookCoverType",
                principalColumn: "BookCoverTypeId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Book_Language_LanguageId",
                table: "Book",
                column: "LanguageId",
                principalTable: "Language",
                principalColumn: "LanguageId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Book_BookCoverType_BookCoverTypeId",
                table: "Book");

            migrationBuilder.DropForeignKey(
                name: "FK_Book_Language_LanguageId",
                table: "Book");

            migrationBuilder.DropTable(
                name: "BookCoverType");

            migrationBuilder.DropTable(
                name: "Language");

            migrationBuilder.DropIndex(
                name: "IX_Book_BookCoverTypeId",
                table: "Book");

            migrationBuilder.DropIndex(
                name: "IX_Book_LanguageId",
                table: "Book");

            migrationBuilder.DropColumn(
                name: "BookCoverTypeId",
                table: "Book");

            migrationBuilder.DropColumn(
                name: "LanguageId",
                table: "Book");
        }
    }
}
