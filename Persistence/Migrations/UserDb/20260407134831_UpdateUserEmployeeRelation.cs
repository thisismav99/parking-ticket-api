using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations.UserDb
{
    /// <inheritdoc />
    public partial class UpdateUserEmployeeRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserClaims_AspNetUsers_UserId",
                schema: "user",
                table: "UserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_UserLogins_AspNetUsers_UserId",
                schema: "user",
                table: "UserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRoles_AspNetUsers_UserId",
                schema: "user",
                table: "UserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_AspNetUsers_Id",
                schema: "user",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_UserTokens_AspNetUsers_UserId",
                schema: "user",
                table: "UserTokens");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                schema: "user",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "AccessFailedCount",
                schema: "user",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ConcurrencyStamp",
                schema: "user",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                schema: "user",
                table: "Users",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "EmailConfirmed",
                schema: "user",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "LockoutEnabled",
                schema: "user",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LockoutEnd",
                schema: "user",
                table: "Users",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedEmail",
                schema: "user",
                table: "Users",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedUserName",
                schema: "user",
                table: "Users",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                schema: "user",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                schema: "user",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PhoneNumberConfirmed",
                schema: "user",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "SecurityStamp",
                schema: "user",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "TwoFactorEnabled",
                schema: "user",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                schema: "user",
                table: "Users",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UserEmpployees",
                schema: "user",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_UserEmpployees_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "user",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                schema: "user",
                table: "Users",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                schema: "user",
                table: "Users",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UserEmpployees_UserId",
                schema: "user",
                table: "UserEmpployees",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserClaims_Users_UserId",
                schema: "user",
                table: "UserClaims",
                column: "UserId",
                principalSchema: "user",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserLogins_Users_UserId",
                schema: "user",
                table: "UserLogins",
                column: "UserId",
                principalSchema: "user",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoles_Users_UserId",
                schema: "user",
                table: "UserRoles",
                column: "UserId",
                principalSchema: "user",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserTokens_Users_UserId",
                schema: "user",
                table: "UserTokens",
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
                name: "FK_UserClaims_Users_UserId",
                schema: "user",
                table: "UserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_UserLogins_Users_UserId",
                schema: "user",
                table: "UserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRoles_Users_UserId",
                schema: "user",
                table: "UserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_UserTokens_Users_UserId",
                schema: "user",
                table: "UserTokens");

            migrationBuilder.DropTable(
                name: "UserEmpployees",
                schema: "user");

            migrationBuilder.DropIndex(
                name: "EmailIndex",
                schema: "user",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "UserNameIndex",
                schema: "user",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "AccessFailedCount",
                schema: "user",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ConcurrencyStamp",
                schema: "user",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Email",
                schema: "user",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "EmailConfirmed",
                schema: "user",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LockoutEnabled",
                schema: "user",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LockoutEnd",
                schema: "user",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "NormalizedEmail",
                schema: "user",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "NormalizedUserName",
                schema: "user",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                schema: "user",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                schema: "user",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PhoneNumberConfirmed",
                schema: "user",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "SecurityStamp",
                schema: "user",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "TwoFactorEnabled",
                schema: "user",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserName",
                schema: "user",
                table: "Users");

            migrationBuilder.AddColumn<Guid>(
                name: "EmployeeId",
                schema: "user",
                table: "Users",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_UserClaims_AspNetUsers_UserId",
                schema: "user",
                table: "UserClaims",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserLogins_AspNetUsers_UserId",
                schema: "user",
                table: "UserLogins",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoles_AspNetUsers_UserId",
                schema: "user",
                table: "UserRoles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_AspNetUsers_Id",
                schema: "user",
                table: "Users",
                column: "Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserTokens_AspNetUsers_UserId",
                schema: "user",
                table: "UserTokens",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
