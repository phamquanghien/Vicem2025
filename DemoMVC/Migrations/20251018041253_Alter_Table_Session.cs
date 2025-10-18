using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DemoMVC.Migrations
{
    /// <inheritdoc />
    public partial class Alter_Table_Session : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Sessions");

            migrationBuilder.DropColumn(
                name: "Period",
                table: "Sessions");

            migrationBuilder.DropColumn(
                name: "SessionCode",
                table: "Sessions");

            migrationBuilder.DropColumn(
                name: "SessionName",
                table: "Sessions");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Batches");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Sessions",
                newName: "SessionDate");

            migrationBuilder.AddColumn<int>(
                name: "SessionType",
                table: "Sessions",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SessionType",
                table: "Sessions");

            migrationBuilder.RenameColumn(
                name: "SessionDate",
                table: "Sessions",
                newName: "Date");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Sessions",
                type: "TEXT",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Period",
                table: "Sessions",
                type: "TEXT",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SessionCode",
                table: "Sessions",
                type: "TEXT",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SessionName",
                table: "Sessions",
                type: "TEXT",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Batches",
                type: "TEXT",
                maxLength: 500,
                nullable: true);
        }
    }
}
