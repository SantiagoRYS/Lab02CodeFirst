using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BibliotecaWebApplication.Data;
using BibliotecaWebApplication.Models;
using Microsoft.AspNetCore.Authorization;

namespace BibliotecaWebApplication.Controllers
{
    public class AutoresController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AutoresController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Autores
        [Authorize(Roles = "Bibliotecario, Administrador")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Autores.ToListAsync());
        }

        // GET: Autores/Details/5
        [Authorize(Roles = "Bibliotecario, Administrador")]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var autor = await _context.Autores
                .Include(a => a.AutorLibros) // Incluye los libros asociados al autor
                .ThenInclude(al => al.Libro) // Incluye los detalles de los libros
                .FirstOrDefaultAsync(a => a.AutorId == id);

            var img = autor.Imagen;
            if (autor == null)
            {
                return NotFound();
            }

            return View(autor);
        }

        // GET: Autores/Create
        [Authorize(Roles = "Administrador")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Autores/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Create([Bind("AutorId,Nombres,Apellidos,Nacionalidad,Imagen")] Autor autor, IFormFile ImagenAutor)
        {
            //if (ModelState.IsValid)
            {
                //agregar la imagen
                if (ImagenAutor != null )
                {
                    var fileName = Path.GetFileName(ImagenAutor.FileName);
                    var filePath = Path.Combine("wwwroot/Imagenes/Autores", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await ImagenAutor.CopyToAsync(stream);
                    }

                    autor.Imagen = "/Imagenes/Autores/" + fileName;
                }



                autor.AutorId = Guid.NewGuid();
                _context.Add(autor);
                await _context.SaveChangesAsync();

                if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                {
                    return Json(new { success = true, autor });
                }

                return RedirectToAction(nameof(Index));
            }
            return View(autor);
        }



        // GET: Autores/Edit/5
        [Authorize(Roles = "Bibliotecario, Administrador")]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var autor = await _context.Autores.FindAsync(id);
            if (autor == null)
            {
                return NotFound();
            }
            return View(autor);
        }

        // POST: Autores/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Bibliotecario, Administrador")]
        public async Task<IActionResult> Edit(Guid id, [Bind("AutorId,Nombres,Apellidos,Nacionalidad,Imagen")] Autor autor, IFormFile ImagenAutor)
        {
            if (id != autor.AutorId)
            {
                return NotFound();
            }

            //if (ModelState.IsValid)
            {
                try
                {
                    if (ImagenAutor != null)
                    {
                        var fileName = Path.GetFileName(ImagenAutor.FileName);
                        var filePath = Path.Combine("wwwroot/Imagenes/Autores", fileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await ImagenAutor.CopyToAsync(stream);
                        }

                        autor.Imagen = "/Imagenes/Autores/" + fileName;  // Almacena la nueva URL de la foto
                    }

                    _context.Update(autor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AutorExists(autor.AutorId))
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
            return View(autor);
        }

        // GET: Autores/Delete/5
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var autor = await _context.Autores
                .FirstOrDefaultAsync(m => m.AutorId == id);
            if (autor == null)
            {
                return NotFound();
            }

            return View(autor);
        }

        // POST: Autores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var autor = await _context.Autores.FindAsync(id);
            if (autor != null)
            {
                if (autor != null)
                {
                    if (!string.IsNullOrEmpty(autor.Imagen))
                    {
                        var imagePath = Path.Combine("wwwroot", autor.Imagen);
                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }
                    }
                    _context.Autores.Remove(autor);
                }

                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Bibliotecario, Administrador")]
        private bool AutorExists(Guid id)
        {
            return _context.Autores.Any(e => e.AutorId == id);
        }
    }
}