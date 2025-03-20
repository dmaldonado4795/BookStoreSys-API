using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStoreSys_API.Migrations
{
    /// <inheritdoc />
    public partial class ChangeDatimeToDateOnly : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateOnly>(
                name: "bookSaved",
                table: "Book",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "bookSaved",
                table: "Book");
        }
    }
}
