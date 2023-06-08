using System.Threading.RateLimiting;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TiendaServicios.Api.Autor.Aplicacion;
using TiendaServicios.Api.Autor.Persistencia;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ContextAutor>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("ConexionDB"));
});

builder.Services.AddFluentValidation(cfg => cfg.RegisterValidatorsFromAssemblyContaining<Nuevo>());

builder.Services.AddAutoMapper(typeof(Consulta.Manejador));


builder.Services.AddMediatR(typeof(Nuevo.Manejador).Assembly);
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
