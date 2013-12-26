using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Netfonds.Net.Http.Configuration {
    internal class HttpRequestMessageConfigurator : IHttpRequestMessageConfigurator {
        private HttpMethod _method;
        private string _path;
        private string _format = "csv";
        private Parameters _parameters;

        private Uri _baseaddress;

        internal IHttpRequestMessageConfigurator BaseAddress(Uri value) {
            _baseaddress = value;
            return this;
        }

        internal IHttpRequestMessageConfigurator Method(HttpMethod method) {
            _method = method;
            return this;
        }

        public IHttpRequestMessageConfigurator Path(string value) {
            _path = value;
            return this;
        }

        public IHttpRequestMessageConfigurator Format(string value = "csv") {
            _format = value;
            return this;
        }

        public IHttpRequestMessageConfigurator Parameters(Parameters value) {
            _parameters = value;
            return this;
        }

        public HttpRequestMessage Build() {
            var builder = new UriBuilder(_baseaddress) {
                Query = _parameters.ToQueryString(),
            };

            builder.Path = "{0}/{1}/{2}".FormatWith(builder.Path, _path, _format);

            var message = new HttpRequestMessage {
                Method = _method,
                RequestUri = builder.Uri,
            };

            return message;
        }
    }
}