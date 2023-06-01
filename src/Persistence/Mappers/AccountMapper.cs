using Domain.Accounts.Entities;
using Persistence.Schemas;

namespace Persistence.Mappers;

public static class AccountMapper
{
    public static Account ToDomain(this AccountRecord record)
        => new()
        {
            Id = record.Id,
            FirstName = record.FirstName,
            Surname = record.Surname,
        };
}
