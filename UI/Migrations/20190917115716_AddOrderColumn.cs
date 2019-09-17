using Microsoft.EntityFrameworkCore.Migrations;

namespace UI.Migrations
{
    public partial class AddOrderColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Receiver",
                table: "Order",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShipTo",
                table: "Order",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Receiver",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "ShipTo",
                table: "Order");
        }
    }
}
