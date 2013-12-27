Netfonds
========

C# implementation to pull market data from Netfonds.no

## References
- http://hopey.netfonds.no

## Example
```c#
var client = new NetfondsClient();

var trades = await client.GetTradesAsync(
    datetime: DateTimeOffset.Now.AddDays(-3), 
    symbol: "AAPL", 
    exchange: "O"
);

var quotes = await client.GetQuotesAsync(
    datetime: DateTimeOffset.Now.AddDays(-3), 
    symbol: "AAPL", 
    exchange: "O"
);
```