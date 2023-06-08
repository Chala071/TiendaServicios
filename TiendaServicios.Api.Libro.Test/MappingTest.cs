using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using TiendaServicios.Api.Libro.DTO;
using TiendaServicios.Api.Libro.Modelo;

namespace TiendaServicios.Api.Libro.Test
{
    public class MappingTest: Profile
    {
        public MappingTest()
        {
            CreateMap<LibreriaMaterial, LibroMaterialDTO>();
        }
    }
}
