using MediatR;
using Microsoft.EntityFrameworkCore;
using TiendaServicios.Api.CarroCompra.Aplicacion;
using TiendaServicios.Api.CarroCompra.Persistencia;
using TiendaServicios.Api.CarroCompra.RemoteInterface;
using TiendaServicios.Api.CarroCompra.RemoteService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<CarritoCompraContext>(options =>
{
    options.UseMySQL(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddMediatR(typeof(Nuevo.Manejador).Assembly);

builder.Services.AddHttpClient("Libros", config =>
{
    config.BaseAddress = new Uri(builder.Configuration["Services:Libros"]);
});

builder.Services.AddHttpClient("Autores", config =>
{
    config.BaseAddress = new Uri(builder.Configuration["Services:Autores"]);
});

builder.Services.AddScoped<ILibroService, LibroService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
