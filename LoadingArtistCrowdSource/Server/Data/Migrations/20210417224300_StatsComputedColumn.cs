using Microsoft.EntityFrameworkCore.Migrations;

namespace LoadingArtistCrowdSource.Server.Data.Migrations
{
    public partial class StatsComputedColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsUsableForStatistics",
                table: "CrowdSourcedFieldDefinition",
                type: "bit",
                nullable: false,
                computedColumnSql: "CAST(CASE WHEN [IsActive] = 1 AND [IsDeleted] <> 1 AND [Type] <> 'Section' THEN 1 ELSE 0 END AS BIT)",
                stored: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsUsableForStatistics",
                table: "CrowdSourcedFieldDefinition");
        }
    }
}
