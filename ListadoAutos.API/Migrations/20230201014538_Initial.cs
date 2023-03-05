using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ListadoAutos.API.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Autos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Marca = table.Column<string>(type: "TEXT", nullable: false),
                    Modelo = table.Column<string>(type: "TEXT", nullable: false),
                    Placa = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Autos", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Autos",
                columns: new[] { "Id", "Marca", "Modelo", "Placa" },
                values: new object[,]
                {
                    { 1, "Honda", "Civic", "ABC1234" },
                    { 2, "Toyota", "Prado", "ABC1234" },
                    { 3, "Audi", "A5", "ABC1234" },
                    { 4, "Chevrolet", "Spark", "ABC1234" },
                    { 5, "BMW", "M3", "ABC1234" },
                    { 6, "Nissan", "Note", "ABC1234" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Autos");
        }
    }
}
