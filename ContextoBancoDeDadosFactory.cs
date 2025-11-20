using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using ReservaCinema.Models;

namespace ReservaCinema
{
    public class ContextoBancoDeDadosFactory : IDesignTimeDbContextFactory<ContextoBancoDeDados>
    {
        public ContextoBancoDeDados CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<ContextoBancoDeDados>();
            optionsBuilder.UseSqlServer(connectionString);

            return new ContextoBancoDeDados(optionsBuilder.Options);
        }
    }
}
