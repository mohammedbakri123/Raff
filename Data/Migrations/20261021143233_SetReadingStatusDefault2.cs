using Microsoft.EntityFrameworkCore.Migrations;
using Raff.Models;

#nullable disable

namespace Raff.Data.Migrations
{
    /// <inheritdoc />
    public partial class SetReadingStatusDefault2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<ReadingStatus>(
                name: "status",
                table: "reading_entries",
                type: "reading_status",
                nullable: true,
                defaultValue: ReadingStatus.WANT_TO_READ,
                oldClrType: typeof(ReadingStatus),
                oldType: "reading_status",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<ReadingStatus>(
                name: "status",
                table: "reading_entries",
                type: "reading_status",
                nullable: true,
                oldClrType: typeof(ReadingStatus),
                oldType: "reading_status",
                oldNullable: true,
                oldDefaultValue: ReadingStatus.WANT_TO_READ);
        }
    }
}
