using Domain;
using Domain.Accounts.Entities;
using Domain.MeterReadings;
using Domain.MeterReadings.Entities;
using Domain.Services.CsvMeterReadings;
using Moq;
using System.Reflection;

namespace Test.Domain.Services;

public class InputValidationTests
{
    [Fact]
    public async Task IgnoreDuplicateEntry()
    {
        var repository = new Mock<IEnsekRepository>();
        repository
            .Setup(x => x.GetAccountById(It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Account { });

        var command = new ImportMeterReadingsCommand(await MockData());

        var handler = new MeterReadingsHandler(repository.Object);
        var response = await handler.Handle(command, CancellationToken.None);

        Assert.NotEmpty(response.Failed);
        Assert.Equal(4, response.Failed.Count());
    }

    [Theory]
    [InlineData(1234, 1, 0)]
    [InlineData(5432, 0, 1)]
    public async Task MatchReadingToAccount(
        int accountId,
        int successful,
        int failed)
    {
        var repository = new Mock<IEnsekRepository>();
        repository
            .Setup(x => x.GetAccountById(1234, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Account { Id = 1234 });

        var command = new ImportMeterReadingsCommand(new[]
        {
            new MeterReading
            {
                Id = 1,
                AccountId = accountId,
                Reading = "123",
                Timestamp = DateTime.UtcNow
            }
        });

        var handler = new MeterReadingsHandler(repository.Object);
        var response = await handler.Handle(command, CancellationToken.None);

        Assert.Equal(successful, response.Successful.Count());
        Assert.Equal(failed, response.Failed.Count());
    }

    [Fact]
    public async Task EnsureReadingIsFiveCharactersLong()
    {
        var repository = new Mock<IEnsekRepository>();
        repository
            .Setup(x => x.GetAccountById(1234, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Account { Id = 1234 });

        repository
            .Setup(x => x.SaveReading(It.IsAny<MeterReading>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((MeterReading reading, CancellationToken _) => reading);

        var command = new ImportMeterReadingsCommand(await MockData());

        var handler = new MeterReadingsHandler(repository.Object);
        var response = await handler.Handle(command, CancellationToken.None);

        Assert.NotEmpty(response.Successful);
        Assert.True(response.Successful.All(x => x.Reading.Length == 5));
    }

    private static async Task<IEnumerable<MeterReading>> MockData()
    {
        var assemblyDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        var testFilePath = $"{assemblyDirectory}/resources/Meter_Reading.csv";

        using var stream = new FileStream(testFilePath, FileMode.Open, FileAccess.Read);

        return await new CsvMeterReadingsInput().Load(stream);
    }
}