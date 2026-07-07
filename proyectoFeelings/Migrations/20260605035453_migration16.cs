using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace proyectoFeelings.Migrations
{
    /// <inheritdoc />
    public partial class migration16 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_StoreProduct_ProductID",
                table: "StoreProduct",
                column: "ProductID");

            migrationBuilder.AddForeignKey(
                name: "FK_StoreProduct_Product_ProductID",
                table: "StoreProduct",
                column: "ProductID",
                principalTable: "Product",
                principalColumn: "ProductID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StoreProduct_Store_StoreID",
                table: "StoreProduct",
                column: "StoreID",
                principalTable: "Store",
                principalColumn: "StoreID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StoreProduct_Product_ProductID",
                table: "StoreProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_StoreProduct_Store_StoreID",
                table: "StoreProduct");

            migrationBuilder.DropIndex(
                name: "IX_StoreProduct_ProductID",
                table: "StoreProduct");
        }
    }
}
