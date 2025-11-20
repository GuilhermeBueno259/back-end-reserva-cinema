namespace ReservaCinema.Models
{
    public class Sessao
    {
        public int Id { get; set; }
        public string Filme { get; set; }
        public int Sala { get; set; }
        public DateTime DataHora { get; set; }
        public string SinopseFilme { get; set; }
        public string DiretoresFilme {  get; set; }
        public int QuantidadeTotalAssentos { get; set; }
        public ICollection<Assento> Assentos { get; set; }
    }
}
