using System.Data.Entity;
using System.Diagnostics;

namespace WebFORNECEDOR.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base("name=MediFormfornecedor")
        {
            // Habilitar o logging de SQL para depuração
            this.Database.Log = s => Debug.WriteLine(s);
        }

        public DbSet<Fornecedor> Fornecedor { get; set; }
        public DbSet<Medicamento> Medicamentos { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Mapeamento explícito para a tabela "Pedidoes"
            modelBuilder.Entity<Pedido>().ToTable("Pedidos");

            // Configurar relacionamento entre Pedido e Medicamento
            modelBuilder.Entity<Pedido>()
                .HasRequired(p => p.Medicamento)
                .WithMany(m => m.Pedidos)
                .HasForeignKey(p => p.MedicamentoId)
                .WillCascadeOnDelete(false);

            // Configurar relacionamento entre Pedido e Fornecedor
            modelBuilder.Entity<Pedido>()
                .HasRequired(p => p.Fornecedor)
                .WithMany(f => f.Pedidos)
                .HasForeignKey(p => p.FornecedorId)
                .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }
    }
}
