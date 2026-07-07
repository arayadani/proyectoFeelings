using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace proyectoFeelings.Migrations
{
    /// <inheritdoc />
    public partial class migration19 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CurrentStoreID",
                table: "Record",
                newName: "StoreID");

            migrationBuilder.CreateIndex(
                name: "IX_Record_StoreID_ProductID",
                table: "Record",
                columns: new[] { "StoreID", "ProductID" });

            migrationBuilder.AddForeignKey(
                name: "FK_Record_StoreProduct_StoreID_ProductID",
                table: "Record",
                columns: new[] { "StoreID", "ProductID" },
                principalTable: "StoreProduct",
                principalColumns: new[] { "StoreID", "ProductID" },
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Record_StoreProduct_StoreID_ProductID",
                table: "Record");

            migrationBuilder.DropIndex(
                name: "IX_Record_StoreID_ProductID",
                table: "Record");

            migrationBuilder.RenameColumn(
                name: "StoreID",
                table: "Record",
                newName: "CurrentStoreID");
        }
    }
}
