using Microsoft.EntityFrameworkCore;
using Servidor.Models;

namespace Servidor.DAL
{
    public class Contexto : DbContext
    {
        public DbSet<Reservacion> Citas { get; set; }
        public DbSet<Mesa> Mesa { get; set; }
        public DbSet<Persona> Persona { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source=Data\Reservacion.db");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Mesa>()
               .HasData(new Mesa()
               {
                   IdMesa = 1,
                   Capacidad = 4,
                   Forma = "Redonda",
                   Precio = 1500,
                   Disponibilidad = true

               },

               new Mesa()
               {
                   IdMesa = 2,
                   Capacidad = 6,
                   Forma = "Rectangular",
                   Precio = 2500,
                   Disponibilidad = true

               }
               );

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Persona>()
               .HasData(new Persona()
               {
                   idPersona = 1,
                   Nombre = "Vismar",
                   Direccion = "San Francisco",
                   Telefono = "829-219-6048",
               },
            new Persona()
            {
                idPersona = 2,
                Nombre = "Gregory",
                Direccion = "San Francisco",
                Telefono = "829-555-0707",
            }
            );
        }
    }
}
