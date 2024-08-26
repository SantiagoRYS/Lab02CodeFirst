using BibliotecaWebApplication.Data;
using BibliotecaWebApplication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaWebApplication.Controllers
{
    public class LibrosController : Controller
    {

        private readonly ApplicationDbContext _context;

        public LibrosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: LibrosController
        [Authorize(Roles = "Bibliotecario, Administrador")]
        public async Task<IActionResult> Index()
        {
            var libros = await _context.Libros
                .Include(l => l.AutorLibros)
                    .ThenInclude(al => al.Autor)
                .ToListAsync();
            return View(libros);
        }

        // GET: LibrosController/Details/5
        [Authorize(Roles = "Bibliotecario, Administrador")]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var libro = await _context.Libros
                .Include(l => l.AutorLibros) // Incluye los autores asociados al libro
                .ThenInclude(al => al.Autor) // Incluye los detalles del autor
                .FirstOrDefaultAsync(m => m.LibroId == id);


            if (libro == null)
            {
                return NotFound();
            }

            return View(libro);
        }

        // GET: LibrosController/Create
        [Authorize(Roles = "Administrador")]
        public IActionResult Create()
        {

            ViewBag.Autores = _context.Autores.ToList();

            return View();
        }

        // POST: LibrosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Create([Bind("LibroId,ISBN,Titulo,NumeroPaginas,Formato,PublicacionId,Portada,Contraportada")] Libro libro, Guid[] selectedAutores, IFormFile portada, IFormFile contraportada)
        {
            //if (ModelState.IsValid)
            {
                    
                var publicacion = new Publicacion();
                _context.Publicaciones.Add(publicacion);
                await _context.SaveChangesAsync();

                libro.PublicacionId = publicacion.PublicacionId;
                libro.LibroId = Guid.NewGuid();

                if (portada != null)
                {
                    string portadaPath = Path.Combine("wwwroot\\Imagenes\\Libros", $"{libro.LibroId}_portada{Path.GetExtension(portada.FileName)}");
                    using (var stream = new FileStream(portadaPath, FileMode.Create))
                    {
                        await portada.CopyToAsync(stream);
                    }
                    libro.Portada = $"/Imagenes/Libros/{libro.LibroId}_portada{Path.GetExtension(portada.FileName)}";
                }

                if (contraportada != null)
                {
                    string contraportadaPath = Path.Combine("wwwroot\\Imagenes\\Libros", $"{libro.LibroId}_contraportada{Path.GetExtension(contraportada.FileName)}");
                    using (var stream = new FileStream(contraportadaPath, FileMode.Create))
                    {
                        await contraportada.CopyToAsync(stream);
                    }
                    libro.Contraportada = $"/Imagenes/Libros/{libro.LibroId}_contraportada{Path.GetExtension(contraportada.FileName)}";
                }

                _context.Add(libro);
                await _context.SaveChangesAsync();

                if (selectedAutores != null)
                {
                    foreach (var autorId in selectedAutores)
                    {
                        var autorLibro = new AutorLibro
                        {
                            AutorId = autorId,
                            LibroId = libro.LibroId
                        };
                        _context.AutorLibros.Add(autorLibro);
                    }
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction(nameof(Index));
            }

            ViewBag.Autores = _context.Autores.ToList();

            return View(libro);
        }


        // GET: LibrosController/Edit/5
        [Authorize(Roles = "Bibliotecario, Administrador")]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var libro = await _context.Libros
                .Include(l => l.Publicacion)
                .Include(l => l.AutorLibros)
                    .ThenInclude(al => al.Autor)
                .FirstOrDefaultAsync(m => m.LibroId == id);
            if (libro == null)
            {
                return NotFound();
            }

            ViewBag.Autores = _context.Autores.ToList();
            ViewBag.SelectedAutores = libro.AutorLibros.Select(al => al.AutorId).ToList() ?? new List<Guid>();

            return View(libro);
        }


        // POST: LibrosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Bibliotecario, Administrador")]
        public async Task<IActionResult> Edit(Guid? id, [Bind("LibroId,ISBN,Titulo,NumeroPaginas,Formato,PublicacionId,Portada,Contraportada")] Libro libro, Guid[] selectedAutores, IFormFile portada, IFormFile contraportada)
        {
            if (id != libro.LibroId)
            {
                return NotFound();
            }

            //if (ModelState.IsValid)
            {
                try
                {
                    var libroDb = await _context.Libros
                        .Include(l => l.Publicacion)
                        .FirstOrDefaultAsync(l => l.LibroId == id);

                    if (libroDb == null)
                    {
                        return NotFound();
                    }

                    libroDb.ISBN = libro.ISBN;
                    libroDb.Titulo = libro.Titulo;
                    libroDb.NumeroPaginas = libro.NumeroPaginas;
                    libroDb.Formato = libro.Formato;


                    if (portada != null)
                    {
                        string portadaPath = Path.Combine("wwwroot\\Imagenes\\Libros", $"{libroDb.LibroId}_portada{Path.GetExtension(portada.FileName)}");
                        using (var stream = new FileStream(portadaPath, FileMode.Create))
                        {
                            await portada.CopyToAsync(stream);
                        }
                        libroDb.Portada = $"/Imagenes/Libros/{libroDb.LibroId}_portada{Path.GetExtension(portada.FileName)}";
                    }

                    if (contraportada != null)
                    {
                        string contraportadaPath = Path.Combine("wwwroot\\Imagenes\\Libros", $"{libroDb.LibroId}_contraportada{Path.GetExtension(contraportada.FileName)}");
                        using (var stream = new FileStream(contraportadaPath, FileMode.Create))
                        {
                            await contraportada.CopyToAsync(stream);
                        }
                        libroDb.Contraportada = $"/Imagenes/Libros/{libroDb.LibroId}_contraportada{Path.GetExtension(contraportada.FileName)}";
                    }


                    _context.Update(libroDb);
                    await _context.SaveChangesAsync();

                    var existingAutorLibros = _context.AutorLibros.Where(al => al.LibroId == id);
                    _context.AutorLibros.RemoveRange(existingAutorLibros);

                    if (selectedAutores != null && selectedAutores.Any())
                    {
                        foreach (var autorId in selectedAutores)
                        {
                            var autorLibro = new AutorLibro
                            {
                                AutorId = autorId,
                                LibroId = libro.LibroId
                            };
                            _context.AutorLibros.Add(autorLibro);
                        }
                        await _context.SaveChangesAsync();
                    }
                }


                catch (DbUpdateConcurrencyException)
                {
                    if (!LibroExists(libro.LibroId))
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

            ViewBag.Autores = _context.Autores.ToList();
            ViewBag.SelectedAutores = selectedAutores != null ? selectedAutores.ToList() : new List<Guid>();
            return View(libro);
        }

        // GET: LibrosController/Delete/5
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var libro = await _context.Libros
                .Include(l => l.AutorLibros)
                    .ThenInclude(al => al.Autor)
                .FirstOrDefaultAsync(m => m.LibroId == id);
            if (libro == null)
            {
                return NotFound();
            }

            return View(libro);
        }


        // POST: LibrosController/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var libro = await _context.Libros
                .Include(l => l.AutorLibros)
                .Include(l => l.Publicacion)
                .FirstOrDefaultAsync(l => l.LibroId == id);

            if (libro != null)
            {

                if (!string.IsNullOrEmpty(libro.Portada))
                {
                    string portadaPath = Path.Combine("wwwroot", libro.Portada.TrimStart('/'));
                    if (System.IO.File.Exists(portadaPath))
                    {
                        System.IO.File.Delete(portadaPath);
                    }
                }

                if (!string.IsNullOrEmpty(libro.Contraportada))
                {
                    string contraportadaPath = Path.Combine("wwwroot", libro.Contraportada.TrimStart('/'));
                    if (System.IO.File.Exists(contraportadaPath))
                    {
                        System.IO.File.Delete(contraportadaPath);
                    }
                }


                //eliminamos la relacion de la clase AutorLibro
                _context.AutorLibros.RemoveRange(libro.AutorLibros);

                if (libro.Publicacion != null)
                {
                    //eliminamos la Publicacion asiciada al libro
                    _context.Publicaciones.Remove(libro.Publicacion);
                }

                //eliminamos el libro como tal
                _context.Libros.Remove(libro);
                await _context.SaveChangesAsync();

            }
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Bibliotecario, Administrador")]
        private bool LibroExists(Guid id)
        {
            return _context.Libros.Any(e => e.LibroId == id);
        }

    }
}
