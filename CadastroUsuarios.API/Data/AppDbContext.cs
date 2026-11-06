
using CadastroUsuarios.API.Models;
using Microsoft.EntityFrameworkCore;

namespace CadastroUsuarios.API.Data;

public class AppDbContext : DbContext {
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) {
    }

    public DbSet<Usuario> Usuarios => Set<Usuario>();

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.Entity<Usuario>(entity => {
            entity.ToTable("usuarios", "public");

            entity.HasKey(u => u.Id);

            entity.Property(u => u.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            entity.Property(u => u.Nome)
                .HasColumnName("nome");

            entity.Property(u => u.Email)
                .HasColumnName("email");

            entity.Property(u => u.Senha)
                .HasColumnName("senha");
        });
    }
}
