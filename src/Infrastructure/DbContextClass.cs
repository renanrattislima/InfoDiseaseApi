namespace Infrastructure
{
    using Domain.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

    public class DbContextClass : DbContext
    {
        public DbContextClass()
        {
        }

        public DbContextClass(DbContextOptions<DbContextClass> contextOptions) : base(contextOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Definir chaves primárias
            modelBuilder.Entity<Relatorio>()
                .HasKey(r => r.Id);

            modelBuilder.Entity<Solicitante>()
                .HasKey(s => s.Id);

            modelBuilder.Entity<DadoEpidemiologico>()
                .HasKey(d => d.Id);

            modelBuilder.Entity<Solicitante>()
                .HasMany(s => s.Relatorios)
                .WithOne(r => r.Solicitante) 
                .HasForeignKey(r => r.SolicitanteId) 
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Relatorio>()
                .HasMany(r => r.DadoEpidemiologicos)
                .WithOne(d => d.Relatorio) 
                .HasForeignKey(d => d.RelatorioId) 
                .OnDelete(DeleteBehavior.Cascade);
        }

        // DbSets para cada entidade
        public DbSet<Solicitante> Solicitantes { get; set; }

        public DbSet<Relatorio> Relatorios { get; set; }

        public DbSet<DadoEpidemiologico> DadoEpidemiologicos { get; set; }
    }
}
