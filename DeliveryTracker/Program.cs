using System.Globalization;
using CommandLine;
using NLog;
using NLog.Config;
using NLog.Layouts;

namespace DeliveryTracker;

public class Program
{
    private const string LoggerLayoutText =
        "${longdate}|${level}|${logger}|${threadid}|${message:withexception=true}";

    private static readonly Logger logger = LogManager.GetCurrentClassLogger();

    public static async Task Main(string[] arguments)
    {
        ParserResult<Arguments> parserResult = Parser
            .Default.ParseArguments<Arguments>(args: arguments)
            .WithParsed(action: RunWithOptions)
            .WithNotParsed(action: HandleErrors);
    }

    private static void RunWithOptions(Arguments arguments)
    {
        SetupLogging(logFilePath: arguments.DeliveryLog);

        if (
            !DateTime.TryParseExact(
                s: arguments.FirstDeliveryDateTime,
                format: "yyyy-MM-dd HH:mm:ss",
                provider: CultureInfo.InvariantCulture,
                style: DateTimeStyles.None,
                result: out DateTime firstDeliveryDateTime
            )
        )
        {
            logger.Error(message: "Invalid date format");
            return;
        }

        try
        {
            logger.Info(message: "Loading orders from \"orders.csv\"...");
            IEnumerable<Order> orders = OrderFileHandler.LoadOrdersFromFileCSV(
                filePath: "orders.csv"
            );

            logger.Info(message: "Filtering orders...");
            IEnumerable<Order> filteredOrders = OrderFilter.FilterOrders(
                orders: orders,
                cityDistrict: arguments.CityDistrict,
                firstDeliveryDateTime: firstDeliveryDateTime
            );

            logger.Info(
                message: "Saving filtered orders to {}...",
                argument: arguments.DeliveryOrder
            );
            OrderFileHandler.SaveOrdersToFileCSV(
                orders: filteredOrders,
                filePath: arguments.DeliveryOrder
            );

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

    private static void HandleErrors(IEnumerable<Error> errors)
    {
        foreach (Error error in errors)
        {
            Console.WriteLine(value: error.ToString());
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
