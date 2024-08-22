namespace BibliotecaWebApplication.Models
{
    public class Estanteria
    {
        public int EstanteriaId { get; set; }
        public double Alto { get; set; }
        public double Ancho { get; set; }

        public ICollection<Estante> Estantes { get; set; } = new List<Estante>();
    }

}
