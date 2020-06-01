using Microsoft.EntityFrameworkCore.Migrations;

namespace HSVIEWER.Migrations
{
    public partial class workOrderIdAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WorkOrderId",
                table: "StagesAnalysis",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WorkOrderId",
                table: "StagesAnalysis");
        }
    }
}
