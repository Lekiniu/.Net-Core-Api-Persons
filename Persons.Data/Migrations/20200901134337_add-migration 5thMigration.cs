using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persons.Data.Migrations
{
    public partial class addmigration5thMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_RelatedPersons_RelateId",
                table: "RelatedPersons");

            migrationBuilder.DropColumn(
                name: "RelateId",
                table: "RelatedPersons");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RelateId",
                table: "RelatedPersons",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_RelatedPersons_RelateId",
                table: "RelatedPersons",
                column: "RelateId");
        }
    }
}
