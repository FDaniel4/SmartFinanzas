using SmartFinanzas.Modelos;

namespace SmartFinanzas
{
    public class GestorMovimientos
    {
        private List<Movimiento> movimientos = [];
        public void Agregar(Movimiento movimiento)
        {
            if(movimiento is not null)
            {
                movimientos.Add(movimiento);
            }

        }
        public List<Movimiento> ObtenerTodos()
        {
            return movimientos;
        }

        public decimal TotalIngresos()
        {
            return movimientos.Where(m => m.Tipo == TipoMovimiento.Ingreso).Sum(m => m.Monto);
        }
        public decimal TotalGastos()
        {
            return movimientos.Where(m => m.Tipo == TipoMovimiento.Gasto).Sum(m => m.Monto);
        }

        public decimal Saldo()
        {
            return TotalIngresos() - TotalGastos();
        }

    }
}
