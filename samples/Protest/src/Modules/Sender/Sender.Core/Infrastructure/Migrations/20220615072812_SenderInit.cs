using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sender.Core.Infrastructure.Migrations
{
    public partial class SenderInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "sender");

            migrationBuilder.CreateTable(
                name: "SenderMessage",
                schema: "sender",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SenderMessage", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SenderMessage",
                schema: "sender");
        }
    }
}
