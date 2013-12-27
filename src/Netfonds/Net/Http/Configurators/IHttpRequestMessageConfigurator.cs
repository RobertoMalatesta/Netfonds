using System.Net.Http;

namespace Netfonds.Net.Http.Configurators {
    internal interface IHttpRequestMessageConfigurator {
        IHttpRequestMessageConfigurator Method(HttpMethod value);
        IHttpRequestMessageConfigurator Address(string value);
        IHttpRequestMessageConfigurator Header(string name, string value);
    }
}
