using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NhaSachDaiThang_BE_API.Migrations
{
    /// <inheritdoc />
    public partial class AddFileToBook : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AdditionalImages",
                table: "Books",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PageCount",
                table: "Books",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Size",
                table: "Books",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Weight",
                table: "Books",
                type: "decimal(5,2)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdditionalImages",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "PageCount",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "Size",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "Books");
        }
    }
}
