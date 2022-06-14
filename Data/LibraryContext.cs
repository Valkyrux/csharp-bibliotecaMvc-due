using Microsoft.EntityFrameworkCore;
using Biblioteca_EntityFramework.Models;

namespace Biblioteca_EntityFramework
{
    public class LibraryContext : DbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options) { }

        public DbSet<Libro> Libri { get; set; }
        public DbSet<Autore> Autori { get; set; }
        public DbSet<Prestito> Prestiti { get; set; }
        public DbSet<Utente> Utenti { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Libro>().ToTable("libri");
            modelBuilder.Entity<Autore>().ToTable("autori").HasKey(c => new {c.Nome, c.Cognome, c.DataNascita});
            modelBuilder.Entity<Prestito>().ToTable("prestiti");
            modelBuilder.Entity<Utente>().ToTable("utenti");
        }
    }
}