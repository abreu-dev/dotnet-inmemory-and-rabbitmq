using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Supply.Infra.Data.Migrations
{
    public partial class VeiculoModeloLinkInVeiculo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "VeiculoModeloId",
                table: "Veiculo",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Veiculo_VeiculoModeloId",
                table: "Veiculo",
                column: "VeiculoModeloId");

            migrationBuilder.AddForeignKey(
                name: "FK_Veiculo_VeiculoModelo_VeiculoModeloId",
                table: "Veiculo",
                column: "VeiculoModeloId",
                principalTable: "VeiculoModelo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Veiculo_VeiculoModelo_VeiculoModeloId",
                table: "Veiculo");

            migrationBuilder.DropIndex(
                name: "IX_Veiculo_VeiculoModeloId",
                table: "Veiculo");

            migrationBuilder.DropColumn(
                name: "VeiculoModeloId",
                table: "Veiculo");
        }
    }
}
