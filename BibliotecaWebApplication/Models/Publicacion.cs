namespace BibliotecaWebApplication.Models
{
    public class Publicacion
    {

        public int PublicacionId { get; set; }

        public ICollection<Ejemplar> Ejemplares { get; set; } = new List<Ejemplar>();

        public ICollection<Libro> Libros { get; set; } = new List<Libro>();

        public ICollection<Revista> Revistas { get; set; } = new List<Revista>();


        // Propiedad combinada para mostrar en el SelectList
        public string TituloNombre
        {
            get
            {
                // Retornar el título del libro si existe, sino el nombre de la revista
                var libro = Libros.FirstOrDefault();
                if (libro != null)
                    return libro.Titulo;

                var revista = Revistas.FirstOrDefault();
                if (revista != null)
                    return revista.Nombre;

                return "Desconocido";
            }
        }

    }
}
