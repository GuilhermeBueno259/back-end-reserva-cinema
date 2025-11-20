using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReservaCinema.Migrations
{
    /// <inheritdoc />
    public partial class AddCascadeOnDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Assentos_IdSessao",
                table: "Assentos",
                column: "IdSessao");

            migrationBuilder.AddForeignKey(
                name: "FK_Assentos_Sessoes_IdSessao",
                table: "Assentos",
                column: "IdSessao",
                principalTable: "Sessoes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assentos_Sessoes_IdSessao",
                table: "Assentos");

            migrationBuilder.DropIndex(
                name: "IX_Assentos_IdSessao",
                table: "Assentos");
        }
    }
}
