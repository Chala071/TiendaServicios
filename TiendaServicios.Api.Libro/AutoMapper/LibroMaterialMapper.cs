using AutoMapper;
using TiendaServicios.Api.Libro.DTO;
using TiendaServicios.Api.Libro.Modelo;

namespace TiendaServicios.Api.Libro.AutoMapper
{
    public class LibroMaterialMapper: Profile
    {
        public LibroMaterialMapper()
        {
            CreateMap<LibreriaMaterial, LibroMaterialDTO>();
        }
    }
}
