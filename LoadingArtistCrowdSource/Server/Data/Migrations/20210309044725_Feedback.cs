using Microsoft.EntityFrameworkCore.Migrations;

namespace LoadingArtistCrowdSource.Server.Data.Migrations
{
    public partial class Feedback : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CrowdSourcedFieldDefinitionFeedback",
                table: "CrowdSourcedFieldDefinitionFeedback");

            migrationBuilder.AddColumn<int>(
                name: "ComicId",
                table: "CrowdSourcedFieldDefinitionFeedback",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CrowdSourcedFieldDefinitionFeedback",
                table: "CrowdSourcedFieldDefinitionFeedback",
                columns: new[] { "ComicId", "CrowdSourcedFieldDefinitionId", "Id" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "432ea055-ea01-443d-a6f7-e97d2c18d275",
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAEGThm/+f+RsBogU8iIZQ5u47tZMj4opKQz2ntvmnM7WlhbLoX0gIOLVAbs1tRy65vw==");

            migrationBuilder.CreateIndex(
                name: "IX_CrowdSourcedFieldDefinitionFeedback_CrowdSourcedFieldDefinitionId",
                table: "CrowdSourcedFieldDefinitionFeedback",
                column: "CrowdSourcedFieldDefinitionId");

            migrationBuilder.AddForeignKey(
                name: "FK_CrowdSourcedFieldDefinitionFeedback_Comic_ComicId",
                table: "CrowdSourcedFieldDefinitionFeedback",
                column: "ComicId",
                principalTable: "Comic",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CrowdSourcedFieldDefinitionFeedback_Comic_ComicId",
                table: "CrowdSourcedFieldDefinitionFeedback");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CrowdSourcedFieldDefinitionFeedback",
                table: "CrowdSourcedFieldDefinitionFeedback");

            migrationBuilder.DropIndex(
                name: "IX_CrowdSourcedFieldDefinitionFeedback_CrowdSourcedFieldDefinitionId",
                table: "CrowdSourcedFieldDefinitionFeedback");

            migrationBuilder.DropColumn(
                name: "ComicId",
                table: "CrowdSourcedFieldDefinitionFeedback");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CrowdSourcedFieldDefinitionFeedback",
                table: "CrowdSourcedFieldDefinitionFeedback",
                columns: new[] { "CrowdSourcedFieldDefinitionId", "Id" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "432ea055-ea01-443d-a6f7-e97d2c18d275",
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAEHdPXFD3bkyPztpuI1DsJekFPV6eCjU9eAtXidhjxMdGEMWkOl0kA1a314ufeD/cjQ==");
        }
    }
}
