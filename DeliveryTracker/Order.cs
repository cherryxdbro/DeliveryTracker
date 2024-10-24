using System.Diagnostics.CodeAnalysis;

namespace DeliveryTracker;

public class Order(Guid id, double weight, string cityDistrict, DateTime deliveryDateTime)
    : IParsable<Order>
{
    public Guid Id { get; set; } = id;
    public double Weight { get; set; } = weight;
    public string CityDistrict { get; set; } = cityDistrict;
    public DateTime DeliveryDateTime { get; set; } = deliveryDateTime;

    public static Order Parse(string s, IFormatProvider? provider)
    {
        string[] parts = s.Split(separator: ',');
        if (parts.Length != 4)
            throw new FormatException(message: "Incorrect order format");

        return new Order(
            id: Guid.Parse(parts[0]),
            weight: double.Parse(s: parts[1]),
            cityDistrict: parts[2],
            deliveryDateTime: DateTime.Parse(s: parts[3])
        );
    }

    public static bool TryParse(
        [NotNullWhen(true)] string? s,
        IFormatProvider? provider,
        [MaybeNullWhen(false)] out Order result
    )
    {
        result = null;
        if (string.IsNullOrWhiteSpace(value: s))
            return false;

        try
        {
            result = Parse(s: s, provider: provider);
            return true;
        }
        catch
        {
            return false;
        }
    }
}
