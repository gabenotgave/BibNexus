using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ULMS2.Data.Migrations
{
    public partial class OptionalizeDepartment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Department",
                table: "Students",
                type: "varchar(24)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(24)");

            migrationBuilder.AlterColumn<string>(
                name: "Department",
                table: "Faculty",
                type: "varchar(24)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(24)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Department",
                table: "Students",
                type: "varchar(24)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(24)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Department",
                table: "Faculty",
                type: "varchar(24)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(24)",
                oldNullable: true);
        }
    }
}
