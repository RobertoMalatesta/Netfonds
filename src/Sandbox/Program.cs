using Netfonds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox {
    class Program {
        static void Main(string[] args) {
            var client = new NetfondsClient();
            var trades = client.GetTradesAsync(x => x
                .Date(DateTimeOffset.Now.AddDays(-1))
                .Exchange("O")
                .Symbol("AAPL")
            ).Result;

            foreach(var trade in trades) {
                Console.WriteLine(trade);
            }
        }
    }
}
