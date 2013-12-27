using Netfonds;
using System;

namespace Sandbox {
    class Program {
        static void Main(string[] args) {
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
        }
    }
}
