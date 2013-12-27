using Netfonds.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Netfonds.Net.Http.Formatting {    
    internal class QuoteMediaTypeFormatter : MediaTypeFormatter {
        private TimeZoneInfo _timezone = TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time");
        public QuoteMediaTypeFormatter() {
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/plain"));
        }

        public override bool CanReadType(Type type) {
            return (type == typeof(Quote)) ? true : typeof(IEnumerable<Quote>).IsAssignableFrom(type);
        }
        public override bool CanWriteType(Type type) {
            return false;
        }

        public override Task<object> ReadFromStreamAsync(Type type, Stream stream, HttpContent content, IFormatterLogger logger) {
            var quotes = new List<Quote>();
            using(var reader = new StreamReader(stream)) {
                var line = reader.ReadLine();
                var seperator = ",".ToCharArray();
                while(null != (line = reader.ReadLine())) {
                    var parts = line.Split(seperator);

                    var datetime = parts[0].Parse("yyyyMMddTHHmmss", _timezone);
                    var bidPrice = parts[1].Parse();
                    var bidSize = parts[2].Parse();
                    var askPrice = parts[4].Parse();
                    var askSize = parts[5].Parse();

                    quotes.Add(new Quote {
                        AskPrice = askPrice,
                        AskSize = askSize,
                        BidPrice = bidPrice,
                        BidSize = bidSize,
                        Datetime = datetime,
                    });
                }
            }

            var tcs = new TaskCompletionSource<object>();
            tcs.SetResult(quotes);
            return tcs.Task;
        }
    }
}
