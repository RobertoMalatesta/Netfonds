using Netfonds.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Netfonds.Net.Http.Formatting {
    internal class TradeMediaTypeFormatter : MediaTypeFormatter{
        private TimeZoneInfo _timezone = TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time");
        public TradeMediaTypeFormatter() {
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/plain"));
        }

        public override bool CanReadType(Type type) {
            return (type == typeof(Trade)) ? true : typeof(IEnumerable<Trade>).IsAssignableFrom(type);            
        }
        public override bool CanWriteType(Type type) {
            return false;
        }

        public override Task<object> ReadFromStreamAsync(Type type, Stream stream, HttpContent content, IFormatterLogger logger) {
            var trades = new List<Trade>();
            using(var reader = new StreamReader(stream)) {
                var line = reader.ReadLine();
                var seperator = ",".ToCharArray();
                while(null != (line = reader.ReadLine())) {
                    var parts = line.Split(seperator);

                    var datetime = parts[0].Parse("yyyyMMddTHHmmss", _timezone);
                    var price = parts[1].Parse();
                    var quantity = parts[2].Parse();

                    trades.Add(new Trade {
                        Datetime = datetime,
                        Price = price,
                        Quantity = quantity,
                    });
                }
            }

            var tcs = new TaskCompletionSource<object>();
            tcs.SetResult(trades);
            return tcs.Task;
        }
    }
}
