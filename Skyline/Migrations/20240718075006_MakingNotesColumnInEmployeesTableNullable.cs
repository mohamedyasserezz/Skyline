using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Skyline.Migrations
{
    /// <inheritdoc />
    public partial class MakingNotesColumnInEmployeesTableNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "HiringDateAndTime",
                table: "Employees",
                newName: "HiringDateTime");

            migrationBuilder.RenameColumn(
                name: "AttendanceTime",
                table: "Employees",
                newName: "AttendenceTime");

            migrationBuilder.RenameColumn(
                name: "Appriasal",
                table: "Employees",
                newName: "Appraisal");

            migrationBuilder.AlterColumn<string>(
                name: "Position",
                table: "Employees",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "Notes",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "NationalId",
                table: "Employees",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(14)",
                oldMaxLength: 14);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "HiringDateTime",
                table: "Employees",
                newName: "HiringDateAndTime");

            migrationBuilder.RenameColumn(
                name: "AttendenceTime",
                table: "Employees",
                newName: "AttendanceTime");

            migrationBuilder.RenameColumn(
                name: "Appraisal",
                table: "Employees",
                newName: "Appriasal");

            migrationBuilder.AlterColumn<string>(
                name: "Position",
                table: "Employees",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Notes",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NationalId",
                table: "Employees",
                type: "nvarchar(14)",
                maxLength: 14,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);
        }
    }
}
