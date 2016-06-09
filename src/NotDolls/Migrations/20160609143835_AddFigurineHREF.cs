using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NotDolls.Migrations
{
    public partial class AddFigurineHREF : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inventory_Image_Inventory_InventoryId",
                table: "Inventory_Image");

            migrationBuilder.DropIndex(
                name: "IX_Inventory_Image_InventoryId",
                table: "Inventory_Image");

            migrationBuilder.AddColumn<string>(
                name: "FigurineHREF",
                table: "Geek",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FigurineHREF",
                table: "Geek");

            migrationBuilder.CreateIndex(
                name: "IX_Inventory_Image_InventoryId",
                table: "Inventory_Image",
                column: "InventoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Inventory_Image_Inventory_InventoryId",
                table: "Inventory_Image",
                column: "InventoryId",
                principalTable: "Inventory",
                principalColumn: "InventoryId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
