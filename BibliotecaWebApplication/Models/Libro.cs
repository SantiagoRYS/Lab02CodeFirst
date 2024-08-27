namespace BibliotecaWebApplication.Models
{
    public class Libro
    {
        public Guid LibroId { get; set; }
        public string ISBN { get; set; }
        public string Titulo { get; set; }
        public int NumeroPaginas { get; set; }
        public string Formato { get; set; }
        public string? Portada { get; set; }
        public string? Contraportada { get; set; }


        public int? PublicacionId { get; set; }
        public Publicacion Publicacion { get; set; }

        public Libro()
        {
            this.LibroId = Guid.NewGuid();
        }

        // Propiedades de Navegación
        public ICollection<AutorLibro> AutorLibros { get; set; } = new List<AutorLibro>();

    }

}
