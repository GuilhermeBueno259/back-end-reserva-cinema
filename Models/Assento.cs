namespace ReservaCinema.Models
{
    public class Assento
    {
        public int Id { get; set; }
        public int IdSessao { get; set; }
        public int NumeroAssento { get; set; }
        public bool Reservado { get; set; }
        public Sessao Sessao { get; set; }
    }
}
