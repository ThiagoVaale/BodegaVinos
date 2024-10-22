using BodegaVinos.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace BodegaVinos.Data.Context
{
    //Clase que representara el contexto de la base de datos en la app y gestionara las entidades y el mapeo a la bd.
    public class WineDbContext : DbContext
    {
        //Aca se va a definir la tabla de vinos, users, cata dentro de la base de datos.
        public DbSet<WineEntity> Wines { get; set; }
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<CataEntity> Catas { get; set; }



        //Constructor que recibe las opciones desde el program.cs y los data sets de las entidades que queremos guardar en la base de datos.
        public WineDbContext(DbContextOptions<WineDbContext> options) : base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CataEntity>()
                .HasMany(c => c.Wines) // Una cata posee muchos vinos
                .WithOne(v => v.Catas); // Un vino pertenece a una cata
        }

    }
}
