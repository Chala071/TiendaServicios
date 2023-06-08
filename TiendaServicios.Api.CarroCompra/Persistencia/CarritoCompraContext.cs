using Microsoft.EntityFrameworkCore;
using TiendaServicios.Api.CarroCompra.Modelo;

namespace TiendaServicios.Api.CarroCompra.Persistencia
{
    public class CarritoCompraContext: DbContext
    {
        public CarritoCompraContext(DbContextOptions<CarritoCompraContext> options): base(options)
        {
        }

        public DbSet<CarritoSesion> CarritoSesion { get; set; }
        public DbSet<CarritoSesionDetalle> CarritoSesionDetalle { get; set; }

    }
}
