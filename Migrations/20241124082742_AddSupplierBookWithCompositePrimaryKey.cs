using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NhaSachDaiThang_BE_API.Migrations
{
    /// <inheritdoc />
    public partial class AddSupplierBookWithCompositePrimaryKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {


            // Xóa cột SupplierBookID hiện tại
            migrationBuilder.DropPrimaryKey(
        name: "PK__SupplierBook",
        table: "SupplierBook");
            migrationBuilder.DropColumn(
                name: "SupplierBookID",
                table: "SupplierBook");

            // Tạo lại cột SupplierBookID mà không có IDENTITY
            migrationBuilder.AddColumn<int>(
                name: "SupplierBookID",
                table: "SupplierBook",
                type: "int",
                nullable: false);

            // Đặt khóa chính phức hợp
            migrationBuilder.AddPrimaryKey(
                name: "PK_SupplierBook",
                table: "SupplierBook",
                columns: new[] { "SupplierBookID", "SupplierId", "BookID" });
        }


        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Xóa khóa chính phức hợp
            migrationBuilder.DropPrimaryKey(
                name: "PK_SupplierBook",
                table: "SupplierBook");

            // Xóa cột SupplierBookID
            migrationBuilder.DropColumn(
                name: "SupplierBookID",
                table: "SupplierBook");

            // Tạo lại cột SupplierBookID với IDENTITY
            migrationBuilder.AddColumn<int>(
                name: "SupplierBookID",
                table: "SupplierBook",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            // Đặt lại khóa chính cũ
            migrationBuilder.AddPrimaryKey(
                name: "PK__SupplierBook",
                table: "SupplierBook",
                column: "SupplierBookID");
        }

    }
}
