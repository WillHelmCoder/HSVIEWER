using Microsoft.EntityFrameworkCore.Migrations;

namespace HSVIEWER.Migrations
{
    public partial class koolss : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HsOwnerId",
                table: "Deals",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HsStageId",
                table: "Deals",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "hsdate",
                table: "Deals",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HsOwnerId",
                table: "Deals");

            migrationBuilder.DropColumn(
                name: "HsStageId",
                table: "Deals");

            migrationBuilder.DropColumn(
                name: "hsdate",
                table: "Deals");
        }
    }
}
