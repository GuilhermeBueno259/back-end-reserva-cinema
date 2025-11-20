namespace ReservaCinema.Models
{
    public class AssentoDTO
    {
        public int Id { get; set; }
        public int IdSessao { get; set; }
        public int NumeroAssento { get; set; }
        public bool Reservado { get; set; }

        public AssentoDTO(Assento assento)
        {
            Id = assento.Id;
            IdSessao = assento.IdSessao;
            NumeroAssento = assento.NumeroAssento;
            Reservado = assento.Reservado;
        }
    }
}
