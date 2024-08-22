using BibliotecaWebApplication.Data;
using BibliotecaWebApplication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaWebApplication.Controllers
{
    public class RevistasController : Controller
    {

        private readonly ApplicationDbContext _context;

        public RevistasController(ApplicationDbContext context)
        {
            _context = context;
        }


        // GET: RevistasController
        [Authorize(Roles = "Bibliotecario, Administrador")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Revistas.ToListAsync());
        }

        // GET: RevistasController/Details/5
        [Authorize(Roles = "Bibliotecario, Administrador")]
        public async Task<IActionResult> Details(Guid? id)
        {

            if (id == null)
            {
                return NotFound();
            }


            var revista = await _context.Revistas
        .FirstOrDefaultAsync(m => m.RevistaId == id);

            if (revista == null)
            {
                return NotFound();
            }
            return View(revista);
        }

        // GET: RevistasController/Create
        [Authorize(Roles = "Administrador")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: RevistasController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Create([Bind("RevistaId,Numero,Nombre,FechaPublicacion,PublicacionId")] Revista revista)
        {

            //if (ModelState.IsValid)
            {

                var publicacion = new Publicacion();
                _context.Publicaciones.Add(publicacion);
                await _context.SaveChangesAsync();

                revista.PublicacionId = publicacion.PublicacionId;

                revista.RevistaId = Guid.NewGuid();
                _context.Add(revista);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));

            }
            return View(revista);
        }



        // GET: RevistasController/Edit/5
        [Authorize(Roles = "Bibliotecario, Administrador")]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var revista = await _context.Revistas
                .Include(l => l.Publicacion)
                .FirstOrDefaultAsync(m => m.RevistaId == id);
            if (revista == null)
            {
                return NotFound();
            }
            return View(revista);
        }
    

        // POST: RevistasController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Bibliotecario, Administrador")]
        public async Task<IActionResult> Edit(Guid? id, [Bind("RevistaId,Numero,Nombre,FechaPublicacion,PublicacionId")] Revista revista)
        {
            if (id != revista.RevistaId)
            {
                return NotFound();
            }
            //if (ModelState.IsValid)
            {
                try
                {
                    var revistaDb = await _context.Revistas
                        .Include(l => l.Publicacion)
                        .FirstOrDefaultAsync(l => l.RevistaId == id);

                    if (revistaDb == null)
                    {
                        return NotFound();
                    }
                    revistaDb.Numero = revista.Numero;
                    revistaDb.Nombre = revista.Nombre;
                    revistaDb.FechaPublicacion = revista.FechaPublicacion;

                    _context.Update(revistaDb);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RevistaExists(revista.RevistaId))
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
            return View(revista);
        }



        // GET: RevistasController/Delete/5
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var revista = await _context.Revistas.FirstOrDefaultAsync(m => m.RevistaId == id);
            if (revista == null)
            {
                return NotFound();
            }

            return View(revista);
        }

        // POST: RevistasController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var revista = await _context.Revistas.FindAsync(id);
            if (revista != null)
            {
                _context.Revistas.Remove(revista);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Bibliotechario, Administrador")]
        private bool RevistaExists(Guid id)
        {
            return _context.Revistas.Any(e => e.RevistaId == id);
        }

    }
}