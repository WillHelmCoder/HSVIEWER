using Microsoft.EntityFrameworkCore.Migrations;

namespace HSVIEWER.Migrations
{
    public partial class modstagessdf : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stages_Pipelines_PipelineId",
                table: "Stages");

            migrationBuilder.DropIndex(
                name: "IX_Stages_PipelineId",
                table: "Stages");

            migrationBuilder.DropColumn(
                name: "HsPipelineId",
                table: "Stages");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HsPipelineId",
                table: "Stages",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Stages_PipelineId",
                table: "Stages",
                column: "PipelineId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stages_Pipelines_PipelineId",
                table: "Stages",
                column: "PipelineId",
                principalTable: "Pipelines",
                principalColumn: "PipelineId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
