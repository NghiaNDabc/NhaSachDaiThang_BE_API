using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NhaSachDaiThang_BE_API.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBookUserSupplierBook : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PurchasePrice",
                table: "Books");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "User",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "SupplierBook",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "SupplierBook");

            migrationBuilder.AddColumn<decimal>(
                name: "PurchasePrice",
                table: "Books",
                type: "decimal(18,2)",
                nullable: true);
        }
    }
}
