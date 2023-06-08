using AutoMapper;
using TiendaServicios.Api.Autor.DTO;
using TiendaServicios.Api.Autor.Modelo;

namespace TiendaServicios.Api.Autor.AutoMapper
{
    public class AutorMapper: Profile
    {
        public AutorMapper()
        {
            CreateMap<AutorLibro, AutorDto>();
        }
    }
}
