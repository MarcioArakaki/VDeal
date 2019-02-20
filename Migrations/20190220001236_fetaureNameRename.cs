using Microsoft.EntityFrameworkCore.Migrations;

namespace VehicleDealer.Migrations
{
    public partial class fetaureNameRename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "name",
                table: "Features",
                newName: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Features",
                newName: "name");
        }
    }
}
