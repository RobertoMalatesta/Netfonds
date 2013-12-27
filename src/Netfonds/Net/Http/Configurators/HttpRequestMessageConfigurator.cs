using System;
using System.Net.Http;

namespace Netfonds.Net.Http.Configurators {
    internal class HttpRequestMessageConfigurator : IHttpRequestMessageConfigurator {
        private HttpRequestMessage _request = new HttpRequestMessage();

        public HttpRequestMessageConfigurator() {
        }

        public IHttpRequestMessageConfigurator Method(HttpMethod value) {
            _request.Method = value;
            return this;
        }

        public IHttpRequestMessageConfigurator Address(string value) {
            _request.RequestUri = new Uri(value, UriKind.RelativeOrAbsolute);
            return this;
        }

        public IHttpRequestMessageConfigurator Header(string name, string value) {
            _request.Headers.Add(name, value);
            return this;
        }

        public HttpRequestMessage Build() {
            return _request;
        }
    }
}
