using Microsoft.EntityFrameworkCore.Migrations;

namespace HSVIEWER.Migrations
{
    public partial class kool : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stages_WorkOrder_WorkOrderId",
                table: "Stages");

            migrationBuilder.AlterColumn<int>(
                name: "WorkOrderId",
                table: "Stages",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "PipelinesPipeLineId",
                table: "Stages",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Pipelines",
                columns: table => new
                {
                    PipeLineId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    WorkOrderId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pipelines", x => x.PipeLineId);
                    table.ForeignKey(
                        name: "FK_Pipelines_WorkOrder_WorkOrderId",
                        column: x => x.WorkOrderId,
                        principalTable: "WorkOrder",
                        principalColumn: "WorkOrderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Stages_PipelinesPipeLineId",
                table: "Stages",
                column: "PipelinesPipeLineId");

            migrationBuilder.CreateIndex(
                name: "IX_Pipelines_WorkOrderId",
                table: "Pipelines",
                column: "WorkOrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stages_Pipelines_PipelinesPipeLineId",
                table: "Stages",
                column: "PipelinesPipeLineId",
                principalTable: "Pipelines",
                principalColumn: "PipeLineId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Stages_WorkOrder_WorkOrderId",
                table: "Stages",
                column: "WorkOrderId",
                principalTable: "WorkOrder",
                principalColumn: "WorkOrderId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stages_Pipelines_PipelinesPipeLineId",
                table: "Stages");

            migrationBuilder.DropForeignKey(
                name: "FK_Stages_WorkOrder_WorkOrderId",
                table: "Stages");

            migrationBuilder.DropTable(
                name: "Pipelines");

            migrationBuilder.DropIndex(
                name: "IX_Stages_PipelinesPipeLineId",
                table: "Stages");

            migrationBuilder.DropColumn(
                name: "PipelinesPipeLineId",
                table: "Stages");

            migrationBuilder.AlterColumn<int>(
                name: "WorkOrderId",
                table: "Stages",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Stages_WorkOrder_WorkOrderId",
                table: "Stages",
                column: "WorkOrderId",
                principalTable: "WorkOrder",
                principalColumn: "WorkOrderId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
