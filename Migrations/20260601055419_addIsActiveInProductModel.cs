using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GenericOps.Migrations
{
    /// <inheritdoc />
    public partial class addIsActiveInProductModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IsActive",
                table: "tblProduct",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "tblProduct");
        }
    }
}
