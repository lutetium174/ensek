using Microsoft.EntityFrameworkCore;
using Persistence.Schemas;

namespace Persistence;

public class EnsekContext : DbContext
{
    public EnsekContext(DbContextOptions<EnsekContext> options) : base(options) { }

    public virtual required DbSet<AccountRecord> Accounts { get; set; }
    public virtual required DbSet<MeterReadingRecord> MeterReadings { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<AccountRecord>(entity => { });
        modelBuilder.Entity<MeterReadingRecord>(entity => { });
    }
}
