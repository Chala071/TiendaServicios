using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TiendaServicios.Api.CarroCompra.Migrations
{
    /// <inheritdoc />
    public partial class MigrationMySql70623 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreacion",
                table: "CarritoSesionDetalle",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaCreacion",
                table: "CarritoSesionDetalle");
        }
    }
}
