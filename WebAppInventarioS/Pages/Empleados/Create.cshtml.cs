using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebAppInventarioS.Models;
using WebAppInventarioS.Services;

namespace WebAppInventarioS.Pages.Empleados
{
    public class CreateModel : PageModel
    {
        private readonly EmpleadoService _empleadoService;
        private readonly DepartamentoService _departamentoService;
        private readonly UbicacionService _ubicacionService;

        public CreateModel(
            EmpleadoService empleadoService,
            DepartamentoService departamentoService,
            UbicacionService ubicacionService)
        {
            _empleadoService = empleadoService;
            _departamentoService = departamentoService;
            _ubicacionService = ubicacionService;
        }

        [BindProperty]
        public Empleado Empleado { get; set; } = new Empleado();

        public List<SelectListItem> Departamentos { get; set; }
        public List<SelectListItem> Ubicaciones { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var departamentos = await _departamentoService.GetAllDepartamentos();
            Departamentos = departamentos.Select(d => new SelectListItem
            {
                Value = d.IdDepartamento.ToString(),
                Text = d.NombreDepartamento
            }).ToList();

            var ubicaciones = await _ubicacionService.GetAllUbicaciones();
            Ubicaciones = ubicaciones.Select(u => new SelectListItem
            {
                Value = u.IdUbicacion.ToString(),
                Text = u.Zona
            }).ToList();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                // Recargar listas si hay error de validaci�n
                await OnGetAsync();
                return Page();
            }
            await _empleadoService.CreateEmpleadoAsync(Empleado);
            return RedirectToPage("./Index");
        }
    }
}