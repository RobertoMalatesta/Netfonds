using System.Net;
using System.Net.Http;

namespace Netfonds.Net.Http {
    internal class NetfondsDelegatingHandler: DelegatingHandler {
        public NetfondsDelegatingHandler() {
            InnerHandler = new HttpClientHandler {
                AllowAutoRedirect = true,
                AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip | DecompressionMethods.None,
            };
        }
    }
}