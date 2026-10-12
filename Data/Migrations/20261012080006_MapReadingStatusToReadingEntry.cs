using Microsoft.EntityFrameworkCore.Migrations;
using Raff.Models;

#nullable disable

namespace Raff.Data.Migrations
{
    /// <inheritdoc />
    public partial class MapReadingStatusToReadingEntry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("ALTER TABLE reading_entries ADD COLUMN IF NOT EXISTS status reading_status;");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "status",
                table: "reading_entries");
        }
    }
}
