using Microsoft.EntityFrameworkCore;

namespace ListadoAutos.API.Modelos
{
    public class ListadoAutosDbContext : DbContext
    {
        public ListadoAutosDbContext(DbContextOptions<ListadoAutosDbContext> opciones) : base(opciones)
        {

        }

        public DbSet<Auto> Autos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Auto>().HasData(
                new List<Auto>
                {
                    new Auto{ Id = 1, Marca = "Honda", Modelo = "Civic", Placa  = "ABC1234"},
                    new Auto{ Id = 2, Marca = "Toyota", Modelo = "Prado", Placa  = "ABC1234"},
                    new Auto{ Id = 3, Marca = "Audi", Modelo = "A5", Placa  = "ABC1234"},
                    new Auto{ Id = 4, Marca = "Chevrolet", Modelo = "Spark", Placa  = "ABC1234"},
                    new Auto{ Id = 5, Marca = "BMW", Modelo = "M3", Placa  = "ABC1234"},
                    new Auto{ Id = 6, Marca = "Nissan", Modelo = "Note", Placa  = "ABC1234"}
                });
        }
    }
}