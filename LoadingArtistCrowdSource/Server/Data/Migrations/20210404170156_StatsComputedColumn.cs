﻿using Microsoft.EntityFrameworkCore.Migrations;

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
                computedColumnSql: "IsActive AND NOT IsDeleted AND Type <> 'Section'",
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
