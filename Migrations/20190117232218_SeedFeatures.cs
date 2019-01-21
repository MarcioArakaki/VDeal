using Microsoft.EntityFrameworkCore.Migrations;

namespace VehicleDealer.Migrations
{
    public partial class SeedFeatures : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Features (Name) VALUES ('It walks')");
            migrationBuilder.Sql("INSERT INTO Features (Name) VALUES ('Automatic')");
            migrationBuilder.Sql("INSERT INTO Features (Name) VALUES ('Flex')");
            migrationBuilder.Sql("INSERT INTO Features (Name) VALUES ('Eletric')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Features  WHERE NAME ='It walks'");
            migrationBuilder.Sql("DELETE FROM Features  WHERE NAME ='Automatic'");
            migrationBuilder.Sql("DELETE FROM Features  WHERE NAME ='Eletric'");
            migrationBuilder.Sql("DELETE FROM Features  WHERE NAME ='Flex'");
        }
    }
}
