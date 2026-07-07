using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace proyectoFeelings.Migrations
{
    /// <inheritdoc />
    public partial class migration17 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Invoice");

            migrationBuilder.RenameColumn(
                name: "Cantidad",
                table: "Record",
                newName: "StoreProductStoreID");

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Record",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StoreProductProductID",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Record_StoreProduct_StoreProductStoreID_StoreProductProductID",
                table: "Record");

            migrationBuilder.DropIndex(
                name: "IX_Record_StoreProductStoreID_StoreProductProductID",
                table: "Record");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Record");

            migrationBuilder.DropColumn(
                name: "StoreProductProductID",
                table: "Record");

            migrationBuilder.RenameColumn(
                name: "StoreProductStoreID",
                table: "Record",
                newName: "Cantidad");

            migrationBuilder.CreateTable(
                name: "Invoice",
                columns: table => new
                {
                    InvoiceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Datetime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    ProductID = table.Column<int>(type: "int", nullable: false),
                    StoreID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoice", x => x.InvoiceId);
                });
        }
    }
}
