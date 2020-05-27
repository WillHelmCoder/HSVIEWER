using Microsoft.EntityFrameworkCore.Migrations;

namespace HSVIEWER.Migrations
{
    public partial class modstagessdfsd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PipelineId",
                table: "Stages");

            migrationBuilder.AddColumn<string>(
                name: "HsPipelineId",
                table: "Stages",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HsPipelineId",
                table: "Stages");

            migrationBuilder.AddColumn<int>(
                name: "PipelineId",
                table: "Stages",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
