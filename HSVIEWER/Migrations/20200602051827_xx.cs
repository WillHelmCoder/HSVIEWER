using Microsoft.EntityFrameworkCore.Migrations;

namespace HSVIEWER.Migrations
{
    public partial class xx : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stages_WorkOrder_WorkOrderId",
                table: "Stages");

            migrationBuilder.AlterColumn<int>(
                name: "WorkOrderId",
                table: "Stages",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PipeLineId",
                table: "Deals",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Stages_WorkOrder_WorkOrderId",
                table: "Stages",
                column: "WorkOrderId",
                principalTable: "WorkOrder",
                principalColumn: "WorkOrderId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stages_WorkOrder_WorkOrderId",
                table: "Stages");

            migrationBuilder.DropColumn(
                name: "PipeLineId",
                table: "Deals");

            migrationBuilder.AlterColumn<int>(
                name: "WorkOrderId",
                table: "Stages",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Stages_WorkOrder_WorkOrderId",
                table: "Stages",
                column: "WorkOrderId",
                principalTable: "WorkOrder",
                principalColumn: "WorkOrderId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
