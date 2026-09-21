using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace proyectoFeelings.Migrations
{
    /// <inheritdoc />
    public partial class migration70 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Record",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "CurrentPrice",
                table: "Record",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Record",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NewCategory",
                table: "Record",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NewDescription",
                table: "Record",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NewPrice",
                table: "Record",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NewProvider",
                table: "Record",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NewQuantity",
                table: "Record",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "NewStatus",
                table: "Record",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Provider",
                table: "Record",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "Record",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "Record");

            migrationBuilder.DropColumn(
                name: "CurrentPrice",
                table: "Record");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Record");

            migrationBuilder.DropColumn(
                name: "NewCategory",
                table: "Record");

            migrationBuilder.DropColumn(
                name: "NewDescription",
                table: "Record");

            migrationBuilder.DropColumn(
                name: "NewPrice",
                table: "Record");

            migrationBuilder.DropColumn(
                name: "NewProvider",
                table: "Record");

            migrationBuilder.DropColumn(
                name: "NewQuantity",
                table: "Record");

            migrationBuilder.DropColumn(
                name: "NewStatus",
                table: "Record");

            migrationBuilder.DropColumn(
                name: "Provider",
                table: "Record");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Record");
        }
    }
}
