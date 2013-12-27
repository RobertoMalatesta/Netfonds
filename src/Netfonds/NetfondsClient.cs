using Netfonds.Net.Http;
using Netfonds.Net.Http.Formatting;
using System;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using Quotes = System.Collections.Generic.List<Netfonds.Models.Quote>;
using Trades = System.Collections.Generic.List<Netfonds.Models.Trade>;

namespace Netfonds {
    public partial class NetfondsClient : IDisposable {
        private MediaTypeFormatter _formatter = new TradeMediaTypeFormatter();
        private HttpMessageHandler _handler = new NetfondsDelegatingHandler();
        private HttpClient _client;
        private volatile bool _disposed;
        public NetfondsClient(string uri = "http://hopey.netfonds.no") {
            _client = new HttpClient(_handler);
            _client.BaseAddress = new Uri(uri);
        }
        ~NetfondsClient() {
            Dispose(false);
        }

        //http://hopey.netfonds.no/tradedump.php?date=20120423&paper=AAPL.O&csv_format=csv
        public Task<Trades> GetTradesAsync(DateTimeOffset datetime = default(DateTimeOffset), string symbol = (string)null, string exchange = (string)null) {
            return _client.SendAsync(x => x
                .Method(HttpMethod.Get)
                .Address("tradedump.php?date={0}&paper={1}.{2}&csv_format=csv", datetime.ToString("yyyyMMdd"), symbol, exchange)                
            ).ReadAsAsync<Trades>(new TradeMediaTypeFormatter());
        }

        //http://hopey.netfonds.no/posdump.php?date=20131223&paper=AAPL.O&csv_format=csv
        public Task<Quotes> GetQuotesAsync(DateTimeOffset datetime = default(DateTimeOffset), string symbol = (string)null, string exchange = (string)null) {
            return _client.SendAsync(x => x
                .Method(HttpMethod.Get)
                .Address("posdump.php?date={0}&paper={1}.{2}&csv_format=csv",datetime.ToString("yyyyMMdd"),symbol,exchange)
            ).ReadAsAsync<Quotes>(new QuoteMediaTypeFormatter());
        }

        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing) {
            if(disposing) {
            }
            _disposed = true;
        }
    }

    public static partial class Extensions {
        public static Task<Trades> GetTradesAsync(this HttpResponseMessage source) {
            return source.Content.ReadAsAsync<Trades>(new[] { new TradeMediaTypeFormatter() });
        }        
    }
}
