using Microsoft.EntityFrameworkCore;
using TiendaServicios.Api.Autor.Modelo;

namespace TiendaServicios.Api.Autor.Persistencia
{
    public class ContextAutor: DbContext
    {
        public ContextAutor(DbContextOptions<ContextAutor> options): base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }

        public DbSet<AutorLibro> AutorLibro { get; set; }
        public DbSet<GradoAcademico> GradoAcademico { get; set; }

    }
}
