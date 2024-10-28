using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NhaSachDaiThang_BE_API.Migrations
{
    /// <inheritdoc />
    public partial class DeletePromotionAndPromotionBookTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookPromotions");

            migrationBuilder.DropTable(
                name: "Promotions");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Promotions",
                columns: table => new
                {
                    PromotionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    Discount = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: true),
                    ModifyBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ModifyDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Promotio__52C42F2F433C3844", x => x.PromotionID);
                });

            migrationBuilder.CreateTable(
                name: "BookPromotions",
                columns: table => new
                {
                    BookID = table.Column<int>(type: "int", nullable: false),
                    PromotionID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__BookProm__D8CC80D5FD16A69C", x => new { x.BookID, x.PromotionID });
                    table.ForeignKey(
                        name: "FK__BookPromo__BookI__48CFD27E",
                        column: x => x.BookID,
                        principalTable: "Books",
                        principalColumn: "BookID");
                    table.ForeignKey(
                        name: "FK__BookPromo__Promo__49C3F6B7",
                        column: x => x.PromotionID,
                        principalTable: "Promotions",
                        principalColumn: "PromotionID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookPromotions_PromotionID",
                table: "BookPromotions",
                column: "PromotionID");
        }
    }
}
