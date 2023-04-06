using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestaoCurso.Infra.Migrations
{
    /// <inheritdoc />
    public partial class Secund : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "Categoria",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "Categoria");
        }
    }
}
