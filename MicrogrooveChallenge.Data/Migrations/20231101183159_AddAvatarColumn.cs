using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MicrogrooveChallenge.Data.Migrations
{
    public partial class AddAvatarColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Avatar",
                table: "Customer",
                type: "BLOB",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Avatar",
                table: "Customer");
        }
    }
}
