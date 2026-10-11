using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Raff.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddCategoryToBooks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "books",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_books_CategoryId",
                table: "books",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_books_Categories_CategoryId",
                table: "books",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_books_Categories_CategoryId",
                table: "books");

            migrationBuilder.DropIndex(
                name: "IX_books_CategoryId",
                table: "books");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "books");
        }
    }
}
