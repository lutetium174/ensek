using Domain.MeterReadings.Entities;
using MediatR;

namespace Domain.MeterReadings;

public record ImportMeterReadingsCommand(IEnumerable<MeterReading> Input) : IRequest<ImportMeterReadingsResponse>;
public record ImportMeterReadingsResponse(IEnumerable<MeterReading> Successful, IEnumerable<MeterReading> Failed);

public class MeterReadingsHandler
    : IRequestHandler<ImportMeterReadingsCommand, ImportMeterReadingsResponse>
{
    private const short MAX_READING_LENGTH = 5;

    private readonly IEnsekRepository _repository;
    public MeterReadingsHandler(IEnsekRepository repository)
    {
        _repository = repository;
    }

    public async Task<ImportMeterReadingsResponse> Handle(
        ImportMeterReadingsCommand request,
        CancellationToken cancellation)
    {
        var orderedRecordsByAccount = request.Input
            .OrderByDescending(r => r.Timestamp)
            .GroupBy(x => x.AccountId);

        var duplicateEntriesByAccount = orderedRecordsByAccount
            .SelectMany(x => x.Skip(1));

        var successfulReadings = new List<MeterReading>();
        var failedReadings = new List<MeterReading>(duplicateEntriesByAccount);

        foreach (var reading in orderedRecordsByAccount.Select(x => x.First()))
        {
            var hasAccount = await HasAccount(reading, cancellation);

            if (!(hasAccount && IsValid(reading)))
            {
                failedReadings.Add(reading);
                continue;
            }

            var latestReading = await LatestReading(reading, cancellation);
            if (latestReading?.Timestamp >= reading.Timestamp)
            {
                failedReadings.Add(reading);
                continue;
            }

            reading.Reading = reading.Reading.PadLeft(5, '0');

            var saved = await _repository.SaveReading(reading, cancellation);
            successfulReadings.Add(saved);
        }

        return new ImportMeterReadingsResponse(successfulReadings, failedReadings);
    }

    private async Task<bool> HasAccount(MeterReading reading, CancellationToken cancellation)
        => await _repository.GetAccountById(reading.AccountId, cancellation) is not null;

    private async Task<MeterReading?> LatestReading(MeterReading reading, CancellationToken cancellation)
        => await _repository.LatestReadingByAccountId(reading.AccountId, cancellation);

    private static bool IsValid(MeterReading reading)
        => reading switch
        {
            { Reading.Length: > 5 } => false,
            { Reading: var value } when int.TryParse(value, out int numeric) && numeric < 0 => false,
            null => false,

            _ => true
        };
}
