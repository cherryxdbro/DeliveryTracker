namespace DeliveryTracker;

public static class OrderFilter
{
    public static IEnumerable<Order> FilterOrders(
        IEnumerable<Order> orders,
        Guid cityDistrict,
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
