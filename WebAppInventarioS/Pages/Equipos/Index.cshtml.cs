using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebAppInventarioS.Models;
using WebAppInventarioS.Models.Dtos;
using WebAppInventarioS.Services;

namespace WebAppInventarioS.Pages.Equipos
{
    public class IndexModel : PageModel
    {
        private readonly EquiposService _equiposService;
        private readonly UbicacionService _ubicacionService;
        private readonly DepartamentoService _departamentoService;
        public IndexModel(EquiposService equiposService, UbicacionService ubicacionService, DepartamentoService departamentoService, EmpleadoService empleadoService)
        {
            _equiposService = equiposService;
            _ubicacionService = ubicacionService;
            _departamentoService = departamentoService;
        }
        public List<EquipoDto> Equipos { get; set; } = new List<EquipoDto>();

        public async Task OnGetAsync()
        {
            Equipos = await _equiposService.GetAllEquiposLink(
                _departamentoService,
                _ubicacionService);
        }
    }
}
