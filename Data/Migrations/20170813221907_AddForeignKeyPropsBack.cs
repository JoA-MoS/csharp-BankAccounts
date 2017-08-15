using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BankAccounts.Data.Migrations
{
    public partial class AddForeignKeyPropsBack : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Restaurants_RestaurantId",
                table: "Reviews");

            migrationBuilder.AlterColumn<int>(
                name: "RestaurantId",
                table: "Reviews",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Restaurants_RestaurantId",
                table: "Reviews",
                column: "RestaurantId",
                principalTable: "Restaurants",
                principalColumn: "RestaurantId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Restaurants_RestaurantId",
                table: "Reviews");

            migrationBuilder.AlterColumn<int>(
                name: "RestaurantId",
                table: "Reviews",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Restaurants_RestaurantId",
                table: "Reviews",
                column: "RestaurantId",
                principalTable: "Restaurants",
                principalColumn: "RestaurantId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
