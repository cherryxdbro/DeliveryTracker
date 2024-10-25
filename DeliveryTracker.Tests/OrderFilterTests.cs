namespace DeliveryTracker.Tests;

public class OrderFilterTests
{
    [Fact]
    public void FilterOrders_ValidInput_ReturnsFilteredOrders()
    {
        Guid leftDistrict = Guid.NewGuid();
        Guid rightDistrict = Guid.NewGuid();

        // Arrange
        List<Order> orders =
        [
            new(
                id: Guid.NewGuid(),
                weight: 2.5,
                cityDistrict: leftDistrict,
                deliveryDateTime: DateTime.Parse("2023-10-24 10:00:00")
            ),
            new(
                id: Guid.NewGuid(),
                weight: 1.0,
                cityDistrict: leftDistrict,
                deliveryDateTime: DateTime.Parse("2023-10-24 10:15:00")
            ),
            new(
                id: Guid.NewGuid(),
                weight: 1.5,
                cityDistrict: rightDistrict,
                deliveryDateTime: DateTime.Parse("2023-10-24 10:20:00")
            ),
        ];

        DateTime firstDeliveryDateTime = DateTime.Parse(s: "2023-10-24 10:00:00");

        // Act
        var filteredOrders = OrderFilter
            .FilterOrders(
                orders: orders,
                cityDistrict: leftDistrict,
                firstDeliveryDateTime: firstDeliveryDateTime
            )
            .ToList();

        // Assert
        Assert.Equal(expected: 2, actual: filteredOrders.Count);
    }
}
