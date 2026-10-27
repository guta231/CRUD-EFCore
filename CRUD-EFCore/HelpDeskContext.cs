using Microsoft.EntityFrameworkCore;

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
                string connectionString = "server=localhost;database=HelpDeskDb;user=root;password=@Guta231;";
                optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            }
        }
    }
}
