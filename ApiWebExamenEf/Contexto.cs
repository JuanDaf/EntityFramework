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
                List<Persona> personasInit = new List<Persona>();

                personasInit.Add( new Persona() { PersonaId = "123456789", Nombres = "Juan David", Apellidos ="Forero", Email = "iotled@yopmil.com", Direccion = "Fúquene", NivelEstrato = Estrato.Medio});
                personasInit.Add(new Persona() { PersonaId = "865292873", Nombres = "Marco", Apellidos = "Martonez", Email = "Martinez@yopmil.com", Direccion = "Fúquene", NivelEstrato = Estrato.Alto });
                personasInit.Add(new Persona() { PersonaId = "642902773", Nombres = "Alonso", Apellidos = "Romero", Email = "Romero@yopmil.com", Direccion = "Fúquene", NivelEstrato = Estrato.Alto });

                Persona.ToTable("Persona");
                Persona.HasKey(p => p.PersonaId);

                Persona.Property(p => p.Nombres).IsRequired();
                Persona.Property(p => p.Apellidos).IsRequired();
                Persona.Property(p => p.Email).IsRequired();
                Persona.Property(p => p.Direccion);
                Persona.Property(p => p.NivelEstrato);

                Persona.HasData(personasInit);

            });


            modelBuilder.Entity<Servicio>(Servicio =>
            {
                List<Servicio> serviciosInit = new List<Servicio>();
                serviciosInit.Add(new Servicio() { ServicioId = Guid.NewGuid(), NommbreServicio = "Televison por cable",FechaCreacion = DateTime.Now, DescripcionServicio = "Sin descripcion"});
                serviciosInit.Add(new Servicio() { ServicioId = Guid.NewGuid(), NommbreServicio = "Telelfonia Claro", FechaCreacion = DateTime.Now, DescripcionServicio = "Sin descripcion" });
               
                Servicio.ToTable("Servicio");
                Servicio.HasKey(s => s.ServicioId);

                Servicio.Property(s => s.NommbreServicio).IsRequired().HasMaxLength(100);
                Servicio.Property(s => s.FechaCreacion).IsRequired();
                Servicio.Property(s => s.DescripcionServicio);

                Servicio.HasData(serviciosInit);
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
