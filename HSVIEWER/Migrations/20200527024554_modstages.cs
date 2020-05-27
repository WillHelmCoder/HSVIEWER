using Microsoft.EntityFrameworkCore.Migrations;

namespace HSVIEWER.Migrations
{
    public partial class modstages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stages_Pipelines_PipelinesPipeLineId",
                table: "Stages");

            migrationBuilder.DropIndex(
                name: "IX_Stages_PipelinesPipeLineId",
                table: "Stages");

            migrationBuilder.DropColumn(
                name: "PipelinesPipeLineId",
                table: "Stages");

            migrationBuilder.RenameColumn(
                name: "PipeLineId",
                table: "Pipelines",
                newName: "PipelineId");

            migrationBuilder.AlterColumn<int>(
                name: "PipelineId",
                table: "Stages",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HsPipelineId",
                table: "Stages",
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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.RenameColumn(
                name: "PipelineId",
                table: "Pipelines",
                newName: "PipeLineId");

            migrationBuilder.AlterColumn<string>(
                name: "PipelineId",
                table: "Stages",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "PipelinesPipeLineId",
                table: "Stages",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Stages_PipelinesPipeLineId",
                table: "Stages",
                column: "PipelinesPipeLineId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stages_Pipelines_PipelinesPipeLineId",
                table: "Stages",
                column: "PipelinesPipeLineId",
                principalTable: "Pipelines",
                principalColumn: "PipeLineId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
