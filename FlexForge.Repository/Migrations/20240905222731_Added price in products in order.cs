using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlexForge.Repository.Migrations
{
    /// <inheritdoc />
    public partial class Addedpriceinproductsinorder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Price",
                table: "ProductInOrders",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "ProductInOrders");
        }
    }
}
