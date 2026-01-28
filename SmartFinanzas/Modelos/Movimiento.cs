using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartFinanzas.Modelos
{
    public class Movimiento
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Concepto { get; set; } = string.Empty;
        [Required]
        public decimal Monto { get; set; }
        //public DateTime Fecha { get; set; }

        //Enum: Ingreso gasto
        public TipoMovimiento Tipo { get; set; }
    }

    public enum TipoMovimiento { Ingreso, Gasto } 
}
