using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace proyectoFeelings.Migrations
{
    /// <inheritdoc />
    public partial class MakeCodeUnique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Product_Code",
                table: "Product",
                column: "Code",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Product_Code",
                table: "Product");
        }
    }
}
