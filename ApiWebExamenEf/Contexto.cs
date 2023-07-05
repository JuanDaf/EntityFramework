using ApiWebExamenEf.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiWebExamenEf
{
    public class Contexto : DbContext
    {
        public DbSet<Persona> personas { get; set; }
        public DbSet<Servicio> servicios { get; set; }
        public DbSet<DetallePaS> detallePaS { get; set; }

        public Contexto(DbContextOptions<Contexto> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Persona>(Persona =>
            {
                Persona.ToTable("Persona");
                Persona.HasKey(p => p.PersonaId);

                Persona.Property(p => p.Nombres).IsRequired();
                Persona.Property(p => p.Apellidos).IsRequired();
                Persona.Property(p => p.Email).IsRequired();
                Persona.Property(p => p.Direccion);
                Persona.Property(p => p.NivelEstrato);

            });


            modelBuilder.Entity<Servicio>(Servicio =>
            {
                Servicio.ToTable("Servicio");
                Servicio.HasKey(s => s.ServicioId);

                Servicio.Property(s => s.NommbreServicio).IsRequired().HasMaxLength(100);
                Servicio.Property(s => s.FechaCreacion).IsRequired();
                Servicio.Property(s => s.DescripcionServicio);
            });

            modelBuilder.Entity<DetallePaS>(DetallePaS =>
            {
                DetallePaS.ToTable("DetallePaS");
                DetallePaS.HasKey(d => d.DetalleId);
                DetallePaS.HasOne(d => d.Persona).WithMany(d => d.DetallePaSs).HasForeignKey(d => d.PersonaId);
                DetallePaS.HasOne(d => d.Servicio).WithMany(d => d.DetallePaSs).HasForeignKey(d => d.ServicioId);

            });
        }
    }
}
