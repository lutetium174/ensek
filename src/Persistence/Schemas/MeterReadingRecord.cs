namespace Persistence.Schemas;

public class MeterReadingRecord
{
    public int Id { get; set; }
    public int AccountId { get; set; }
    public DateTime Timestamp { get; set; }
    public string Reading { get; set; } = null!;
}
