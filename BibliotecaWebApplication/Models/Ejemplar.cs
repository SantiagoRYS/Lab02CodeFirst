using Mono.TextTemplating;

namespace BibliotecaWebApplication.Models
{
    public class Ejemplar
    {
        public int EjemplarId { get; set; }

        public int PublicacionId { get; set; }
        public Publicacion Publicacion { get; set; }

        public int EstanteId { get; set; }
        public Estante Estante { get; set; }

    }

}
