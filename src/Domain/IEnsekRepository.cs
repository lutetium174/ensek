using Domain.Accounts.Entities;
using Domain.MeterReadings.Entities;

namespace Domain;

public interface IEnsekRepository
{
    Task<Account?> GetAccountById(
        int accountId, 
        CancellationToken cancellation);

    Task<MeterReading> SaveReading(
        MeterReading reading, 
        CancellationToken cancellation);

    Task<MeterReading?> LatestReadingByAccountId(
        int accountId,
        CancellationToken cancellation);
}