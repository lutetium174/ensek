using Domain.MeterReadings.Entities;

namespace Domain;

public interface IMeterReadingsInput
{
    Task<IEnumerable<MeterReading>> Load(Stream input);
}