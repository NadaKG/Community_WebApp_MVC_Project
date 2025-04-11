using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Community_With_CMS.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateImg : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageContentType",
                table: "Post");

            migrationBuilder.DropColumn(
                name: "ImageData",
                table: "Post");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Post",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Post");

            migrationBuilder.AddColumn<string>(
                name: "ImageContentType",
                table: "Post",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "ImageData",
                table: "Post",
                type: "varbinary(max)",
                nullable: true);
        }
    }
}
