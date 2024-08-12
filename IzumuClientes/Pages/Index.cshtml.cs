using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using IzumuClientes.Configuration;
using Microsoft.Extensions.Options;
using IzumuClientes.Models;
using static IzumuClientes.Models.ClienteModels;


namespace IzumuClientes.Pages
{
    public class IndexModel : PageModel
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;


        public IndexModel(HttpClient httpClient, IOptions<Microservicio> apiSettings)
        {
            _httpClient = httpClient;
            _baseUrl = apiSettings.Value.BaseUrl_Service;
        }

        public List<Cliente> Clientes { get; set; } = new List<Cliente>();
        public List<TipoDocumento> TiposDocumento { get; set; } = new List<TipoDocumento>();
        public List<Plan> Planes { get; set; } = new List<Plan>();

        public async Task OnGet()
        {
            // Obtener clientes
            var responseClientes = await _httpClient.GetStringAsync(GetApiUrl("GetClientes"));
            var clientesApiResponse = JsonConvert.DeserializeObject<ApiResponse<List<Cliente>>>(responseClientes);
            Clientes = clientesApiResponse?.Response ?? new List<Cliente>();




            // Obtener tipos de documento
            var responseTipoDocumento = await _httpClient.GetStringAsync(GetApiUrl("GetTipoDocumento"));
            var tipoDocumentoApiResponse = JsonConvert.DeserializeObject<ApiResponse<List<TipoDocumento>>>(responseTipoDocumento);
            TiposDocumento = tipoDocumentoApiResponse?.Response ?? new List<TipoDocumento>();

            // Obtener planes
            //var responsePlanes = await _httpClient.GetStringAsync(GetApiUrl("GetPlan"));
            //var planesApiResponse = JsonConvert.DeserializeObject<ApiResponse<List<Plan>>>(responsePlanes);
            //Planes = planesApiResponse?.Response ?? new List<Plan>();
        }

        public async Task<IActionResult> OnPostDeleteClienteAsync(int Id)
        {
            var response = await _httpClient.DeleteAsync(GetApiUrl($"DeleteCliente/{Id}"));
            // Obtener el mensaje del servicio si es exitoso
            var responseContent = await response.Content.ReadAsStringAsync();
            var apiResponse = JsonConvert.DeserializeObject<ApiResponse<string>>(responseContent);


            if (response.IsSuccessStatusCode)
            {   
                TempData["SuccessMessage"] = apiResponse.Mensaje;
            }
            else {
                TempData["ErrorMessage"] = apiResponse.Mensaje;
            }
            return RedirectToPage();
        }



        private string GetApiUrl(string endpoint)
        {
            return $"{_baseUrl}/{endpoint}";
        }
    }

}
