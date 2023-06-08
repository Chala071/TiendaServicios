using MediatR;
using TiendaServicios.Api.CarroCompra.Modelo;
using TiendaServicios.Api.CarroCompra.Persistencia;

namespace TiendaServicios.Api.CarroCompra.Aplicacion
{
    public class Nuevo
    {
        public class Ejecuta: IRequest
        {
            public DateTime FechaCreacionSesion { get; set; }
            public List<string> ProductoLista { get; set; }
        }

        public class Manejador : IRequestHandler<Ejecuta>
        {
            private readonly CarritoCompraContext _contexto;
            public Manejador(CarritoCompraContext contexto)
            {
                _contexto = contexto;
            }

            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var carritoSesion = new CarritoSesion
                {
                    FechaCreacion = request.FechaCreacionSesion
                };

                _contexto.CarritoSesion.Add(carritoSesion);
                var value = await _contexto.SaveChangesAsync();
                if (value == 0)
                {
                    throw new Exception("No se pudo insertar el carrito de compras");
                }
                
                int id = carritoSesion.CarritoSesionId;

                foreach (var producto in request.ProductoLista)
                {
                    var detalleSesion = new CarritoSesionDetalle
                    {
                        FechaCreacion = DateTime.Now,
                        CarritoSesionId = id,
                        ProductoSeleccionado = producto
                    };

                    _contexto.CarritoSesionDetalle.Add(detalleSesion);
                }

                value = await _contexto.SaveChangesAsync();
                if (value > 0)
                {
                    return Unit.Value;
                }

                throw new Exception("No se pudo insertar el detalle del carrito de compras");
            }
        }
    }
}
