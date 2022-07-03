using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OFF.Infrastructure.EntityFramework.Migrations
{
    public partial class Addcollumnavaibletodish : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Avaible",
                table: "Dishes",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Avaible",
                table: "Dishes");
        }
    }
}
