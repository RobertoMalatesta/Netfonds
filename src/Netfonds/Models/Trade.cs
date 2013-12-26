using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netfonds.Models {
    public class Trade {
        public DateTimeOffset Datetime { get; set; }
        public double Price { get; set; }
        public double Quantity { get; set; }

        public override string ToString() {
            return (new { Datetime, Price, Quantity }).ToString();
        }
    }
}
