using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Netfonds {
    public partial class NetfondsClient {
        public interface IGetTradesConfigurator {
            IGetTradesConfigurator Date(DateTimeOffset value);
            IGetTradesConfigurator Symbol(string value);
            IGetTradesConfigurator Exchange(string value);
        }

        private class GetTradesSettings {
            public DateTimeOffset Date { get; set; }
            public string Symbol { get; set; }
            public string Exchange { get; set; }
            public string Format { get; set; }
        }

        private class GetTradesConfigurator : IGetTradesConfigurator {
            private GetTradesSettings _settings = new GetTradesSettings {
                Format = "csv"
            };

            public IGetTradesConfigurator Date(DateTimeOffset value) {
                _settings.Date = value.Date;
                return this;
            }

            public IGetTradesConfigurator Symbol(string value) {
                _settings.Symbol = value;
                return this;
            }

            public IGetTradesConfigurator Exchange(string value) {
                _settings.Exchange = value;
                return this;
            }

            public HttpRequestMessage Build() {
                var parameters = new Parameters();
                parameters.Add("date", _settings.Date.ToString("yyyyMMdd"));
                parameters.Add("paper", "{0}.{1}".FormatWith(_settings.Symbol, _settings.Exchange));
                parameters.Add("csv_format", _settings.Format);

                var query = parameters.ToQueryString();

                var uri = "tradedump.php?{0}".FormatWith(query);

                var request = new HttpRequestMessage {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri(uri, UriKind.Relative)
                };

                return request;
            }
        }
    }
}