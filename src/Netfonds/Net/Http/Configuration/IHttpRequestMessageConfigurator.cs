using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netfonds.Net.Http.Configuration {
    internal interface IHttpRequestMessageConfigurator {
        IHttpRequestMessageConfigurator Path(string value);
        IHttpRequestMessageConfigurator Format(string value = "csv");
        IHttpRequestMessageConfigurator Parameters(Parameters value);
    }
}
