namespace BibliotecaWebApplication.Models
{
    public class Autor
    {

        public Guid AutorId { get; set; }

        public string Nombres { get; set; }

        public string Apellidos { get; set; }

        public string Nacionalidad { get; set; }


        public string? Imagen { get; set; } // Propiedad para almacenar la ruta de la imagen


        public Autor()
        {
            this.AutorId = Guid.NewGuid();
        }

        // Propiedades de Navegación
        public ICollection<AutorLibro> AutorLibros { get; set; } = new List<AutorLibro>();

    }
}
