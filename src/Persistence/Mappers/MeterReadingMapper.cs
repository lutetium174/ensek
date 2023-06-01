using Domain.MeterReadings.Entities;
using Persistence.Schemas;

namespace Persistence.Mappers;

public static class MeterReadingMapper
{
    public static MeterReading ToDomain(this MeterReadingRecord record)
        => new()
        {
            Id = record.Id,
            AccountId = record.AccountId,
            Reading = record.Reading,
            Timestamp = record.Timestamp,
        };

    public static MeterReadingRecord ToRecord(this MeterReading entity)
        => new()
        {
            AccountId = entity.AccountId,
            Timestamp = entity.Timestamp,
            Reading = entity.Reading
        };
}
