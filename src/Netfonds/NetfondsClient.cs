using Netfonds.Models;
using Netfonds.Net.Http;
using Netfonds.Net.Http.Configuration;
using Netfonds.Net.Http.Formatting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;

namespace Netfonds {
    public partial class NetfondsClient : IDisposable {
        private MediaTypeFormatter _formatter = new TradeMediaTypeFormatter();
        private HttpMessageHandler _handler = new NetfondsDelegatingHandler();
        private HttpClient _client;
        private volatile bool _disposed;
        public NetfondsClient(string uri = "http://hopey.netfonds.no") {
            //_handler = new DefaultHttpMessageHandler();
            _client = new HttpClient(_handler);
            _client.BaseAddress = new Uri(uri);
        }
        ~NetfondsClient() {
            Dispose(false);
        }

        private Task<HttpResponseMessage> SendAsync(HttpMethod method, Action<IHttpRequestMessageConfigurator> configure) {
            var configurator = new HttpRequestMessageConfigurator();

            configurator.Method(method);
            configurator.BaseAddress(_client.BaseAddress);
            configure(configurator);

            var request = configurator.Build();
            return _client.SendAsync(request);
        }
        public async Task<Trades> GetTradesAsync(Action<IGetTradesConfigurator> configure) {
            var c = new GetTradesConfigurator();
            configure(c);

            var request = c.Build();

            return await _client.SendAsync(request)
                .GetAwaiter()
                .GetResult()
                .GetTradesAsync();                
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
