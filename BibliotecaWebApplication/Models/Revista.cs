using System.ComponentModel.DataAnnotations;

namespace BibliotecaWebApplication.Models
{
    public class Revista
    {
        public Guid RevistaId { get; set; }
        public string Numero { get; set; }
        public string Nombre { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaPublicacion { get; set; }

        public int PublicacionId { get; set; }
        public Publicacion Publicacion { get; set; }
    }

}
