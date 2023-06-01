using CsvHelper;
using Domain.MeterReadings.Entities;
using Domain.Services.CsvMeterReadings.Mappings;
using System.Globalization;

namespace Domain.Services.CsvMeterReadings;

public class CsvMeterReadingsInput : IMeterReadingsInput
{
    public async Task<IEnumerable<MeterReading>> Load(Stream input)
    {
        using var reader = new StreamReader(input);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

        csv.Context.RegisterClassMap<MeterReadingMap>();

        var records = new List<MeterReading>();
        await foreach (var record in csv.GetRecordsAsync<MeterReading>())
        {
            records.Add(record);
        }

        return records;
    }
}
