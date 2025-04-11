using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Community_With_CMS.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemovePropResKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResourceKey",
                table: "Menu");

            migrationBuilder.DropColumn(
                name: "ResourceKey",
                table: "Category");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ResourceKey",
                table: "Menu",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ResourceKey",
                table: "Category",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
