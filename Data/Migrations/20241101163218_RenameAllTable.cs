using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NhaSachDaiThang_BE_API.Migrations
{
    /// <inheritdoc />
    public partial class RenameAllTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_SupplierBook_Books_BookID",
            //    table: "SupplierBook");

            migrationBuilder.RenameTable(
                name: "Vouchers",
                newName: "Voucher");

            migrationBuilder.RenameTable(
                name: "Roles",
                newName: "Role");

            migrationBuilder.RenameTable(
                name: "Reviews",
                newName: "Review");

            migrationBuilder.RenameTable(
                name: "Payments",
                newName: "Payment");

            migrationBuilder.RenameTable(
                name: "Orders",
                newName: "Order");

            migrationBuilder.RenameTable(
                name: "OrderDetails",
                newName: "OrderDetail");

            migrationBuilder.RenameTable(
                name: "Categories",
                newName: "Category");

            migrationBuilder.RenameTable(
                name: "Books",
                newName: "Book");

            //migrationBuilder.RenameIndex(
            //    name: "IX_Reviews_BookID",
            //    table: "Review",
            //    newName: "IX_Review_BookID");

            //migrationBuilder.RenameIndex(
            //    name: "IX_Payments_OrderID",
            //    table: "Payment",
            //    newName: "IX_Payment_OrderID");

            //migrationBuilder.RenameIndex(
            //    name: "IX_Orders_UserId",
            //    table: "Order",
            //    newName: "IX_Order_UserId");

            //migrationBuilder.RenameIndex(
            //    name: "IX_OrderDetails_OrderID",
            //    table: "OrderDetail",
            //    newName: "IX_OrderDetail_OrderID");

            //migrationBuilder.RenameIndex(
            //    name: "IX_OrderDetails_BookID",
            //    table: "OrderDetail",
            //    newName: "IX_OrderDetail_BookID");

            //migrationBuilder.RenameIndex(
            //    name: "IX_Books_CategoryID",
            //    table: "Book",
            //    newName: "IX_Book_CategoryID");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_SupplierBook_Book_BookID",
            //    table: "SupplierBook",
            //    column: "BookID",
            //    principalTable: "Book",
            //    principalColumn: "BookID",
            //    onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_SupplierBook_Book_BookID",
            //    table: "SupplierBook");

            migrationBuilder.RenameTable(
                name: "Voucher",
                newName: "Vouchers");

            migrationBuilder.RenameTable(
                name: "Role",
                newName: "Roles");

            migrationBuilder.RenameTable(
                name: "Review",
                newName: "Reviews");

            migrationBuilder.RenameTable(
                name: "Payment",
                newName: "Payments");

            migrationBuilder.RenameTable(
                name: "OrderDetail",
                newName: "OrderDetails");

            migrationBuilder.RenameTable(
                name: "Order",
                newName: "Orders");

            migrationBuilder.RenameTable(
                name: "Category",
                newName: "Categories");

            migrationBuilder.RenameTable(
                name: "Book",
                newName: "Books");

            //migrationBuilder.RenameIndex(
            //    name: "IX_Review_BookID",
            //    table: "Reviews",
            //    newName: "IX_Reviews_BookID");

            //migrationBuilder.RenameIndex(
            //    name: "IX_Payment_OrderID",
            //    table: "Payments",
            //    newName: "IX_Payments_OrderID");

            //migrationBuilder.RenameIndex(
            //    name: "IX_OrderDetail_OrderID",
            //    table: "OrderDetails",
            //    newName: "IX_OrderDetails_OrderID");

            //migrationBuilder.RenameIndex(
            //    name: "IX_OrderDetail_BookID",
            //    table: "OrderDetails",
            //    newName: "IX_OrderDetails_BookID");

            //migrationBuilder.RenameIndex(
            //    name: "IX_Order_UserId",
            //    table: "Orders",
            //    newName: "IX_Orders_UserId");

            //migrationBuilder.RenameIndex(
            //    name: "IX_Book_CategoryID",
            //    table: "Books",
            //    newName: "IX_Books_CategoryID");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_SupplierBook_Books_BookID",
            //    table: "SupplierBook",
            //    column: "BookID",
            //    principalTable: "Books",
            //    principalColumn: "BookID",
            //    onDelete: ReferentialAction.Restrict);
        }
    }
}
