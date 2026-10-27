using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CRUD_EFCore
{
    public class HelpDeskContext : DbContext
    {
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Tecnico> Tecnicos { get; set; }
        public DbSet<Chamado> Chamados { get; set; }
        public DbSet<Equipamento> Equipamentos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfiguration configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                    .Build();

                string? connectionString = configuration.GetConnectionString("DefaultConnection");

                if (string.IsNullOrEmpty(connectionString))
                {
                    connectionString = Environment.GetEnvironmentVariable("HELPDESK_CONNECTION");
                }

                if (string.IsNullOrEmpty(connectionString))
                {
                    throw new Exception("String de conexão não encontrada. Configure o arquivo appsettings.json ou a variável de ambiente HELPDESK_CONNECTION.");
                }

                optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            }
        }
    }
}
