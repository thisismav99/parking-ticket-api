using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "Vehicles",
                schema: "vehicle",
                newName: "Vehicles",
                newSchema: "parking");

            migrationBuilder.RenameTable(
                name: "Employees",
                schema: "employee",
                newName: "Employees",
                newSchema: "parking");

            migrationBuilder.RenameTable(
                name: "Customers",
                schema: "customer",
                newName: "Customers",
                newSchema: "parking");

            migrationBuilder.RenameTable(
                name: "Company",
                schema: "company",
                newName: "Company",
                newSchema: "parking");

            migrationBuilder.RenameTable(
                name: "Address",
                schema: "common",
                newName: "Address",
                newSchema: "parking");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "common");

            migrationBuilder.EnsureSchema(
                name: "company");

            migrationBuilder.EnsureSchema(
                name: "customer");

            migrationBuilder.EnsureSchema(
                name: "employee");

            migrationBuilder.EnsureSchema(
                name: "vehicle");

            migrationBuilder.RenameTable(
                name: "Vehicles",
                schema: "parking",
                newName: "Vehicles",
                newSchema: "vehicle");

            migrationBuilder.RenameTable(
                name: "Employees",
                schema: "parking",
                newName: "Employees",
                newSchema: "employee");

            migrationBuilder.RenameTable(
                name: "Customers",
                schema: "parking",
                newName: "Customers",
                newSchema: "customer");

            migrationBuilder.RenameTable(
                name: "Company",
                schema: "parking",
                newName: "Company",
                newSchema: "company");

            migrationBuilder.RenameTable(
                name: "Address",
                schema: "parking",
                newName: "Address",
                newSchema: "common");
        }
    }
}
