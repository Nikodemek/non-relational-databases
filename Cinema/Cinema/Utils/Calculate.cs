using System;
using System.Collections.Generic;
using System.Linq;
using Cinema.Entity;
using Cinema.Entity.Enums;

namespace Cinema.Utils;

public static class Calculate
{
    private static readonly IReadOnlyDictionary<ClientType, decimal> DiscountTable = new Dictionary<ClientType, decimal>()
    {
        { ClientType.Default, 0m },
        { ClientType.Silver, 0.1m },
        { ClientType.Gold, 0.2m },
    };

    public static int Age(Client client, DateTime? time = default)
    {
        DateTime now = time ?? DateTime.Now;
        DateTime birthday = client.Birthday;

        int age = now.Year - birthday.Year;
        if (birthday.Date > now.AddYears(-age)) age--;

        return age;
    }

    public static decimal FinalPrice(Client client, IEnumerable<Ticket> tickets)
    {
        decimal discount = DiscountTable[client.ClientType];
        decimal ticketsPrice = tickets.Sum(t => t.Price);

        decimal finalPrice = ticketsPrice - discount * ticketsPrice;
        return finalPrice;
    }
}