using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace proyectoFeelings.Migrations
{
    /// <inheritdoc />
    public partial class migration18 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Record_StoreProduct_StoreProductStoreID_StoreProductProductID",
                table: "Record");

            migrationBuilder.DropIndex(
                name: "IX_Record_StoreProductStoreID_StoreProductProductID",
                table: "Record");

            migrationBuilder.DropColumn(
                name: "StoreProductProductID",
                table: "Record");

            migrationBuilder.DropColumn(
                name: "StoreProductStoreID",
                table: "Record");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StoreProductProductID",
                table: "Record",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StoreProductStoreID",
                table: "Record",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Record_StoreProductStoreID_StoreProductProductID",
                table: "Record",
                columns: new[] { "StoreProductStoreID", "StoreProductProductID" });

            migrationBuilder.AddForeignKey(
                name: "FK_Record_StoreProduct_StoreProductStoreID_StoreProductProductID",
                table: "Record",
                columns: new[] { "StoreProductStoreID", "StoreProductProductID" },
                principalTable: "StoreProduct",
                principalColumns: new[] { "StoreID", "ProductID" },
                onDelete: ReferentialAction.Cascade);
        }
    }
}
