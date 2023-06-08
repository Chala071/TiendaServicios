using MediatR;
using Microsoft.EntityFrameworkCore;
using TiendaServicios.Api.CarroCompra.DTO;
using TiendaServicios.Api.CarroCompra.Persistencia;
using TiendaServicios.Api.CarroCompra.RemoteInterface;

namespace TiendaServicios.Api.CarroCompra.Aplicacion
{
    public class Consulta
    {
        public class Ejecuta: IRequest<CarritoDTO>
        {
            public int CarroSesionId { get; set; }
        }

        public class Manejador : IRequestHandler<Ejecuta, CarritoDTO>
        {
            private readonly CarritoCompraContext _contexto;
            private readonly ILibroService _libroService;

            public Manejador(CarritoCompraContext contexto, ILibroService libroService)
            {
                _contexto = contexto;
                _libroService = libroService;
            }

            public async Task<CarritoDTO> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var carritoSesion = await _contexto.CarritoSesion.FirstOrDefaultAsync(x => x.CarritoSesionId == request.CarroSesionId);
                var carritoSesionDetalle = await _contexto.CarritoSesionDetalle.Where(x => x.CarritoSesionId == request.CarroSesionId).ToListAsync();

                var listaCarritoDto = new List<CarritoDetalleDTO>();

                foreach(var libro in carritoSesionDetalle)
                {
                    var response = await _libroService.GetLibro(Guid.Parse(libro.ProductoSeleccionado));
                    if(response.resultado)
                    {
                        var objetoLibro = response.libro;
                        if (objetoLibro != null)
                        {
                            var carritoDetalle = new CarritoDetalleDTO
                            {
                                TituloLibro = objetoLibro.Titulo,
                                FechaPublicacion = objetoLibro.FechaPublicacion,
                                LibroId = objetoLibro.LibreriaMaterialId
                            };
                            listaCarritoDto.Add(carritoDetalle);
                        }                        
                    }
                }

                if (carritoSesion != null)
                {
                    var carritoSesionDto = new CarritoDTO
                    {
                        CarritoId = carritoSesion.CarritoSesionId,
                        FechaCreacionSesion = carritoSesion.FechaCreacion,
                        ListaDetalle = listaCarritoDto
                    };

                    return carritoSesionDto;
                }

                throw new Exception("No se pudo encontrar el carrito de compras");
            }
        }
    }
}
