using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TiendaServicios.Api.Libro.Aplicacion;
using TiendaServicios.Api.Libro.DTO;

namespace TiendaServicios.Api.Libro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibroController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LibroController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Crear(Nuevo.Ejecuta data)
        {
            return await _mediator.Send(data);
        }

        [HttpGet]
        public async Task<ActionResult<List<LibroMaterialDTO>>> GetLibros()
        {
            return await _mediator.Send(new Consulta.Ejecuta());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LibroMaterialDTO>> GetLibroUnico(Guid id)
        {
            return await _mediator.Send(new ConsultaFiltro.LibroUnico { LibreriaMaterialId = id });
        }
    }
}
