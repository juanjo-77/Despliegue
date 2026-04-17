using Despliegue.Data;
using Despliegue.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Despliegue.Controllers
{
    public class EventsController : Controller
    {
        private readonly AppDbContext _context;      // Guarda en esa variable la DB

        public EventsController(AppDbContext context)
        {
            _context = context;  // Le inyectamos el AppDbContext para acceder a MySql
        }

        //===============================================================================

        // GET: Galería pública  -  Filtros
        public async Task<IActionResult> Gallery(string? categoria, string? orden)  // Metodo asincrono recibe dos parametros
        {
            var eventos = _context.Events.AsQueryable();   // Crea consulta a la tabla 

            if (!string.IsNullOrEmpty(categoria))           //Dependiendo de la categoria que eligio 
                eventos = eventos.Where(e => e.Categoria == categoria);       // se busca y se agrega en la pantalla

            eventos = orden switch    //segun el orden 
            {
                "fecha_asc"  => eventos.OrderBy(e => e.Fecha),     //ascendente
                "fecha_desc" => eventos.OrderByDescending(e => e.Fecha),   //descendente
                _            => eventos.OrderBy(e => e.Fecha)     // Default
            };

            // Contenedor para datos extras
            ViewBag.CategoriaActual = categoria;    
            ViewBag.OrdenActual     = orden;
            ViewBag.Categorias      = await _context.Events
                                        .Select(e => e.Categoria)   // Marcar SELECT, filtro
                                        .Distinct()
                                        .ToListAsync();

            return View(await eventos.ToListAsync());   // se ejecuta la consulta
        }

        //===============================================================================


        // GET: Panel admin
        public async Task<IActionResult> Index()
        {       // Trae todos los eventos del mas reciente al mas antiguo
            return View(await _context.Events.OrderByDescending(e => e.Fecha).ToListAsync());
        }

        //===============================================================================

        // GET: Crear
        public IActionResult Create() => View();

        // POST: Crear
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(Event evento)   // toman lo del formulario del HTML 
        {                                                       // y lo comvierten a Evento
            if (!ModelState.IsValid) return View(evento);// validacion de formulario

            _context.Events.Add(evento);   //add the event 
            await _context.SaveChangesAsync();  // guarda cambios

            TempData["Mensaje"] = "Evento creado exitosamente.";  // mensaje temporal
            return RedirectToAction(nameof(Index)); // lo manda al index cuando acabe
        }

        //===============================================================================

        // GET: Editar
        public async Task<IActionResult> Edit(int id)
        {
            var evento = await _context.Events.FindAsync(id);  // busca clave primaria 
            if (evento is null) return NotFound();   // si no existe devuelve error
            return View(evento);
        }

        // POST: Editar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Event evento)
        {
            if (id != evento.Id) return BadRequest();  // verifica que la id del url = la del formulario
            if (!ModelState.IsValid) return View(evento);

            _context.Update(evento);
            await _context.SaveChangesAsync();

            TempData["Mensaje"] = "Evento actualizado exitosamente.";
            return RedirectToAction(nameof(Index));
        }

        //===============================================================================

        // GET: Eliminar
        public async Task<IActionResult> Delete(int id)
        {
            var evento = await _context.Events.FindAsync(id);   // busca el evento y lo manda a la vista
            if (evento is null) return NotFound();   // validacion
            return View(evento);
        }

        // POST: Eliminar
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var evento = await _context.Events.FindAsync(id);   // busca el evento 
            if (evento is not null)
            {
                _context.Events.Remove(evento);    // si existe lo elimina con remove
                await _context.SaveChangesAsync();
            }
            TempData["Mensaje"] = "Evento eliminado.";
            return RedirectToAction(nameof(Index));
        }
    }
}