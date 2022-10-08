using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Servidor.Migrations
{
    public partial class MigracionInicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Mesa",
                columns: table => new
                {
                    IdMesa = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Capacidad = table.Column<int>(type: "INTEGER", nullable: false),
                    Forma = table.Column<string>(type: "TEXT", nullable: true),
                    Precio = table.Column<float>(type: "REAL", nullable: false),
                    Disponibilidad = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mesa", x => x.IdMesa);
                });

            migrationBuilder.CreateTable(
                name: "Persona",
                columns: table => new
                {
                    idPersona = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nombre = table.Column<string>(type: "TEXT", nullable: true),
                    Direccion = table.Column<string>(type: "TEXT", nullable: true),
                    Telefono = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persona", x => x.idPersona);
                });

            migrationBuilder.CreateTable(
                name: "Citas",
                columns: table => new
                {
                    idReservacion = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IdMesa = table.Column<int>(type: "INTEGER", nullable: false),
                    idPersona = table.Column<int>(type: "INTEGER", nullable: false),
                    Nombre = table.Column<string>(type: "TEXT", nullable: true),
                    Fecha = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Ubicacion = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Citas", x => x.idReservacion);
                    table.ForeignKey(
                        name: "FK_Citas_Mesa_IdMesa",
                        column: x => x.IdMesa,
                        principalTable: "Mesa",
                        principalColumn: "IdMesa",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Citas_Persona_idPersona",
                        column: x => x.idPersona,
                        principalTable: "Persona",
                        principalColumn: "idPersona",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Mesa",
                columns: new[] { "IdMesa", "Capacidad", "Disponibilidad", "Forma", "Precio" },
                values: new object[] { 1, 4, true, "Redonda", 1500f });

            migrationBuilder.InsertData(
                table: "Mesa",
                columns: new[] { "IdMesa", "Capacidad", "Disponibilidad", "Forma", "Precio" },
                values: new object[] { 2, 6, true, "Rectangular", 2500f });

            migrationBuilder.InsertData(
                table: "Persona",
                columns: new[] { "idPersona", "Direccion", "Nombre", "Telefono" },
                values: new object[] { 1, "San Francisco", "Vismar", "829-219-6048" });

            migrationBuilder.InsertData(
                table: "Persona",
                columns: new[] { "idPersona", "Direccion", "Nombre", "Telefono" },
                values: new object[] { 2, "San Francisco", "Gregory", "829-555-0707" });

            migrationBuilder.CreateIndex(
                name: "IX_Citas_IdMesa",
                table: "Citas",
                column: "IdMesa");

            migrationBuilder.CreateIndex(
                name: "IX_Citas_idPersona",
                table: "Citas",
                column: "idPersona");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Citas");

            migrationBuilder.DropTable(
                name: "Mesa");

            migrationBuilder.DropTable(
                name: "Persona");
        }
    }
}
