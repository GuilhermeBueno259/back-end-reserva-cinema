namespace ReservaCinema.Models
{
    public class SessaoDTO
    {
        public int Id { get; set; }
        public string Filme { get; set; }
        public int Sala { get; set; }
        public string DataHora { get; set; }
        public string SinopseFilme { get; set; }
        public string DiretoresFilme { get; set; }
        public int QuantidadeTotalAssentos { get; set; }

        public SessaoDTO (Sessao sessao)
        {
            Id = sessao.Id;
            Filme = sessao.Filme;
            Sala = sessao.Sala;
            DataHora = sessao.DataHora.ToString("dd/MM/yyyy HH:mm:ss");
            SinopseFilme = sessao.SinopseFilme;
            DiretoresFilme = sessao.DiretoresFilme;
            QuantidadeTotalAssentos = sessao.QuantidadeTotalAssentos;
        }
    }
}
