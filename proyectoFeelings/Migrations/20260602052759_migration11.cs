using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace proyectoFeelings.Migrations
{
    /// <inheritdoc />
    public partial class migration11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Store_StoreID",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<int>(
                name: "StoreID",
                table: "AspNetUsers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Store_StoreID",
                table: "AspNetUsers",
                column: "StoreID",
                principalTable: "Store",
                principalColumn: "StoreID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Store_StoreID",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<int>(
                name: "StoreID",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Store_StoreID",
                table: "AspNetUsers",
                column: "StoreID",
                principalTable: "Store",
                principalColumn: "StoreID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
