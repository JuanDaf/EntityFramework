namespace ApiWebExamenEf.Models
{
    public class Persona
    {

        public string PersonaId { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Email { get; set; }
        public string Direccion { get; set;}
        public Estrato NivelEstrato { get; set; }
        
        public virtual ICollection<DetallePaS> DetallePaSs { get; set; }
    }
    public enum Estrato
    {
        Bajo, Medio, Alto
    }
}
