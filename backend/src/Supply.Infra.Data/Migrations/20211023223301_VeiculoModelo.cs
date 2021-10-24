using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Supply.Infra.Data.Migrations
{
    public partial class VeiculoModelo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VeiculoModelo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Removed = table.Column<bool>(type: "bit", nullable: false),
                    VeiculoMarcaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VeiculoModelo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VeiculoModelo_VeiculoMarca_VeiculoMarcaId",
                        column: x => x.VeiculoMarcaId,
                        principalTable: "VeiculoMarca",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VeiculoModelo_VeiculoMarcaId",
                table: "VeiculoModelo",
                column: "VeiculoMarcaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VeiculoModelo");
        }
    }
}
