using Netfonds.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
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
            var trades = new Trades();
            using(var reader = new StreamReader(stream)) {
                var line = reader.ReadLine();
                var seperator = ",".ToCharArray();
                while(null != (line = reader.ReadLine())) {
                    var parts = line.Split(seperator);

                    var datetime = parts[0].Parse("yyyyMMddTHHmmss", _timezone);
                    var price = parts[1].Parse();
                    var quantity = parts[2].Parse();

                    trades.Add(datetime, price, quantity);
                }
            }

            var tcs = new TaskCompletionSource<object>();
            tcs.SetResult(trades);
            return tcs.Task;
        }
    }

    public static partial class Extensions {
        internal static Trades Add(this Trades source, DateTimeOffset datetime, double price, double quantity) {
            source.Add(new Trade {
                Datetime = datetime,
                Price = price,
                Quantity = quantity
            });
            return source;
        }

        internal static DateTimeOffset Parse(this string source, string format, TimeZoneInfo timezone) {
            var datetime = DateTime.ParseExact(source, format, null);
            return new DateTimeOffset(datetime.Year, datetime.Month, datetime.Day, datetime.Hour, datetime.Minute, datetime.Second, datetime.Millisecond, timezone.GetUtcOffset(datetime));
        }

        internal static double Parse(this string source) {
            return double.Parse(source);
        }
    }
}
