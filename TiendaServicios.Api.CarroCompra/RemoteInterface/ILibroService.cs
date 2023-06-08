using TiendaServicios.Api.CarroCompra.RemoteModel;

namespace TiendaServicios.Api.CarroCompra.RemoteInterface
{
    public interface ILibroService
    {
        Task<(bool resultado, LibroRemote? libro, string? ErrorMessage)> GetLibro(Guid LibroId);
    }
}
