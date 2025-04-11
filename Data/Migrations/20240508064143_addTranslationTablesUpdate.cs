using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Community_With_CMS.Data.Migrations
{
    /// <inheritdoc />
    public partial class addTranslationTablesUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryTranslation_Category_CategoryId",
                table: "CategoryTranslation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CategoryTranslation",
                table: "CategoryTranslation");

            migrationBuilder.RenameTable(
                name: "CategoryTranslation",
                newName: "CategoryTranslations");

            migrationBuilder.RenameIndex(
                name: "IX_CategoryTranslation_CategoryId",
                table: "CategoryTranslations",
                newName: "IX_CategoryTranslations_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CategoryTranslations",
                table: "CategoryTranslations",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryTranslations_Category_CategoryId",
                table: "CategoryTranslations",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryTranslations_Category_CategoryId",
                table: "CategoryTranslations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CategoryTranslations",
                table: "CategoryTranslations");

            migrationBuilder.RenameTable(
                name: "CategoryTranslations",
                newName: "CategoryTranslation");

            migrationBuilder.RenameIndex(
                name: "IX_CategoryTranslations_CategoryId",
                table: "CategoryTranslation",
                newName: "IX_CategoryTranslation_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CategoryTranslation",
                table: "CategoryTranslation",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryTranslation_Category_CategoryId",
                table: "CategoryTranslation",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
