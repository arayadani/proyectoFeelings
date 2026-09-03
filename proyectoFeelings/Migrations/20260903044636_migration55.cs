using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace proyectoFeelings.Migrations
{
    /// <inheritdoc />
    public partial class migration55 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Product_Code",
                table: "Product");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Product_Code",
                table: "Product",
                column: "Code",
                unique: true);
        }
    }
}
