using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroService_Izumu.Test.Models
{
    public class ClienteRequest
    {
        public int Id { get; set; }
        public int TipoDocumentoId { get; set; }
        public string NumeroDocumento { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string PrimerNombre { get; set; }
        public string SegundoNombre { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
        public string DireccionResidencia { get; set; }
        public string NumeroCelular { get; set; }
        public string Email { get; set; }
        public int PlanId { get; set; }
    }
}
