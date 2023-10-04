using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pocketpay.Migrations
{
    /// <inheritdoc />
    public partial class transference : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Withdraw_Transaction_transactionId",
                table: "Withdraw");

            migrationBuilder.RenameColumn(
                name: "transactionId",
                table: "Withdraw",
                newName: "TransactionId");

            migrationBuilder.RenameIndex(
                name: "IX_Withdraw_transactionId",
                table: "Withdraw",
                newName: "IX_Withdraw_TransactionId");

            migrationBuilder.CreateTable(
                name: "Transferences",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    SenderId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ReceiverId = table.Column<Guid>(type: "TEXT", nullable: false),
                    TransactionId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Value = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transferences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transferences_Account_ReceiverId",
                        column: x => x.ReceiverId,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transferences_Account_SenderId",
                        column: x => x.SenderId,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transferences_Transaction_TransactionId",
                        column: x => x.TransactionId,
                        principalTable: "Transaction",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transferences_ReceiverId",
                table: "Transferences",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_Transferences_SenderId",
                table: "Transferences",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Transferences_TransactionId",
                table: "Transferences",
                column: "TransactionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Withdraw_Transaction_TransactionId",
                table: "Withdraw",
                column: "TransactionId",
                principalTable: "Transaction",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Withdraw_Transaction_TransactionId",
                table: "Withdraw");

            migrationBuilder.DropTable(
                name: "Transferences");

            migrationBuilder.RenameColumn(
                name: "TransactionId",
                table: "Withdraw",
                newName: "transactionId");

            migrationBuilder.RenameIndex(
                name: "IX_Withdraw_TransactionId",
                table: "Withdraw",
                newName: "IX_Withdraw_transactionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Withdraw_Transaction_transactionId",
                table: "Withdraw",
                column: "transactionId",
                principalTable: "Transaction",
                principalColumn: "Id");
        }
    }
}
