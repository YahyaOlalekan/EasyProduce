using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class AddproducetypeNavPropInRequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Requests_ProduceTypeId",
                table: "Requests",
                column: "ProduceTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_ProduceTypes_ProduceTypeId",
                table: "Requests",
                column: "ProduceTypeId",
                principalTable: "ProduceTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requests_ProduceTypes_ProduceTypeId",
                table: "Requests");

            migrationBuilder.DropIndex(
                name: "IX_Requests_ProduceTypeId",
                table: "Requests");
        }
    }
}
