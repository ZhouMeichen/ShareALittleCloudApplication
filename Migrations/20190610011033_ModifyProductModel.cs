using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ShareALittle.Migrations
{
    public partial class ModifyProductModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "ProductImage",
                table: "Product",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContactDetail",
                table: "Product",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OwnerID",
                table: "Product",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContactDetail",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "OwnerID",
                table: "Product");

            migrationBuilder.AlterColumn<byte[]>(
                name: "ProductImage",
                table: "Product",
                nullable: true,
                oldClrType: typeof(byte[]));
        }
    }
}
