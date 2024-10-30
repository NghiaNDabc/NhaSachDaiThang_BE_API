using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NhaSachDaiThang_BE_API.Migrations
{
    /// <inheritdoc />
    public partial class AddTrackingTimeToSupplier_Supplierbook : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "SupplierBook",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "SupplierBook",
                type: "datetime",
                nullable: true,
                defaultValueSql: "(getdate())");

            migrationBuilder.AddColumn<string>(
                name: "ModifyBy",
                table: "SupplierBook",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifyDate",
                table: "SupplierBook",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Supplier",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Supplier",
                type: "datetime",
                nullable: true,
                defaultValueSql: "(getdate())");

            migrationBuilder.AddColumn<string>(
                name: "ModifyBy",
                table: "Supplier",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifyDate",
                table: "Supplier",
                type: "datetime",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "SupplierBook");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "SupplierBook");

            migrationBuilder.DropColumn(
                name: "ModifyBy",
                table: "SupplierBook");

            migrationBuilder.DropColumn(
                name: "ModifyDate",
                table: "SupplierBook");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Supplier");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Supplier");

            migrationBuilder.DropColumn(
                name: "ModifyBy",
                table: "Supplier");

            migrationBuilder.DropColumn(
                name: "ModifyDate",
                table: "Supplier");
        }
    }
}
