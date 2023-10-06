using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pocketpay.Migrations
{
    /// <inheritdoc />
    public partial class removedStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transferences_Account_ReceiverId",
                table: "Transferences");

            migrationBuilder.DropForeignKey(
                name: "FK_Transferences_Account_SenderId",
                table: "Transferences");

            migrationBuilder.DropForeignKey(
                name: "FK_Transferences_Transaction_TransactionId",
                table: "Transferences");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Transaction");

            migrationBuilder.AlterColumn<Guid>(
                name: "TransactionId",
                table: "Transferences",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<Guid>(
                name: "SenderId",
                table: "Transferences",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<Guid>(
                name: "ReceiverId",
                table: "Transferences",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AddForeignKey(
                name: "FK_Transferences_Account_ReceiverId",
                table: "Transferences",
                column: "ReceiverId",
                principalTable: "Account",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transferences_Account_SenderId",
                table: "Transferences",
                column: "SenderId",
                principalTable: "Account",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transferences_Transaction_TransactionId",
                table: "Transferences",
                column: "TransactionId",
                principalTable: "Transaction",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transferences_Account_ReceiverId",
                table: "Transferences");

            migrationBuilder.DropForeignKey(
                name: "FK_Transferences_Account_SenderId",
                table: "Transferences");

            migrationBuilder.DropForeignKey(
                name: "FK_Transferences_Transaction_TransactionId",
                table: "Transferences");

            migrationBuilder.AlterColumn<Guid>(
                name: "TransactionId",
                table: "Transferences",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "SenderId",
                table: "Transferences",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ReceiverId",
                table: "Transferences",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Transaction",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Transferences_Account_ReceiverId",
                table: "Transferences",
                column: "ReceiverId",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transferences_Account_SenderId",
                table: "Transferences",
                column: "SenderId",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transferences_Transaction_TransactionId",
                table: "Transferences",
                column: "TransactionId",
                principalTable: "Transaction",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
