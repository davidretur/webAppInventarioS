using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebAppInventarioS.Models;
using WebAppInventarioS.Services;

namespace WebAppInventarioS.Pages.Equipos
{
    public class EditModel : PageModel
    {
        private readonly EquiposService _equiposService;
        private readonly UbicacionService _ubicacionService;
        private readonly DepartamentoService _departamentoService;
        private readonly EmpleadoService _empleadoService;
        public EditModel(EquiposService equiposService, UbicacionService ubicacionService, DepartamentoService departamentoService, EmpleadoService empleadoService)
        {
            _equiposService = equiposService;
            _ubicacionService = ubicacionService;
            _departamentoService = departamentoService;
            _empleadoService = empleadoService;
        }

        [BindProperty]
        public Equipo Equipo { get; set; } = new Equipo();
        public List<SelectListItem> Ubicaciones { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> Departamentos { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> Empleados { get; set; } = new List<SelectListItem>();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Equipo = await _equiposService.GetEquipoById(id);
            if (Equipo == null)
            {
                return NotFound();
            }
            Ubicaciones = (await _ubicacionService.GetAllUbicaciones())
                .Select(u => new SelectListItem { Value = u.IdUbicacion.ToString(), Text = u.Zona }).ToList();
            Departamentos = (await _departamentoService.GetAllDepartamentos())
                .Select(d => new SelectListItem { Value = d.IdDepartamento.ToString(), Text = d.NombreDepartamento }).ToList();
            Empleados = (await _empleadoService.GetAllEmpleadosU())
                .Select(e => new SelectListItem { Value = e.IdEmpleado.ToString(), Text = e.Nombre + " " + e.ApellidoP + " " + e.ApellidoM }).ToList();
            return Page();
        }
        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                // Recargar listas si hay error de validación
                await OnGetAsync(id);
                return Page();
            }
            await _equiposService.UpdateEquipo(id, Equipo);
            return RedirectToPage("./Index");
        }
    }
}
