using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NhaSachDaiThang_BE_API.Migrations
{
    /// <inheritdoc />
    public partial class RenamePublicDateToPublicYear : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PublishDate",
                table: "Book");

            migrationBuilder.AddColumn<int>(
                name: "PublishYear",
                table: "Book",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PublishYear",
                table: "Book");

            migrationBuilder.AddColumn<DateOnly>(
                name: "PublishDate",
                table: "Book",
                type: "date",
                nullable: true);
        }
    }
}
