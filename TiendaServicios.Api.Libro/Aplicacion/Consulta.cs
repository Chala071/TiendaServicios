using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TiendaServicios.Api.Libro.DTO;
using TiendaServicios.Api.Libro.Modelo;
using TiendaServicios.Api.Libro.Persistencia;

namespace TiendaServicios.Api.Libro.Aplicacion
{
    public class Consulta
    {
        public class Ejecuta : IRequest<List<LibroMaterialDTO>>
        {
            
        }

        public class Manejador : IRequestHandler<Ejecuta, List<LibroMaterialDTO>>
        {
            private readonly LibreriaContext _contexto;
            private readonly IMapper _mapper;

            public Manejador(LibreriaContext contexto, IMapper mapper)
            {
                _contexto = contexto;
                _mapper = mapper;
            }

            public async Task<List<LibroMaterialDTO>> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var libros = await _contexto.LibreriaMaterial.ToListAsync();
                var librosDto = _mapper.Map<List<LibreriaMaterial>, List<LibroMaterialDTO>>(libros);
                return librosDto;
            }
        }
    }
}
