using CommandLine;

namespace DeliveryTracker;

internal class Arguments
{
    [Option(
        shortName: 'd',
        longName: "cityDistrict",
        Required = true,
        HelpText = "City district for filtering orders."
    )]
    public required string CityDistrict { get; set; }

    [Option(
        shortName: 't',
        longName: "firstDeliveryDateTime",
        Required = true,
        HelpText = "First delivery time in format yyyy-MM-dd HH:mm:ss."
    )]
    public required string FirstDeliveryDateTime { get; set; }

    [Option(
        shortName: 'l',
        longName: "deliveryLog",
        Required = true,
        HelpText = "Path to the log file."
    )]
    public required string DeliveryLog { get; set; }

    [Option(
        shortName: 'o',
        longName: "deliveryOrder",
        Required = true,
        HelpText = "Path to the output file."
    )]
    public required string DeliveryOrder { get; set; }
}
