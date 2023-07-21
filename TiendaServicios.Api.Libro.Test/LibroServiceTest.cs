using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using GenFu;
using Microsoft.EntityFrameworkCore;
using Moq;
using TiendaServicios.Api.Libro.Aplicacion;
using TiendaServicios.Api.Libro.DTO;
using TiendaServicios.Api.Libro.Modelo;
using TiendaServicios.Api.Libro.Persistencia;
using Xunit;

namespace TiendaServicios.Api.Libro.Test
{
    public class LibroServiceTest
    {

        private IEnumerable<LibreriaMaterial> ObtenerDataPrueba()
        {
            // Este metodo es para generar data de prueba
            A.Configure<LibreriaMaterial>()
                .Fill(x => x.Titulo).AsArticleTitle()                
                .Fill(x => x.LibreriaMaterialId, () => { return Guid.NewGuid(); });

            var listaLibros = A.ListOf<LibreriaMaterial>(30);

            listaLibros[0].LibreriaMaterialId = Guid.Empty;

            return listaLibros;
        }

        private Mock<LibreriaContext> CrearContexto()
        {
            var dataPrueba = ObtenerDataPrueba().AsQueryable();

            var dbSet = new Mock<DbSet<LibreriaMaterial>>();

            //Para que se utiliza: para que cuando se haga una consulta a la base de datos, se devuelva la data de prueba
            dbSet.As<IQueryable<LibreriaMaterial>>().Setup(x => x.Provider).Returns(dataPrueba.Provider);
            dbSet.As<IQueryable<LibreriaMaterial>>().Setup(x => x.Expression).Returns(dataPrueba.Expression);
            dbSet.As<IQueryable<LibreriaMaterial>>().Setup(x => x.ElementType).Returns(dataPrueba.ElementType);
            dbSet.As<IQueryable<LibreriaMaterial>>().Setup(x => x.GetEnumerator()).Returns(dataPrueba.GetEnumerator());

            dbSet.As<IAsyncEnumerable<LibreriaMaterial>>().Setup(x => x.GetAsyncEnumerator(new CancellationToken()))
                .Returns(new AsyncEnumerator<LibreriaMaterial>(dataPrueba.GetEnumerator()));

            dbSet.As<IQueryable<LibreriaMaterial>>().Setup(x => x.Provider).Returns(new AsyncQueryProvider<LibreriaMaterial>(dataPrueba.Provider));

            var contexto = new Mock<LibreriaContext>();
            contexto.Setup(x => x.LibreriaMaterial).Returns(dbSet.Object);
            return contexto;
            
        }

        [Fact]
        public async void GetLibroPorId()
        {
            var mockContexto = CrearContexto();

            var mapConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingTest());
            });

            var mapper = mapConfig.CreateMapper();

            var request = new ConsultaFiltro.LibroUnico();
            request.LibreriaMaterialId  = Guid.Empty;

            var manejador = new ConsultaFiltro.Manejador(mockContexto.Object, mapper);

            var libro = await manejador.Handle(request, new CancellationToken());

            Assert.NotNull(libro);
            Assert.True(libro.LibreriaMaterialId == Guid.Empty);
        }

        [Fact]
        public async void GetLibros() { 
            //Debugger.Launch();
            //Que metodo dentro de mi microservicio de libro se va a encargar de obtener todos los libros
            //1. Emulamos el contexto de base de datos:
            //--Para emular el contexto de base de datos, necesitamos objetos de tipo mock
            //--Que es un mock: objetos que simulan el comportamiento de objetos reales
            var mockContext = CrearContexto();

            //2. Emulamos el mapper
            var mapConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingTest());
            });

            var mapper = mapConfig.CreateMapper();

            //3. Instanciamos la clase que vamos a probar y pasarle como parametros los mocks que se han creado
            var servicio = new Consulta.Manejador(mockContext.Object, mapper);

            //4. Ejecutamos el metodo que queremos probar            
            var resultado = await servicio.Handle(new Consulta.Ejecuta(), new CancellationToken());

            //5. Verificamos el resultado
            Assert.True(resultado.Any());
        }

        [Fact]
        public async void GuardarLibro()
        {
            //Debugger.Launch();
            var options = new DbContextOptionsBuilder<LibreriaContext>()
                .UseInMemoryDatabase(databaseName: "BaseDatosLibro")
                .Options;

            var contexto = new LibreriaContext(options);

            var request = new Nuevo.Ejecuta()
            {
                Titulo = "Libro de Microservicios",
                AutorLibro = Guid.Empty,
                FechaPublicacion = DateTime.Now
            };

            var manejador = new Nuevo.Manejador(contexto);

            var libro = await manejador.Handle(request, new CancellationToken());

            Assert.True(libro != null);
        }
    }
}
