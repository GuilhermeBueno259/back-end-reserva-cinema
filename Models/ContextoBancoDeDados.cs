using Microsoft.EntityFrameworkCore;

namespace ReservaCinema.Models
{
    public class ContextoBancoDeDados : DbContext
    {
        public ContextoBancoDeDados(DbContextOptions<ContextoBancoDeDados> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Sessao>()
                .HasMany(p => p.Assentos)
                .WithOne(c => c.Sessao)
                .HasForeignKey(c => c.IdSessao)
                .OnDelete(DeleteBehavior.Cascade);
        }

        public DbSet<Sessao> Sessoes { get; set; } = null!;
        public DbSet<Assento> Assentos { get; set; } = null!;
    }
}
