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
    public class EstantesController : Controller
    {

        private readonly ApplicationDbContext _context;

        public EstantesController(ApplicationDbContext context)
        {
            _context = context;
        }


        // GET: EstantesController
        public async Task<IActionResult> Index()
        {
            var estantes = _context.Estantes.Include(e => e.Estanteria);
            return View(await estantes.ToListAsync());
        }

        // GET: EstantesController/Details/5
        public async Task<IActionResult> Details(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }


            var estante = await _context.Estantes
                .Include(e => e.Estanteria)
                .FirstOrDefaultAsync(m => m.EstanteId == id);

            if (estante == null)
            {
                return NotFound();
            }
            return View(estante);
        }

        // GET: EstantesController/Create
        public ActionResult Create()
        {
            ViewData["EstanteriaId"] = new SelectList(_context.Estanterias, "EstanteriaId", "EstanteriaId");

            return View();
        }

        // POST: EstantesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EstanteId,CodigoEstante,EstanteriaId")] Estante estante)
        {
            //if (ModelState.IsValid)
            {
                _context.Add(estante);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            ViewData["EstanteriaId"] = new SelectList(_context.Estanterias, "EstanteriaId", "EstanteriaId", estante.EstanteriaId);
            return View(estante);
        }

        // GET: EstantesController/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estante = await _context.Estantes.FindAsync(id);
            if (estante == null)
            {
                return NotFound();
            }

            ViewData["EstanteriaId"] = new SelectList(_context.Estanterias, "EstanteriaId", "EstanteriaId", estante.EstanteriaId);

            return View(estante);
        }

        // POST: EstantesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EstanteId,CodigoEstante,EstanteriaId")] Estante estante)
        {
            if (id != estante.EstanteId)
            {
                return NotFound();
            }

            //if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(estante);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EstanteExists(estante.EstanteId))
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

            ViewData["EstanteriaId"] = new SelectList(_context.Estanterias, "EstanteriaId", "EstanteriaId", estante.EstanteriaId);
            
            return View(estante);
        }

        // GET: EstantesController/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estante = await _context.Estantes
                .FirstOrDefaultAsync(m => m.EstanteId == id);
            if (estante == null)
            {
                return NotFound();
            }

            return View(estante);
        }

        // POST: EstantesController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var estante = await _context.Estantes.FindAsync(id);
            if (estante != null)
            {
                _context.Estantes.Remove(estante);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EstanteExists(int id)
        {
            return _context.Estantes.Any(e => e.EstanteId == id);
        }
    }
}
