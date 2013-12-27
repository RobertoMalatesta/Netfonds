using System;

namespace Netfonds.Models {
    public class Quote {
        public DateTimeOffset Datetime { get; set; }
        
        public double BidPrice { get; set; }
        
        public double BidSize { get; set; }
        
        public double AskPrice { get; set; }
        
        public double AskSize { get; set; }

        public override string ToString() {
            return (new { Datetime, BidPrice, BidSize, AskPrice, AskSize }).ToString();
        }
    }
}
