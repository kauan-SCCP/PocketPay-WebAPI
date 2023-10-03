using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pocketpay.Migrations
{
    /// <inheritdoc />
    public partial class transactionRefactor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Account_FromId",
                table: "Transaction");

            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Account_ToId",
                table: "Transaction");

            migrationBuilder.DropIndex(
                name: "IX_Transaction_FromId",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "FromId",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "Value",
                table: "Transaction");

            migrationBuilder.RenameColumn(
                name: "ToId",
                table: "Transaction",
                newName: "OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_Transaction_ToId",
                table: "Transaction",
                newName: "IX_Transaction_OwnerId");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Transaction",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Transaction",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_Account_OwnerId",
                table: "Transaction",
                column: "OwnerId",
                principalTable: "Account",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Account_OwnerId",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Transaction");

            migrationBuilder.RenameColumn(
                name: "OwnerId",
                table: "Transaction",
                newName: "ToId");

            migrationBuilder.RenameIndex(
                name: "IX_Transaction_OwnerId",
                table: "Transaction",
                newName: "IX_Transaction_ToId");

            migrationBuilder.AddColumn<Guid>(
                name: "FromId",
                table: "Transaction",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Value",
                table: "Transaction",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_FromId",
                table: "Transaction",
                column: "FromId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_Account_FromId",
                table: "Transaction",
                column: "FromId",
                principalTable: "Account",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_Account_ToId",
                table: "Transaction",
                column: "ToId",
                principalTable: "Account",
                principalColumn: "Id");
        }
    }
}
