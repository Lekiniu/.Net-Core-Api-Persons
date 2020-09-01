using Microsoft.EntityFrameworkCore.Migrations;

namespace Persons.Data.Migrations
{
    public partial class _2ndMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RelatedPersons_Persons_PersonId",
                table: "RelatedPersons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RelatedPersons",
                table: "RelatedPersons");

            migrationBuilder.DropIndex(
                name: "IX_RelatedPersons_RelatedPersonId",
                table: "RelatedPersons");

            migrationBuilder.AlterColumn<int>(
                name: "PersonTypeId",
                table: "RelatedPersons",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_RelatedPersons_RelateId",
                table: "RelatedPersons",
                column: "RelateId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RelatedPersons",
                table: "RelatedPersons",
                columns: new[] { "RelatedPersonId", "PersonTypeId", "PersonId" });

            migrationBuilder.AddForeignKey(
                name: "FK_RelatedPersons_Persons_PersonId",
                table: "RelatedPersons",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "PersonId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RelatedPersons_Persons_PersonId",
                table: "RelatedPersons");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_RelatedPersons_RelateId",
                table: "RelatedPersons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RelatedPersons",
                table: "RelatedPersons");

            migrationBuilder.AlterColumn<int>(
                name: "PersonTypeId",
                table: "RelatedPersons",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddPrimaryKey(
                name: "PK_RelatedPersons",
                table: "RelatedPersons",
                column: "RelateId");

            migrationBuilder.CreateIndex(
                name: "IX_RelatedPersons_RelatedPersonId",
                table: "RelatedPersons",
                column: "RelatedPersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_RelatedPersons_Persons_PersonId",
                table: "RelatedPersons",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "PersonId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
