using BibliotecaWebApplication.Data;
using BibliotecaWebApplication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaWebApplication.Controllers
{
    [Authorize(Roles = "Bibliotecario, Administrador")]
    public class EjemplaresController : Controller
    {

        private readonly ApplicationDbContext _context;

        public EjemplaresController(ApplicationDbContext context)
        {
            _context = context;
        }


        // GET: EjemplaresController
        public async Task<IActionResult> Index()
        {
            var ejemplares = _context.Ejemplares
                .Include(e => e.Publicacion)
                    .ThenInclude(p => p.Libros)  // Incluimos los libros relacionados
                .Include(e => e.Publicacion)
                    .ThenInclude(p => p.Revistas) // Incluimos las revistas relacionadas
                .Include(e => e.Estante);



            return View(await ejemplares.ToListAsync());
        }

        // GET: EjemplaresController/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ejemplar = await _context.Ejemplares
                .Include(e => e.Publicacion)
                    .ThenInclude(p => p.Libros)
                .Include(e => e.Publicacion)
                    .ThenInclude(p => p.Revistas)
                .Include(e => e.Estante)
                .FirstOrDefaultAsync(m => m.EjemplarId == id);

            if (ejemplar == null)
            {
                return NotFound();
            }

            ViewData["PublicacionTexto"] = ejemplar.Publicacion.Libros.Any()
                ? $"{ejemplar.Publicacion.Libros.First().Titulo} (Libro)"
                    : ejemplar.Publicacion.Revistas.Any()
                ? $"{ejemplar.Publicacion.Revistas.First().Nombre} (Revista)"
                    : "Desconocido";

            return View(ejemplar);
        }


        // GET: EjemplaresController/Create
        public ActionResult Create()
        {
            // Obtener todas las publicaciones con sus libros o revistas asociadas
            var publicaciones = _context.Publicaciones
                .Include(p => p.Libros)
                .Include(p => p.Revistas)
                .ToList();

            // Crear una lista de selecciones con el PublicacionId como valor
            ViewData["PublicacionId"] = new SelectList(publicaciones.Select(p => new
            {
                PublicacionId = p.PublicacionId,
                Texto = p.Libros.Any()
                    ? $"{p.Libros.First().Titulo} (Libro)"
                        : p.Revistas.Any()
                    ? $"{p.Revistas.First().Nombre} (Revista)"
                        : "Desconocido"
            }), "PublicacionId", "Texto");

            ViewData["EstanteId"] = new SelectList(_context.Estantes, "EstanteId", "EstanteId");

            return View();
        }


        // POST: EjemplaresController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EjemplarId,PublicacionId,EstanteId")] Ejemplar ejemplar)
        {
            //if (ModelState.IsValid)
            {
                _context.Add(ejemplar);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewData["PublicacionId"] = new SelectList(_context.Publicaciones, "PublicacionId", "PublicacionId");
            ViewData["EstanteId"] = new SelectList(_context.Estantes, "EstanteId", "EstanteId");

            return View(ejemplar);
        }

        // GET: EjemplaresController/Edit/5
        // GET: EjemplaresController/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ejemplar = await _context.Ejemplares
                .Include(e => e.Publicacion) // Incluye Publicacion
                .Include(e => e.Estante)
                .FirstOrDefaultAsync(m => m.EjemplarId == id);

            if (ejemplar == null)
            {
                return NotFound();
            }

            // Cargamos las publicaciones con el título/nombre para el SelectList
            var publicaciones = await _context.Publicaciones
                .Include(p => p.Libros)
                .Include(p => p.Revistas)
                .ToListAsync();

            var publicacionList = publicaciones.Select(p => new
            {
                PublicacionId = p.PublicacionId,
                Texto = p.Libros.Any()
                ? $"{p.Libros.First().Titulo} (Libro)"
                    : p.Revistas.Any()
                ? $"{p.Revistas.First().Nombre} (Revista)"
                    : "Desconocido"
            }).ToList();


            ViewData["PublicacionId"] = new SelectList(publicacionList, "PublicacionId", "Texto", ejemplar.PublicacionId);
            ViewData["EstanteId"] = new SelectList(_context.Estantes, "EstanteId", "CodigoEstante", ejemplar.EstanteId);

            return View(ejemplar);
        }

        // POST: EjemplaresController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EjemplarId,PublicacionId,EstanteId")] Ejemplar ejemplar)
        {
            if (id != ejemplar.EjemplarId)
            {
                return NotFound();
            }

            //if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ejemplar);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EjemplarExists(ejemplar.EjemplarId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            var publicaciones = await _context.Publicaciones
                .Include(p => p.Libros)
                .Include(p => p.Revistas)
                .ToListAsync();

            var publicacionList = publicaciones.Select(p => new
            {
                PublicacionId = p.PublicacionId,
                Texto = p.Libros.Any()
                    ? $"{p.Libros.First().Titulo} (Libro)"
                        : p.Revistas.Any()
                    ? $"{p.Revistas.First().Nombre} (Revista)"
                        : "Desconocido"
            }).ToList();


            ViewData["PublicacionId"] = new SelectList(publicacionList, "PublicacionId", "Texto", ejemplar.PublicacionId);
            ViewData["EstanteId"] = new SelectList(_context.Estantes, "EstanteId", "CodigoEstante", ejemplar.EstanteId);

            return View(ejemplar);
        }



        // GET: EjemplaresController/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ejemplar = await _context.Ejemplares
                .FirstOrDefaultAsync(m => m.EjemplarId == id);
            if (ejemplar == null)
            {
                return NotFound();
            }

            return View(ejemplar);
        }

        // POST: EjemplaresController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ejemplar = await _context.Ejemplares.FindAsync(id);
            if (ejemplar != null)
            {
                _context.Ejemplares.Remove(ejemplar);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EjemplarExists(int id)
        {
            return _context.Ejemplares.Any(e => e.EjemplarId == id);
        }
    }
}
