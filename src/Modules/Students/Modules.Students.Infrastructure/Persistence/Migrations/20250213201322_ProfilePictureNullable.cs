using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Modules.Students.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ProfilePictureNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ProfilePicture",
                schema: "students",
                table: "Students",
                type: "VARCHAR(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(255)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ProfilePicture",
                schema: "students",
                table: "Students",
                type: "VARCHAR(255)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "VARCHAR(255)",
                oldNullable: true);
        }
    }
}
