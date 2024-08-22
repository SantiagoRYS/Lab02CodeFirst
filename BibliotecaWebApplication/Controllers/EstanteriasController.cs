using BibliotecaWebApplication.Data;
using BibliotecaWebApplication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaWebApplication.Controllers
{
    [Authorize(Roles = "Bibliotecario, Administrador")]
    public class EstanteriasController : Controller
    {

        private readonly ApplicationDbContext _context;

        public EstanteriasController(ApplicationDbContext context)
        {
            _context = context;
        }


        // GET: EstanteriasController
        public async Task<IActionResult> Index()
        {
            return View(await _context.Estanterias.ToListAsync());
        }

        // GET: EstanteriasController/Details/5
        public async Task<IActionResult> Details(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }


            var estanteria = await _context.Estanterias
                .FirstOrDefaultAsync(m => m.EstanteriaId == id);

            if (estanteria == null)
            {
                return NotFound();
            }
            return View(estanteria);
        }

        // GET: EstanteriasController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EstanteriasController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Alto,Ancho")] Estanteria estanteria)
        {
            if (ModelState.IsValid)
            {
                _context.Add(estanteria);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(estanteria);
        }


        // GET: EstanteriasController/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estanteria = await _context.Estanterias.FindAsync(id);
            if (estanteria == null)
            {
                return NotFound();
            }
            return View(estanteria);
        }

        // POST: EstanteriasController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EstanteriaId,Alto,Ancho")] Estanteria estanteria)
        {
            if (id != estanteria.EstanteriaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(estanteria);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EstanteriaExists(estanteria.EstanteriaId))
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
            return View(estanteria);
        }

        // GET: EstanteriasController/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estanteria = await _context.Estanterias
                .FirstOrDefaultAsync(m => m.EstanteriaId == id);
            if (estanteria == null)
            {
                return NotFound();
            }

            return View(estanteria);
        }

        // POST: EstanteriasController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var estanteria = await _context.Estanterias.FindAsync(id);
            if (estanteria != null)
            {
                _context.Estanterias.Remove(estanteria);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EstanteriaExists(int id)
        {
            return _context.Estanterias.Any(e => e.EstanteriaId == id);
        }

    }
}
