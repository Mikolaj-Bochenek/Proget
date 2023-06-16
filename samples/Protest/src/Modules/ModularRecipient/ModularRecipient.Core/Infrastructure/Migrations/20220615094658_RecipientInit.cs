using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ModularRecipient.Core.Infrastructure.Migrations
{
    public partial class RecipientInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "recipient");

            migrationBuilder.CreateTable(
                name: "RecipientMessage",
                schema: "recipient",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipientMessage", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RecipientMessage",
                schema: "recipient");
        }
    }
}
