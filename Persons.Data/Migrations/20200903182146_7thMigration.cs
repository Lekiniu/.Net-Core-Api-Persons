using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persons.Data.Migrations
{
    public partial class _7thMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_RelatedPersons",
                table: "RelatedPersons");

            migrationBuilder.AlterColumn<int>(
                name: "PersonTypeId",
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                name: "PersonTypeId",
                table: "RelatedPersons",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_RelatedPersons",
                table: "RelatedPersons",
                columns: new[] { "RelatedPersonId", "PersonTypeId", "PersonId" });
        }
    }
}
