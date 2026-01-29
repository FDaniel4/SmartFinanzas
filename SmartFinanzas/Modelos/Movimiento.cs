using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartFinanzas.Modelos
{
    public class Movimiento
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "El concepto es obligatorio")]
        public string Concepto { get; set; } = string.Empty;

        [Range(0.01, double.MaxValue, ErrorMessage = "El monto debe ser mayor a 0")]
        public decimal Monto { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;

        //Enum: Ingreso gasto
        public TipoMovimiento Tipo { get; set; }

        //Enum: Categoria
        public Categoria Categoria { get; set; }
    }

    public enum TipoMovimiento { Ingreso, Gasto }

    public enum Categoria
    {
        Sueldo,
        Alimentacion,
        Transporte,
        Entretenimiento,
        Deudas,
        Salud,
        Otros,
        Ahorro 
    }
}
