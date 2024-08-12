namespace IzumuClientes.Models
{
    public class ClienteModels
    {
        public class ApiResponse<T>
        {
            public string Mensaje { get; set; }
            public T Response { get; set; }
        }

        public class Cliente
        {
            public int Id { get; set; }
            public int TipoDocumentoId { get; set; }
            public string NombreTipoDocumento { get; set; }
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
            public string Nombre_Plan { get; set; }
        }

        public class ClienteUpdate
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


        public class ClienteCreate
        {
            
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

        public class TipoDocumento
        {
            public int Id { get; set; }
            public string Cod_TipoDocumento { get; set; }
            public string NombreTipoDocumento { get; set; }
        }

        public class Plan
        {
            public int Id { get; set; }
            public string Nombre_Plan { get; set; }
        }
    }
}
