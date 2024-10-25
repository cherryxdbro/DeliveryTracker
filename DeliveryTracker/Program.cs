using System.Globalization;
using Microsoft.Extensions.Configuration;
using NLog;
using NLog.Config;
using NLog.Layouts;

namespace DeliveryTracker;

public static class Program
{
    private const string LoggerLayoutText =
        "${longdate}|${level}|${logger}|${threadid}|${message:withexception=true}";

    private static readonly Logger logger = LogManager.GetCurrentClassLogger();

    public static void Main(string[] arguments)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .AddJsonFile(path: "appsettings.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables()
            .AddCommandLine(args: arguments)
            .Build();

        if (
            !Guid.TryParseExact(
                input: configuration[key: "CityDistrict"],
                format: null,
                result: out Guid cityDistrict
            )
        )
            Console.WriteLine("CityDistrict invalid");

        if (
            !DateTime.TryParseExact(
                s: configuration[key: "FirstDeliveryDateTime"],
                format: "yyyy-MM-dd HH:mm:ss",
                provider: CultureInfo.InvariantCulture,
                style: DateTimeStyles.None,
                result: out DateTime firstDeliveryDateTime
            )
        )
            Console.WriteLine("FirstDeliveryDateTime invalid");

        string deliveryLog = configuration[key: "DeliveryLog"] ?? "delivery_tracker.log";
        string deliveryOrder = configuration[key: "DeliveryOrder"] ?? "filtered_orders.csv";

        SetupLogging(logFilePath: deliveryLog);

        try
        {
            logger.Info(message: "Loading orders from \"orders.csv\"...");
            IEnumerable<Order> orders = OrderFileHandler.LoadOrdersFromFileCSV(
                filePath: "orders.csv"
            );

            logger.Info(message: "Filtering orders...");
            IEnumerable<Order> filteredOrders = OrderFilter.FilterOrders(
                orders: orders,
                cityDistrict: cityDistrict,
                firstDeliveryDateTime: firstDeliveryDateTime
            );

            logger.Info(message: "Saving filtered orders to {}...", argument: deliveryOrder);
            OrderFileHandler.SaveOrdersToFileCSV(orders: filteredOrders, filePath: deliveryOrder);

            logger.Info(message: "Operations completed successfully");
        }
        catch (Exception exception)
        {
            logger.Error(exception: exception, message: "An error occurred during the process");
        }
        finally
        {
            LogManager.Shutdown();
        }
    }

    private static void SetupLogging(string logFilePath)
    {
        ISetupBuilder setupBuilder = LogManager
            .Setup()
            .LoadConfiguration(configBuilder: builder =>
            {
                ISetupConfigurationTargetBuilder setupColoredConsole = builder
                    .ForLogger()
                    .FilterMinLevel(minLevel: LogLevel.Info)
                    .WriteToColoredConsole(layout: Layout.FromString(layoutText: LoggerLayoutText));
                ISetupConfigurationTargetBuilder setupFile = builder
                    .ForLogger()
                    .FilterMinLevel(minLevel: LogLevel.Trace)
                    .WriteToFile(
                        fileName: logFilePath,
                        layout: Layout.FromString(layoutText: LoggerLayoutText)
                    );
            });
    }
}
