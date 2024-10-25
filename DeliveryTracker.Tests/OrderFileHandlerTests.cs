using System.Globalization;

namespace DeliveryTracker.Tests;

public class OrderFileHandlerTests
{
    [Fact]
    public void LoadOrdersFromFileCSV_ValidFile_ReturnsOrders()
    {
        // Arrange
        List<Order> expectedOrders =
        [
            new(
                id: Guid.NewGuid(),
                weight: 2.5,
                cityDistrict: Guid.NewGuid(),
                deliveryDateTime: DateTime.Parse(s: "2023-10-24 10:00:00")
            ),
            new(
                id: Guid.NewGuid(),
                weight: 1.0,
                cityDistrict: Guid.NewGuid(),
                deliveryDateTime: DateTime.Parse(s: "2023-10-24 10:05:00")
            ),
        ];

        File.WriteAllLines(
            path: "test_orders.csv",
            contents:
            [
                "OrderId,Weight,CityDistrict,DeliveryDateTime",
                $"{expectedOrders[index: 0].Id},{expectedOrders[index: 0].Weight.ToString(CultureInfo.InvariantCulture)},{expectedOrders[index: 0].CityDistrict},{expectedOrders[index: 0].DeliveryDateTime:yyyy-MM-dd HH:mm:ss}",
                $"{expectedOrders[index: 1].Id},{expectedOrders[index: 1].Weight.ToString(CultureInfo.InvariantCulture)},{expectedOrders[index: 1].CityDistrict},{expectedOrders[index: 1].DeliveryDateTime:yyyy-MM-dd HH:mm:ss}",
            ]
        );

        // Act
        IEnumerable<Order> orders = OrderFileHandler.LoadOrdersFromFileCSV(
            filePath: "test_orders.csv"
        );

        // Assert
        Assert.Equal(expected: expectedOrders.Count, actual: orders.Count());
    }

    [Fact]
    public void SaveOrdersToFileCSV_ValidOrders_WritesToFile()
    {
        // Arrange
        List<Order> orders =
        [
            new(
                id: Guid.NewGuid(),
                weight: 2.5,
                cityDistrict: Guid.NewGuid(),
                deliveryDateTime: DateTime.Parse(s: "2023-10-24 10:00:00")
            ),
            new(
                id: Guid.NewGuid(),
                weight: 1.0,
                cityDistrict: Guid.NewGuid(),
                deliveryDateTime: DateTime.Parse(s: "2023-10-24 10:05:00")
            ),
        ];

        // Act
        OrderFileHandler.SaveOrdersToFileCSV(orders: orders, filePath: "output_orders.csv");

        // Assert
        string[] savedOrders = File.ReadAllLines(path: "output_orders.csv");
        Assert.Equal(expected: 3, actual: savedOrders.Length);
        Assert.Contains(expectedSubstring: "OrderId,Weight,CityDistrict,DeliveryDateTime", actualString: savedOrders[0]);
    }
}
