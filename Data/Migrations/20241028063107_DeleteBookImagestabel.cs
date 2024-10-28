using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NhaSachDaiThang_BE_API.Migrations
{
    /// <inheritdoc />
    public partial class DeleteBookImagestabel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookImages");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BookImages",
                columns: table => new
                {
                    ImageID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookID = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    ImageURL = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ModifyBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ModifyDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__BookImag__7516F4EC1E8E4B20", x => x.ImageID);
                    table.ForeignKey(
                        name: "FK__BookImage__BookI__4222D4EF",
                        column: x => x.BookID,
                        principalTable: "Books",
                        principalColumn: "BookID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookImages_BookID",
                table: "BookImages",
                column: "BookID");
        }
    }
}
