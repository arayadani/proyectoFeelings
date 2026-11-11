using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace proyectoFeelings.Migrations
{
    /// <inheritdoc />
    public partial class migration8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ReductionReason",
                table: "Record",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReductionReason",
                table: "Record");
        }
    }
}
