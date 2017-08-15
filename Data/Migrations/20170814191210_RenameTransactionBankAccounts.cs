using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BankAccounts.Data.Migrations
{
    public partial class RenameTransactionBankAccounts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_BankAccounts_AccountId",
                table: "Transactions");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_AccountId",
                table: "Transactions",
                newName: "IX_Transactions_BankAccountId");

            migrationBuilder.RenameColumn(
                name: "AccountId",
                table: "Transactions",
                newName: "BankAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_BankAccounts_BankAccountId",
                table: "Transactions",
                column: "BankAccountId",
                principalTable: "BankAccounts",
                principalColumn: "BankAccountId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_BankAccounts_BankAccountId",
                table: "Transactions");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_BankAccountId",
                table: "Transactions",
                newName: "IX_Transactions_AccountId");

            migrationBuilder.RenameColumn(
                name: "BankAccountId",
                table: "Transactions",
                newName: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_BankAccounts_AccountId",
                table: "Transactions",
                column: "AccountId",
                principalTable: "BankAccounts",
                principalColumn: "BankAccountId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
