using Microsoft.EntityFrameworkCore.Migrations;

namespace HSVIEWER.Migrations
{
    public partial class kools : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HsPipeLineId",
                table: "Pipelines",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HsPipeLineId",
                table: "Pipelines");
        }
    }
}
