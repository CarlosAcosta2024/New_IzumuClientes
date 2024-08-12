//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.RazorPages;

//namespace IzumuClientes.Pages
//{
//    public class PrivacyModel : PageModel
//    {
//        private readonly ILogger<PrivacyModel> _logger;

//        public PrivacyModel(ILogger<PrivacyModel> logger)
//        {
//            _logger = logger;
//        }
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using IzumuClientes.Configuration;
using Microsoft.Extensions.Options;
using static IzumuClientes.Models.ClienteModels;
using System.Text;

namespace IzumuClientes.Pages
{
    public class PrivacyModel : PageModel
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public PrivacyModel(HttpClient httpClient, IOptions<Microservicio> apiSettings)
        {
            _httpClient = httpClient;
            _baseUrl = apiSettings.Value.BaseUrl_Service;
        }
        [BindProperty(SupportsGet = true)]
        public int? Id { get; set; } 

        [BindProperty(SupportsGet = true)]
        public string NombreTipoDocumento { get; set; }

        [BindProperty(SupportsGet = true)]
        public string NumeroDocumento { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime FechaNacimiento { get; set; }

        [BindProperty(SupportsGet = true)]
        public string PrimerNombre { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SegundoNombre { get; set; }

        [BindProperty(SupportsGet = true)]
        public string PrimerApellido { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SegundoApellido { get; set; }

        [BindProperty(SupportsGet = true)]
        public string DireccionResidencia { get; set; }

        [BindProperty(SupportsGet = true)]
        public string NumeroCelular { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Email { get; set; }

        [BindProperty(SupportsGet = true)]
        public int PlanId { get; set; }

        public List<TipoDocumento> TiposDocumento { get; set; } = new List<TipoDocumento>();
        public List<Plan> Planes { get; set; } = new List<Plan>();

        public async Task OnGet()
        {
            //Obtener tipos de documento
            var responseTipoDocumento = await _httpClient.GetStringAsync(GetApiUrl("GetTipoDocumento"));
            var tipoDocumentoApiResponse = JsonConvert.DeserializeObject<ApiResponse<List<TipoDocumento>>>(responseTipoDocumento);
            TiposDocumento = tipoDocumentoApiResponse?.Response ?? new List<TipoDocumento>();

            // Obtener planes
            var responsePlanes = await _httpClient.GetStringAsync(GetApiUrl("GetPlan"));
            var planesApiResponse = JsonConvert.DeserializeObject<ApiResponse<List<Plan>>>(responsePlanes);
            Planes = planesApiResponse?.Response ?? new List<Plan>();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            string url;
            HttpResponseMessage response;

            if (Id.HasValue)
            {
                // Si existe un Id, se trata de una actualización
                var clienteUpdate = new ClienteUpdate
                {
                    Id = Id.Value,
                    TipoDocumentoId = int.Parse(NombreTipoDocumento),
                    NumeroDocumento = NumeroDocumento,
                    FechaNacimiento = FechaNacimiento,
                    PrimerNombre = PrimerNombre.ToUpper(),
                    SegundoNombre = SegundoNombre?.ToUpper(),
                    PrimerApellido = PrimerApellido.ToUpper(),
                    SegundoApellido = SegundoApellido?.ToUpper(),
                    DireccionResidencia = DireccionResidencia.ToUpper(),
                    NumeroCelular = NumeroCelular,
                    Email = Email.ToUpper(),
                    PlanId = PlanId
                };

                url = GetApiUrl("UpdateCliente");
                var clienteJson = JsonConvert.SerializeObject(clienteUpdate);
                var content = new StringContent(clienteJson, Encoding.UTF8, "application/json");
                response = await _httpClient.PutAsync(url, content);
            }
            else
            {
                // Si no hay Id, se trata de una creación
                var clienteCreate = new ClienteCreate
                {
                    TipoDocumentoId = int.Parse(NombreTipoDocumento),
                    NumeroDocumento = NumeroDocumento,
                    FechaNacimiento = FechaNacimiento,
                    PrimerNombre = PrimerNombre.ToUpper(),
                    SegundoNombre = SegundoNombre?.ToUpper(),
                    PrimerApellido = PrimerApellido.ToUpper(),
                    SegundoApellido = SegundoApellido?.ToUpper(),
                    DireccionResidencia = DireccionResidencia.ToUpper(),
                    NumeroCelular = NumeroCelular,
                    Email = Email.ToUpper(),
                    PlanId = PlanId
                };

                url = GetApiUrl("CreateCliente");
                var clienteJson = JsonConvert.SerializeObject(clienteCreate);
                var content = new StringContent(clienteJson, Encoding.UTF8, "application/json");
                response = await _httpClient.PostAsync(url, content);
            }
            var responseContent = await response.Content.ReadAsStringAsync();
            var apiResponse = JsonConvert.DeserializeObject<ApiResponse<string>>(responseContent);


            if (response.IsSuccessStatusCode)
            {
               
                TempData["SuccessMessage"] = apiResponse.Mensaje;

                return RedirectToPage("Index");
            }
            else
            {
                await CargarDatosAsync();

                //ModelState.AddModelError(string.Empty, "Error al procesar la solicitud:"+ apiResponse.Mensaje.ToString());
                TempData["ErrorMessage"] =  apiResponse.Mensaje;
                return RedirectToPage("AgregarCliente");
            }
        }


        private string GetApiUrl(string endpoint)
        {
            return $"{_baseUrl}/{endpoint}";
        }



        private async Task CargarDatosAsync()
        {
            // Obtener tipos de documento
            var responseTipoDocumento = await _httpClient.GetStringAsync(GetApiUrl("GetTipoDocumento"));
            var tipoDocumentoApiResponse = JsonConvert.DeserializeObject<ApiResponse<List<TipoDocumento>>>(responseTipoDocumento);
            TiposDocumento = tipoDocumentoApiResponse?.Response ?? new List<TipoDocumento>();

            // Obtener planes
            var responsePlanes = await _httpClient.GetStringAsync(GetApiUrl("GetPlan"));
            var planesApiResponse = JsonConvert.DeserializeObject<ApiResponse<List<Plan>>>(responsePlanes);
            Planes = planesApiResponse?.Response ?? new List<Plan>();
        }
    }


    

  
}
