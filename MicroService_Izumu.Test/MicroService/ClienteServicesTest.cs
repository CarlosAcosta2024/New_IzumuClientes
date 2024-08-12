using Microservice_Izumu.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using NSubstitute;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using static Azure.Core.HttpHeader;
using Microsoft.EntityFrameworkCore;
using System.Net;
using NSubstitute.ExceptionExtensions;
using MicroService_Izumu.Test.Services;
using MicroService_Izumu.Test.Models;
using Microservice_Izumu.Data;
using Moq;


namespace MicroService_Izumu.Test.MicroService
{
    public class ClienteServicesTest
    {

        private Cliente cliente;
        private ClienteRequest clienteRequest;
        private IMapper mapper;
        [SetUp]
        public void Setup()
        {
            mapper = Substitute.For<IMapper>();

            cliente = new Cliente()
            {
                TipoDocumentoId = 1,
                NombreTipoDocumento = "Cédula de Ciudadanía",
                NumeroDocumento = "80177522",
                FechaNacimiento = Convert.ToDateTime("19/01/1985"),
                PrimerNombre = "Carlos",
                SegundoNombre = "Fernando",
                PrimerApellido = "Acosta",
                SegundoApellido = "Rodriguez",
                DireccionResidencia = "Tv 42 4-14",
                NumeroCelular = "3202884667",
                Email = "corpcap1@hotmail.com",
                PlanId = 4,
                Nombre_Plan = "Plan Familiar"

            };

            clienteRequest = new ClienteRequest()
            {
                Id = 1,
                TipoDocumentoId = 1,
                NumeroDocumento = "80177522",
                FechaNacimiento = Convert.ToDateTime("20/01/1985"),
                PrimerNombre = "Carlos",
                SegundoNombre = "Fernando",
                PrimerApellido = "Acosta",
                SegundoApellido = "Rodriguez",
                DireccionResidencia = "Calle 64c 111",
                NumeroCelular = "3202884667",
                Email = "corpcap2011@gmail.com",
                PlanId = 3
            };
        }

        private IServiceProvider CreateContext(string nameBD)
        {

            var services = new ServiceCollection();
            services.AddDbContext<ClienteDbContext>(options => options.UseInMemoryDatabase(databaseName: nameBD),
            ServiceLifetime.Scoped,
            ServiceLifetime.Scoped);

            return services.BuildServiceProvider();
        }

        [TestCase(HttpStatusCode.OK)]
        [TestCase(HttpStatusCode.InternalServerError)]
        public async Task When_Add_Employee_Services(HttpStatusCode code)
        {
            // Arrange
            var nameDB = Guid.NewGuid().ToString();
            var serviceProvider = CreateContext(nameDB);

            var db = serviceProvider.GetService<ClienteDbContext>();
            db.Clientes.Add(new Cliente { /* inicializa los campos necesarios */ });
            await db.SaveChangesAsync();

            var mapperMock = new Mock<IMapper>();

            if (code == HttpStatusCode.OK)
            {
                mapperMock.Setup(m => m.Map<Cliente>(It.IsAny<ClienteRequest>())).Returns(new Cliente { /* inicializa los campos */ });
            }
            else
            {
                mapperMock.Setup(m => m.Map<Cliente>(It.IsAny<ClienteRequest>())).Throws(new Exception("Simulated Exception"));
            }

            var services = new ClienteServices(db, mapperMock.Object);

            // Act
            var responseServices = await services.UpdateCliente(new ClienteRequest { /* inicializa los campos */ });

            // Assert
            Assert.That(responseServices.HttpCode, Is.EqualTo(code));
        }




    }
}
