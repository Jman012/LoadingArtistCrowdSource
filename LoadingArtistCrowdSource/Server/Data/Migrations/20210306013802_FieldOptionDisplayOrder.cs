using Microsoft.EntityFrameworkCore.Migrations;

namespace LoadingArtistCrowdSource.Server.Data.Migrations
{
    public partial class FieldOptionDisplayOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DisplayOrder",
                table: "CrowdSourcedFieldDefinitionOption",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "432ea055-ea01-443d-a6f7-e97d2c18d275",
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAEHdPXFD3bkyPztpuI1DsJekFPV6eCjU9eAtXidhjxMdGEMWkOl0kA1a314ufeD/cjQ==");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DisplayOrder",
                table: "CrowdSourcedFieldDefinitionOption");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "432ea055-ea01-443d-a6f7-e97d2c18d275",
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAEGcMHFGENP1e3gIgzicFPkEkznYA2I/i4ygvv6gkD5J5ZbtP5mZhNULGL5kn+OKtQA==");
        }
    }
}
