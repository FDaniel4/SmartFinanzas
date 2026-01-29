using Microsoft.EntityFrameworkCore;
using SmartFinanzas.Modelos;

namespace SmartFinanzas.Data
{
    public class FinanzasDbContext : DbContext
    {
        public FinanzasDbContext(DbContextOptions<FinanzasDbContext> options)
            : base(options)
        {

        }
        public DbSet<Movimiento> Movimientos { get; set; }
        public DbSet<Meta> Metas { get; set; }
    }
}
