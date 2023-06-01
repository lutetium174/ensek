using Domain;
using Domain.Accounts.Entities;
using Domain.MeterReadings.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Mappers;

namespace Persistence;

public class EnsekRepository : IEnsekRepository
{
    private readonly EnsekContext _context;
    public EnsekRepository(EnsekContext context)
        => _context = context;

    public async Task<Account?> GetAccountById(
        int accountId,
        CancellationToken cancellation)
    {
        var account = await _context.Accounts
            .Where(x => x.Id == accountId)
            .SingleOrDefaultAsync(cancellationToken: cancellation);

        return account?.ToDomain();
    }

    public async Task<MeterReading?> LatestReadingByAccountId(
            int accountId,
            CancellationToken cancellation)
    {
        var reading = await _context.MeterReadings
            .Where(x => x.AccountId == accountId)
            .OrderByDescending(x => x.Timestamp)
            .FirstOrDefaultAsync(cancellation);

        return reading?.ToDomain();
    }
    
    public async Task<MeterReading> SaveReading(
        MeterReading reading,
        CancellationToken cancellation)
    {
        var entry = await _context.MeterReadings.AddAsync(reading.ToRecord(), cancellation);
        await _context.SaveChangesAsync(cancellation);

        return entry.Entity.ToDomain();
    }
}
