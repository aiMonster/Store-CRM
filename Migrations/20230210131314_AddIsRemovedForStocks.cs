using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoreCRM.Migrations
{
    public partial class AddIsRemovedForStocks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsRemoved",
                table: "Stocks",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRemoved",
                table: "Stocks");
        }
    }
}
