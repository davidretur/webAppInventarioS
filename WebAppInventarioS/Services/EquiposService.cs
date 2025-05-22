using WebAppInventarioS.Models;
using WebAppInventarioS.Models.Dtos;

namespace WebAppInventarioS.Services
{
    public class EquiposService
    {
        private readonly HttpClient _httpClient;
        public EquiposService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("InventarioApi");
        }
        public async Task<List<Equipo>> GetAllEquipos()
        {
            return await _httpClient.GetFromJsonAsync<List<Equipo>>("api/Equipo");
        }
            public async Task<List<EquipoDto>> GetAllEquiposLink(
                DepartamentoService departamentoService,
                UbicacionService ubicacionService
                )
            {
                var equipos = await _httpClient.GetFromJsonAsync<List<Equipo>>("api/Equipo");
                var departamentos = await departamentoService.GetAllDepartamentos();
                var ubicaciones = await ubicacionService.GetAllUbicaciones();

                var resultado = from e in equipos
                                join d in departamentos on e.IdDepartamento equals d.IdDepartamento into deptos
                                from d in deptos.DefaultIfEmpty()
                                join u in ubicaciones on e.IdUbicacion equals u.IdUbicacion into ubis
                                from u in ubis.DefaultIfEmpty()
                                select new EquipoDto
                                {
                                    IdEquipo = e.IdEquipo,
                                    NumeroSerie = e.NumeroSerie,
                                    Marca = e.Marca,
                                    Modelo = e.Modelo,
                                    Ip = e.Ip,
                                    Ram = e.Ram,
                                    DiscoDuro = e.DiscoDuro,
                                    Procesador = e.Procesador,
                                    So = e.So,
                                    EquipoEstatus = e.EquipoEstatus,
                                    Empresa = e.Empresa,
                                    Renovar = e.Renovar,
                                    FechaUltimaCaptura = e.FechaUltimaCaptura,
                                    FechaUltimoMantto = e.FechaUltimoMantto,
                                    ElaboroResponsiva = e.ElaboroResponsiva,
                                    Zona = u?.Zona ?? "",
                                    NombreDepartamento = d?.NombreDepartamento ?? "",
                                    Status = e.Status
                                };

                return resultado.ToList();
            }
        public async Task<Equipo> GetEquipoById(int id)
        {
            return await _httpClient.GetFromJsonAsync<Equipo>($"api/Equipo/{id}");
        }
        public async Task<Equipo> CreateEquipo(Equipo equipo)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Equipo", equipo);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Equipo>();
        }
        public async Task UpdateEquipo(int id, Equipo equipo)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/Equipo/{id}", equipo);
            response.EnsureSuccessStatusCode();
        }
        public async Task DeleteEquipoAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/Equipo/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
