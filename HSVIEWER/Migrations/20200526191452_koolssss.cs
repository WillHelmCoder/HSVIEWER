using Microsoft.EntityFrameworkCore.Migrations;

namespace HSVIEWER.Migrations
{
    public partial class koolssss : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deals_Stages_StageId",
                table: "Deals");

            migrationBuilder.DropIndex(
                name: "IX_Deals_StageId",
                table: "Deals");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Deals");

            migrationBuilder.DropColumn(
                name: "StageId",
                table: "Deals");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OwnerId",
                table: "Deals",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StageId",
                table: "Deals",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Deals_StageId",
                table: "Deals",
                column: "StageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Deals_Stages_StageId",
                table: "Deals",
                column: "StageId",
                principalTable: "Stages",
                principalColumn: "StageId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
