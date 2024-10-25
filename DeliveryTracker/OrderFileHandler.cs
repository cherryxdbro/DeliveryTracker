namespace DeliveryTracker;

public static class OrderFileHandler
{
    public static IEnumerable<Order> LoadOrdersFromFileCSV(string filePath)
    {
        List<Order> orders = [];

        if (!File.Exists(path: filePath))
        {
            return orders;
        }

        try
        {
            foreach (string line in File.ReadLines(path: filePath))
            {
                if (Order.TryParse(s: line, provider: null, result: out Order? order))
                {
                    orders.Add(item: order);
                }
            }
        }
        catch (Exception exception)
        {
            throw new IOException(message: "Error reading orders file", innerException: exception);
        }

        return orders;
    }

    public static void SaveOrdersToFileCSV(IEnumerable<Order> orders, string filePath)
    {
        try
        {
            using StreamWriter writer = new(path: filePath);

            writer.WriteLine(value: "OrderId,Weight,CityDistrict,DeliveryDateTime");

            foreach (Order order in orders)
            {
                writer.WriteLine(
                    value: $"{order.Id},{order.Weight},{order.CityDistrict},{order.DeliveryDateTime:yyyy-MM-dd HH:mm:ss}"
                );
            }
        }
        catch (Exception exception)
        {
            throw new IOException(
                message: "Error writing orders to file",
                innerException: exception
            );
        }
    }
}
