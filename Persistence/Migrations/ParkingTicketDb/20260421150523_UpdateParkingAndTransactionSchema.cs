using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateParkingAndTransactionSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parkings_Transactions_TransactionId",
                schema: "parking",
                table: "Parkings");

            migrationBuilder.DropIndex(
                name: "IX_Parkings_TransactionId",
                schema: "parking",
                table: "Parkings");

            migrationBuilder.DropColumn(
                name: "TransactionId",
                schema: "parking",
                table: "Parkings");

            migrationBuilder.AddColumn<Guid>(
                name: "ParkingId",
                schema: "parking",
                table: "Transactions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_ParkingId",
                schema: "parking",
                table: "Transactions",
                column: "ParkingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Parkings_ParkingId",
                schema: "parking",
                table: "Transactions",
                column: "ParkingId",
                principalSchema: "parking",
                principalTable: "Parkings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Parkings_ParkingId",
                schema: "parking",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_ParkingId",
                schema: "parking",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "ParkingId",
                schema: "parking",
                table: "Transactions");

            migrationBuilder.AddColumn<Guid>(
                name: "TransactionId",
                schema: "parking",
                table: "Parkings",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Parkings_TransactionId",
                schema: "parking",
                table: "Parkings",
                column: "TransactionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Parkings_Transactions_TransactionId",
                schema: "parking",
                table: "Parkings",
                column: "TransactionId",
                principalSchema: "parking",
                principalTable: "Transactions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
