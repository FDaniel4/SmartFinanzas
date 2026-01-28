//using System.ComponentModel.DataAnnotations;

//namespace SmartFinanzas.Modelos
//{
//    public class Meta
//    {
//        [Key]
//        public int Id { get; set; }
//        public string Nombre { get; set; } = string.Empty;
//        public decimal MontoObjetivo { get; set; }
//        public decimal MontoAhorrado { get; set; }
//        public DateTime FechaEstimada { get; set; }

//        // Propiedad calculada para el progreso
//        public double Progreso => MontoObjetivo == 0 ? 0 : (double)(MontoAhorrado / MontoObjetivo) * 100;
//    }
//}
