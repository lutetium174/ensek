using Domain.Services.CsvMeterReadings;
using System.Reflection;

namespace Test.Domain.Services;

public class CsvReadTests
{
    [Fact]
    public async Task ReadingsFromCsv()
    {
        var sut = new CsvMeterReadingsInput();
        var assemblyDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        var testFilePath = $"{assemblyDirectory}/resources/Meter_Reading.csv";

        using var stream = new FileStream(testFilePath, FileMode.Open, FileAccess.Read);

        var readings = await sut.Load(stream);
        Assert.NotEmpty(readings);
    }
}