namespace TiendaServicios.Api.CarroCompra.DTO
{
    public class CarritoDTO
    {
        public int CarritoId { get; set; }
        public DateTime? FechaCreacionSesion { get; set; }
        public List<CarritoDetalleDTO> ListaDetalle { get; set; }
    }
}
