using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiWebExamenEf.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Persona",
                columns: table => new
                {
                    PersonaId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Nombres = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Apellidos = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NivelEstrato = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persona", x => x.PersonaId);
                });

            migrationBuilder.CreateTable(
                name: "Servicio",
                columns: table => new
                {
                    ServicioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NommbreServicio = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DescripcionServicio = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servicio", x => x.ServicioId);
                });

            migrationBuilder.CreateTable(
                name: "DetallePaS",
                columns: table => new
                {
                    DetalleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServicioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PersonaId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetallePaS", x => x.DetalleId);
                    table.ForeignKey(
                        name: "FK_DetallePaS_Persona_PersonaId",
                        column: x => x.PersonaId,
                        principalTable: "Persona",
                        principalColumn: "PersonaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DetallePaS_Servicio_ServicioId",
                        column: x => x.ServicioId,
                        principalTable: "Servicio",
                        principalColumn: "ServicioId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DetallePaS_PersonaId",
                table: "DetallePaS",
                column: "PersonaId");

            migrationBuilder.CreateIndex(
                name: "IX_DetallePaS_ServicioId",
                table: "DetallePaS",
                column: "ServicioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DetallePaS");

            migrationBuilder.DropTable(
                name: "Persona");

            migrationBuilder.DropTable(
                name: "Servicio");
        }
    }
}
