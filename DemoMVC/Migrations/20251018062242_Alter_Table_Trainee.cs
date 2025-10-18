using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DemoMVC.Migrations
{
    /// <inheritdoc />
    public partial class Alter_Table_Trainee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Trainees");

            migrationBuilder.DropColumn(
                name: "CompanyName",
                table: "Trainees");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Trainees");

            migrationBuilder.DropColumn(
                name: "Position",
                table: "Trainees");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Trainees",
                newName: "Organization");

            migrationBuilder.AddColumn<DateTime>(
                name: "RegistrationDate",
                table: "Trainees",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RegistrationDate",
                table: "Trainees");

            migrationBuilder.RenameColumn(
                name: "Organization",
                table: "Trainees",
                newName: "Email");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Trainees",
                type: "TEXT",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CompanyName",
                table: "Trainees",
                type: "TEXT",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Trainees",
                type: "TEXT",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Position",
                table: "Trainees",
                type: "TEXT",
                maxLength: 100,
                nullable: true);
        }
    }
}
