namespace ApiWebExamenEf.Models
{
    public class DetallePaS
    {

        public Guid  DetalleId { get; set; }

        public Guid ServicioId { get; set; }    

        public string PersonaId { get; set;}

        public virtual Persona Persona { get; set;}
        
        public virtual Servicio Servicio { get; set;}
    }
}
