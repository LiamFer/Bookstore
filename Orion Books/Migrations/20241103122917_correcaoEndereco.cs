using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Orion_Books.Migrations
{
    /// <inheritdoc />
    public partial class correcaoEndereco : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CEP",
                table: "Endereco",
                newName: "Cidade");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Cidade",
                table: "Endereco",
                newName: "CEP");
        }
    }
}
