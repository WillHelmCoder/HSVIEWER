using Microsoft.EntityFrameworkCore.Migrations;

namespace HSVIEWER.Migrations
{
    public partial class otra : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OwnerPipelineValue",
                table: "OwnerStageAnalysis");

            migrationBuilder.AddColumn<string>(
                name: "PipeLineId",
                table: "OwnerStageAnalysis",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "StageValue",
                table: "OwnerStageAnalysis",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PipeLineId",
                table: "OwnerStageAnalysis");

            migrationBuilder.DropColumn(
                name: "StageValue",
                table: "OwnerStageAnalysis");

            migrationBuilder.AddColumn<double>(
                name: "OwnerPipelineValue",
                table: "OwnerStageAnalysis",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
