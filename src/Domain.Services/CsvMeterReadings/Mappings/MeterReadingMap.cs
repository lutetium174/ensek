using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using Domain.MeterReadings.Entities;

namespace Domain.Services.CsvMeterReadings.Mappings;

internal class MeterReadingMap : ClassMap<MeterReading>
{
    public MeterReadingMap()
    {
        Map(m => m.AccountId).Name("AccountId");
        Map(m => m.Timestamp).Name("MeterReadingDateTime");
        Map(m => m.Reading).Name("MeterReadValue");

        Map(m => m.Timestamp)
            .TypeConverterOption.Format("dd/MM/yyyy HH:mm")
            .TypeConverter<DateTimeUtcConverter>();
    }

    public class DateTimeUtcConverter : DateTimeConverter
    {
        public override object? ConvertFromString(
            string? text, 
            IReaderRow row, 
            MemberMapData memberMapData)
        {        
            var dateTime = (DateTime?)base.ConvertFromString(text, row, memberMapData);
            return DateTime.SpecifyKind(dateTime ?? DateTime.MinValue, DateTimeKind.Utc);
        }

        public override string? ConvertToString(
            object? value, 
            IWriterRow row, 
            MemberMapData memberMapData)
        {            
            var dateTime = (DateTime?)value ?? DateTime.MinValue;
            return base.ConvertToString(dateTime.ToUniversalTime(), row, memberMapData);
        }
    }
}
