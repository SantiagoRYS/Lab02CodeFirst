namespace BibliotecaWebApplication.Models
{
    public class Estante
    {
        public int EstanteId { get; set; }
        public int CodigoEstante { get; set; }
        public int EstanteriaId { get; set; }
        public Estanteria Estanteria { get; set; }

        public ICollection<Ejemplar> Ejemplares { get; set; } = new List<Ejemplar>();
    }

}
