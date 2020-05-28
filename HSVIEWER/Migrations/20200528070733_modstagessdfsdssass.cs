using Microsoft.EntityFrameworkCore.Migrations;

namespace HSVIEWER.Migrations
{
    public partial class modstagessdfsdssass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PipelineId",
                table: "StagesAnalysis",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PipelineId",
                table: "StagesAnalysis");
        }
    }
}
