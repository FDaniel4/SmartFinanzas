using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartFinanzas.Modelos
{
    public class Meta
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre de la meta es obligatoria")]
        public string Nombre { get; set; } = string.Empty;
        public decimal MontoObjetivo { get; set; }
        public decimal MontoAhorrado { get; set; }
        public DateTime FechaEstimada { get; set; }

        // Propiedad calculada para el progreso
        public double Progreso => MontoObjetivo == 0 ? 0 : (double)(MontoAhorrado / MontoObjetivo) * 100;
    }
}
