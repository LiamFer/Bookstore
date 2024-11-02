using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Orion_Books.Migrations
{
    /// <inheritdoc />
    public partial class AddSinopseAndDisponivelToLivro : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Sinopse",
                table: "Livros",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sinopse",
                table: "Livros");
        }
    }
}
