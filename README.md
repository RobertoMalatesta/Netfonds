Netfonds
========

C# implementation to pull market data from Netfonds.no

## References
- http://hopey.netfonds.no

## Example
```c#
var client = new NetfondsClient();

var trades = client.GetTradesAsync(
    datetime: DateTimeOffset.Now.AddDays(-3), 
    symbol: "AAPL", 
    exchange: "O"
).Result;

var quotes = client.GetQuotesAsync(
    datetime: DateTimeOffset.Now.AddDays(-3), 
    symbol: "AAPL", 
    exchange: "O"
).Result;
```