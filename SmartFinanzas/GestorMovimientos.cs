using SmartFinanzas.Modelos;
using SmartFinanzas.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SmartFinanzas
{
    public class GestorMovimientos
    {
        private readonly FinanzasDbContext _context;

        public GestorMovimientos(FinanzasDbContext context)
        {
            _context = context;
        }
        public void Agregar(Movimiento movimiento)
        {
            if(movimiento is not null)
            {
                _context.Movimientos.Add(movimiento);
                _context.SaveChanges();
            }

        }
        public List<Movimiento> ObtenerTodos()
        {
            return _context.Movimientos.OrderByDescending(m => m.Fecha).ToList();
        }

        public decimal TotalIngresos()
        {
            return _context.Movimientos.Where(m => m.Tipo == TipoMovimiento.Ingreso).Sum(m => m.Monto);
        }
        public decimal TotalGastos()
        {
            return _context.Movimientos.Where(m => m.Tipo == TipoMovimiento.Gasto).Sum(m => m.Monto);
        }

        public decimal Saldo()
        {
            return TotalIngresos() - TotalGastos();
        }
        public void Eliminar(int id)
        {
            var eliminar = _context.Movimientos.Find(id);
            if(eliminar != null)
            {
                _context.Movimientos.Remove(eliminar);
                _context.SaveChanges();
            }
        }
        public void Editar(Movimiento movimiento)
        {
            if(movimiento is not null)
            {
                var existente = _context.Movimientos.Find(movimiento.Id);
                if(existente != null)
                {
                    existente.Concepto = movimiento.Concepto;
                    existente.Monto = movimiento.Monto;
                    existente.Tipo = movimiento.Tipo;

                    existente.Fecha = movimiento.Fecha;
                    existente.Categoria = movimiento.Categoria;
                    _context.SaveChanges();
                }
            }
        }
        //Gestion de metas

        public void AgregarMeta(Meta meta)
        {
            if(meta is not null)
            {
                _context.Metas.Add(meta);
                _context.SaveChanges();
            }
        }
        public List<Meta> ObtenerMetas()
        {
            return _context.Metas.ToList(); ;
        }
        public void EliminarMeta(int id)
        {
            var meta = _context.Metas.Find(id);
            if(meta != null)
            {
                _context.Metas.Remove(meta);
                _context.SaveChanges();
            }
        }
        public void EditarMeta(Meta metaEditada)
        {
            var existente = _context.Metas.Find(metaEditada.Id);
            if(existente != null)
            {
                existente.Nombre = metaEditada.Nombre;
                existente.MontoObjetivo = metaEditada.MontoObjetivo;
                existente.MontoAhorrado = metaEditada.MontoAhorrado;
                existente.FechaEstimada = metaEditada.FechaEstimada;
                _context.SaveChanges();
            }
        }
        //Analisis y presupuesto
        public Dictionary<string, decimal> ObtenerGastosPorCategoria()
        {
            return _context.Movimientos
                .Where(m => m.Tipo == TipoMovimiento.Gasto)
                .GroupBy(m => m.Categoria)
                .Select(g => new { Categoria = g.Key, Total = g.Sum(m => m.Monto) })
                .ToDictionary(k => k.Categoria.ToString(), v => v.Total);
        }
        public string ObtenerEstadoSalud()
        {
            decimal ingresos = TotalIngresos();
            decimal saldo = Saldo();

            // Evitar división por cero
            if (ingresos == 0) return "Sin datos suficientes";

            decimal porcentajeAhorro = (saldo / ingresos) * 100;

            if (porcentajeAhorro >= 25)
                return "Bueno (Ahorro saludable)";
            else if (porcentajeAhorro >= 15)
                return "Regular (Podría mejorar)";
            else
                return "Malo (Ahorro crítico)";
        }

        // Sugerencia de Inversión (Imagen 1 Punto 2)
        // Retorna un objeto con los montos sugeridos basados en el Saldo disponible
        public (decimal Inversion, decimal Gustos, decimal EndeudamientoPosible) ObtenerSugerenciasDistribucion()
        {
            decimal saldoActual = Saldo();

            // Si no hay saldo positivo, no podemos sugerir inversión
            if (saldoActual <= 0) return (0, 0, 0);

            // Reglas: 20% Inversión, 30% Gustos, Resto (50%) Endeudamiento/Otros
            decimal inversion = saldoActual * 0.20m;
            decimal gustos = saldoActual * 0.30m;
            decimal resto = saldoActual * 0.50m;

            return (inversion, gustos, resto);
        }

        private decimal _presupuestoMensual = 0; 

        public void EstablecerPresupuesto(decimal monto)
        {
            _presupuestoMensual = monto;
        }

        public decimal ObtenerPresupuesto()
        {
            return _presupuestoMensual;
        }

        //retorna true si hay alerta
        public bool VerificarAlertaPresupuesto()
        {
            if (_presupuestoMensual <= 0) return false; // Si no hay presupuesto, no hay alerta
            return TotalGastos() > _presupuestoMensual;
        }
    }
}
