using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persons.Data.Migrations
{
    public partial class _4thMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RelatedPersons_Persons_RelatedPersonId",
                table: "RelatedPersons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RelatedPersons",
                table: "RelatedPersons");

            migrationBuilder.AlterColumn<int>(
                name: "PersonId",
                table: "RelatedPersons",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "RelatedPersonId",
                table: "RelatedPersons",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "RelateId",
                table: "RelatedPersons",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_RelatedPersons",
                table: "RelatedPersons",
                column: "RelateId");

            migrationBuilder.CreateIndex(
                name: "IX_RelatedPersons_RelatedPersonId",
                table: "RelatedPersons",
                column: "RelatedPersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_RelatedPersons_Persons_RelatedPersonId",
                table: "RelatedPersons",
                column: "RelatedPersonId",
                principalTable: "Persons",
                principalColumn: "PersonId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RelatedPersons_Persons_RelatedPersonId",
                table: "RelatedPersons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RelatedPersons",
                table: "RelatedPersons");

            migrationBuilder.DropIndex(
                name: "IX_RelatedPersons_RelatedPersonId",
                table: "RelatedPersons");

            migrationBuilder.DropColumn(
                name: "RelateId",
                table: "RelatedPersons");

            migrationBuilder.AlterColumn<int>(
                name: "RelatedPersonId",
                table: "RelatedPersons",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PersonId",
                table: "RelatedPersons",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_RelatedPersons",
                table: "RelatedPersons",
                columns: new[] { "RelatedPersonId", "PersonId" });

            migrationBuilder.AddForeignKey(
                name: "FK_RelatedPersons_Persons_RelatedPersonId",
                table: "RelatedPersons",
                column: "RelatedPersonId",
                principalTable: "Persons",
                principalColumn: "PersonId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
