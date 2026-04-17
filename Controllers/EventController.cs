using Despliegue.Data;
using Despliegue.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Despliegue.Controllers
{
    public class EventsController : Controller
    {
        private readonly AppDbContext _context;

        public EventsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Galería pública
        public async Task<IActionResult> Gallery(string? categoria, string? orden)
        {
            var eventos = _context.Events.AsQueryable();

            if (!string.IsNullOrEmpty(categoria))
                eventos = eventos.Where(e => e.Categoria == categoria);

            eventos = orden switch
            {
                "fecha_asc"  => eventos.OrderBy(e => e.Fecha),
                "fecha_desc" => eventos.OrderByDescending(e => e.Fecha),
                _            => eventos.OrderBy(e => e.Fecha)
            };

            ViewBag.CategoriaActual = categoria;
            ViewBag.OrdenActual     = orden;
            ViewBag.Categorias      = await _context.Events
                                        .Select(e => e.Categoria)
                                        .Distinct()
                                        .ToListAsync();

            return View(await eventos.ToListAsync());
        }

        //===============================================================================


        // GET: Panel admin
        public async Task<IActionResult> Index()
        {
            return View(await _context.Events.OrderByDescending(e => e.Fecha).ToListAsync());
        }

        //===============================================================================

        // GET: Crear
        public IActionResult Create() => View();

        // POST: Crear
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(Event evento)
        {
            if (!ModelState.IsValid) return View(evento);

            _context.Events.Add(evento);
            await _context.SaveChangesAsync();
            TempData["Mensaje"] = "Evento creado exitosamente.";
            return RedirectToAction(nameof(Index));
        }

        //===============================================================================

        // GET: Editar
        public async Task<IActionResult> Edit(int id)
        {
            var evento = await _context.Events.FindAsync(id);
            if (evento is null) return NotFound();
            return View(evento);
        }

        // POST: Editar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Event evento)
        {
            if (id != evento.Id) return BadRequest();
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
            var evento = await _context.Events.FindAsync(id);
            if (evento is null) return NotFound();
            return View(evento);
        }

        // POST: Eliminar
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var evento = await _context.Events.FindAsync(id);
            if (evento is not null)
            {
                _context.Events.Remove(evento);
                await _context.SaveChangesAsync();
            }
            TempData["Mensaje"] = "Evento eliminado.";
            return RedirectToAction(nameof(Index));
        }
    }
}