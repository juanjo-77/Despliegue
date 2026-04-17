using System.ComponentModel.DataAnnotations;

namespace Despliegue.Models
{
    public class Event
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(150)]
        [Display(Name = "Nombre del evento")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "La fecha es obligatoria")]
        [Display(Name = "Fecha")]
        [DataType(DataType.Date)]
        public DateTime Fecha { get; set; }

        [Required(ErrorMessage = "La descripción es obligatoria")]
        [StringLength(1000)]
        [Display(Name = "Descripción")]
        public string Descripcion { get; set; } = string.Empty;

        [Required(ErrorMessage = "La ubicación es obligatoria")]
        [StringLength(200)]
        [Display(Name = "Ubicación")]
        public string Ubicacion { get; set; } = string.Empty;

        [Display(Name = "Categoría")]
        public string Categoria { get; set; } = "General";

        [Display(Name = "URL del póster")]
        public string ImagenUrl { get; set; } = string.Empty;
    }
}