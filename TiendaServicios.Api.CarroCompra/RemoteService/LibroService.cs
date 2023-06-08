using System.Text.Json;
using TiendaServicios.Api.CarroCompra.RemoteInterface;
using TiendaServicios.Api.CarroCompra.RemoteModel;

namespace TiendaServicios.Api.CarroCompra.RemoteService
{
    public class LibroService : ILibroService
    {
        private readonly IHttpClientFactory _httpClient;
        private readonly ILogger<LibroService> _logger;

        public LibroService(IHttpClientFactory httpClient, ILogger<LibroService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<(bool resultado, LibroRemote? libro, string? ErrorMessage)> GetLibro(Guid LibroId)
        {
            try
            {
                var cliente = _httpClient.CreateClient("Libros");
                var response = await cliente.GetAsync($"api/Libro/{LibroId}");
                if (response.IsSuccessStatusCode)
                {
                    var contenido = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                    var resultado = JsonSerializer.Deserialize<LibroRemote>(contenido, options);
                    return (true, resultado, string.Empty);
                }

                return (false, null, response.ReasonPhrase);

            }catch(Exception ex)
            {
                _logger.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }
    }
}
