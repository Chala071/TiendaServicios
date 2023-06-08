using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TiendaServicios.Api.Libro.DTO;
using TiendaServicios.Api.Libro.Modelo;
using TiendaServicios.Api.Libro.Persistencia;

namespace TiendaServicios.Api.Libro.Aplicacion
{
    public class ConsultaFiltro
    {
        public class LibroUnico: IRequest<LibroMaterialDTO>
        {
            public Guid? LibreriaMaterialId { get; set; }
        }

        public class Manejador : IRequestHandler<LibroUnico, LibroMaterialDTO>
        {

            private readonly LibreriaContext _contexto;
            private readonly IMapper _mapper;

            public Manejador(LibreriaContext context, IMapper mapper)
            {
                _contexto = context;
                _mapper = mapper;
            }

            public LibreriaContext Context { get; }
            public IMapper Mapper { get; }

            public async Task<LibroMaterialDTO> Handle(LibroUnico request, CancellationToken cancellationToken)
            {
                var libro = await _contexto.LibreriaMaterial.Where(x => x.LibreriaMaterialId == request.LibreriaMaterialId).FirstOrDefaultAsync();

                if(libro == null)
                {
                    throw new Exception("No se encontro el libro");
                }

                var libroDto = _mapper.Map<LibreriaMaterial, LibroMaterialDTO>(libro);
                return libroDto;
            }
        }
    }
}
