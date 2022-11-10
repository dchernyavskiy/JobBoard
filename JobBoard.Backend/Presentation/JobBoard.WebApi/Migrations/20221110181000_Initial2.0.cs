using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobBoard.WebApi.Migrations
{
    public partial class Initial20 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Locations",
                newName: "City");

            migrationBuilder.AddColumn<string>(
                name: "Employment",
                table: "Jobs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ShortDiscription",
                table: "Jobs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Employment",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "ShortDiscription",
                table: "Jobs");

            migrationBuilder.RenameColumn(
                name: "City",
                table: "Locations",
                newName: "Name");
        }
    }
}
