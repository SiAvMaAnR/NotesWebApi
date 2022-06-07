using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Notes.Infrastucture.Migrations
{
    public partial class addsurnameperson : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Surname",
                table: "Persons",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Surname",
                table: "Persons");
        }
    }
}
