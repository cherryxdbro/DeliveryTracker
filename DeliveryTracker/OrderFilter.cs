namespace DeliveryTracker;

internal static class OrderFilter
{
    internal static IEnumerable<Order> FilterOrders(
        IEnumerable<Order> orders,
        string cityDistrict,
        DateTime firstDeliveryDateTime
    )
    {
        DateTime endDateTime = firstDeliveryDateTime.AddMinutes(value: 30);
        return orders.Where(predicate: order =>
            order.CityDistrict == cityDistrict
            && order.DeliveryDateTime >= firstDeliveryDateTime
            && order.DeliveryDateTime <= endDateTime
        );
    }
}
