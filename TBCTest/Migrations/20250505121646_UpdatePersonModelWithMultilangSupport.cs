using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TBCTest.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePersonModelWithMultilangSupport : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Cities");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "People",
                newName: "LastNameGe");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "People",
                newName: "LastNameEn");

            migrationBuilder.AlterColumn<string>(
                name: "PersonalNumber",
                table: "People",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(11)",
                oldMaxLength: 11);

            migrationBuilder.AddColumn<string>(
                name: "FirstNameEn",
                table: "People",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FirstNameGe",
                table: "People",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NameEn",
                table: "Cities",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NameGe",
                table: "Cities",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstNameEn",
                table: "People");

            migrationBuilder.DropColumn(
                name: "FirstNameGe",
                table: "People");

            migrationBuilder.DropColumn(
                name: "NameEn",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "NameGe",
                table: "Cities");

            migrationBuilder.RenameColumn(
                name: "LastNameGe",
                table: "People",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "LastNameEn",
                table: "People",
                newName: "FirstName");

            migrationBuilder.AlterColumn<string>(
                name: "PersonalNumber",
                table: "People",
                type: "nvarchar(11)",
                maxLength: 11,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Cities",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
