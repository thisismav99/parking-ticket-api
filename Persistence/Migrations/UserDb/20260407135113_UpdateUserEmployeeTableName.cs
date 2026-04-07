using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations.UserDb
{
    /// <inheritdoc />
    public partial class UpdateUserEmployeeTableName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserEmpployees_Users_UserId",
                schema: "user",
                table: "UserEmpployees");

            migrationBuilder.RenameTable(
                name: "UserEmpployees",
                schema: "user",
                newName: "UserEmployees",
                newSchema: "user");

            migrationBuilder.RenameIndex(
                name: "IX_UserEmpployees_UserId",
                schema: "user",
                table: "UserEmployees",
                newName: "IX_UserEmployees_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserEmployees_Users_UserId",
                schema: "user",
                table: "UserEmployees",
                column: "UserId",
                principalSchema: "user",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserEmployees_Users_UserId",
                schema: "user",
                table: "UserEmployees");

            migrationBuilder.RenameTable(
                name: "UserEmployees",
                schema: "user",
                newName: "UserEmpployees",
                newSchema: "user");

            migrationBuilder.RenameIndex(
                name: "IX_UserEmployees_UserId",
                schema: "user",
                table: "UserEmpployees",
                newName: "IX_UserEmpployees_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserEmpployees_Users_UserId",
                schema: "user",
                table: "UserEmpployees",
                column: "UserId",
                principalSchema: "user",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
