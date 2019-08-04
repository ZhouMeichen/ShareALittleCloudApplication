using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ShareALittle.Migrations
{
    public partial class ChangeProductModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PathOfImage",
                table: "Product");

            migrationBuilder.AlterColumn<string>(
                name: "ProductName",
                table: "Product",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AddColumn<byte[]>(
                name: "ProductImage",
                table: "Product",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductImage",
                table: "Product");

            migrationBuilder.AlterColumn<string>(
                name: "ProductName",
                table: "Product",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PathOfImage",
                table: "Product",
                nullable: true);
        }
    }
}
