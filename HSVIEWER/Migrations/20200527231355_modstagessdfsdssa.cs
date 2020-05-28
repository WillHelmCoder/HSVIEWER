using Microsoft.EntityFrameworkCore.Migrations;

namespace HSVIEWER.Migrations
{
    public partial class modstagessdfsdssa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OwnerAnalysis",
                columns: table => new
                {
                    OwnerAnalysisId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OwnerName = table.Column<string>(nullable: true),
                    DealsNumber = table.Column<int>(nullable: false),
                    DealAverage = table.Column<double>(nullable: false),
                    OwnerPipelineValue = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OwnerAnalysis", x => x.OwnerAnalysisId);
                });

            migrationBuilder.CreateTable(
                name: "OwnerStageAnalysis",
                columns: table => new
                {
                    OwnerStageAnalysisId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OwnerName = table.Column<string>(nullable: true),
                    StageName = table.Column<string>(nullable: true),
                    DealsNumber = table.Column<int>(nullable: false),
                    DealAverage = table.Column<double>(nullable: false),
                    OwnerPipelineValue = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OwnerStageAnalysis", x => x.OwnerStageAnalysisId);
                });

            migrationBuilder.CreateTable(
                name: "StagesAnalysis",
                columns: table => new
                {
                    StagesAnalysisId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Stagename = table.Column<string>(nullable: true),
                    DealsNumber = table.Column<int>(nullable: false),
                    DealAverage = table.Column<double>(nullable: false),
                    StageValue = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StagesAnalysis", x => x.StagesAnalysisId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OwnerAnalysis");

            migrationBuilder.DropTable(
                name: "OwnerStageAnalysis");

            migrationBuilder.DropTable(
                name: "StagesAnalysis");
        }
    }
}
