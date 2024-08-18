using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlexForge.Repository.Migrations
{
    /// <inheritdoc />
    public partial class AddedAgeGroupenumfieldinProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AgeGroup",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AgeGroup",
                table: "Products");
        }
    }
}
