namespace DeliveryTracker.Tests;

public class OrderTests
{
    [Theory]
    [InlineData(
        data: "809e6f7d-a64c-42bd-b939-1b448f187571,2.5,69236d81-c7da-49f0-8c8d-0679f677de08,2023-10-24 10:00:00"
    )]
    [InlineData(
        data: "7fcce8a0-1de5-42cf-b6d4-26ff1b59c483,1.0,f375f113-7089-40ee-a862-5912432d8c6c,2023-10-24 10:05:00"
    )]
    public void Parse_ValidString_ReturnsOrder(string input)
    {
        // Act
        Order order = Order.Parse(s: input, provider: null);

        // Assert
        Assert.NotNull(@object: order);
        Assert.NotEqual(expected: Guid.Empty, actual: order.Id);
    }

    [Theory]
    [InlineData(data: "Invalid,Input")]
    [InlineData(
        data: "809e6f7d-a64c-42bd-b939-1b448f187571,InvalidWeight,69236d81-c7da-49f0-8c8d-0679f677de08,2023-10-24 10:00:00"
    )]
    public void Parse_InvalidString_ThrowsFormatException(string input)
    {
        // Act & Assert
        FormatException formatException = Assert.Throws<FormatException>(
            testCode: () => Order.Parse(s: input, provider: null)
        );
    }
}
