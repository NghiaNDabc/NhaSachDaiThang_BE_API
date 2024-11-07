using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NhaSachDaiThang_BE_API.Migrations
{
    /// <inheritdoc />
    public partial class AddSupplierBookIdTSupplierbooktable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK__SupplierBook",
                table: "SupplierBook");

            migrationBuilder.AddColumn<int>(
                name: "SupplierBookID",
                table: "SupplierBook",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK__SupplierBook",
                table: "SupplierBook",
                column: "SupplierBookID");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierBook_SupplierId",
                table: "SupplierBook",
                column: "SupplierId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK__SupplierBook",
                table: "SupplierBook");

            migrationBuilder.DropIndex(
                name: "IX_SupplierBook_SupplierId",
                table: "SupplierBook");

            migrationBuilder.DropColumn(
                name: "SupplierBookID",
                table: "SupplierBook");

            migrationBuilder.AddPrimaryKey(
                name: "PK__SupplierBook",
                table: "SupplierBook",
                columns: new[] { "SupplierId", "BookID" });
        }
    }
}
