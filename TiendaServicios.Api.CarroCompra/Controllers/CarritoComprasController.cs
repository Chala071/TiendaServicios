using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TiendaServicios.Api.CarroCompra.Aplicacion;
using TiendaServicios.Api.CarroCompra.DTO;

namespace TiendaServicios.Api.CarroCompra.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarritoComprasController : ControllerBase
    {
        private readonly IMediator mediator;

        public CarritoComprasController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Crear(Nuevo.Ejecuta data)
        {
            return await mediator.Send(data);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CarritoDTO>> GetCarrito(int id)
        {
            return await mediator.Send(new Consulta.Ejecuta { CarroSesionId = id });
        }


    }
}
