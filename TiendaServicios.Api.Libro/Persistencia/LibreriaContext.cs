using Microsoft.EntityFrameworkCore;
using TiendaServicios.Api.Libro.Modelo;

namespace TiendaServicios.Api.Libro.Persistencia
{
    public class LibreriaContext: DbContext
    {
        public LibreriaContext()
        {
            
        }
        public LibreriaContext(DbContextOptions<LibreriaContext> options): base(options)
        {
            
        }

        public virtual DbSet<LibreriaMaterial> LibreriaMaterial { get; set; }        
    }
}
