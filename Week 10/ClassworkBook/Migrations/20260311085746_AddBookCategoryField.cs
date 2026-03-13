using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClassworkBook.Migrations
{
    /// <inheritdoc />
    public partial class AddBookCategoryField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BookCategory",
                table: "tblbooks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BookCategory",
                table: "tblbooks");
        }
    }
}
