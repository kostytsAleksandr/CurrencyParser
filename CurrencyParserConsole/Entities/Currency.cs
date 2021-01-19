using System;
using System.Collections.Generic;
using System.Text;

namespace CurrencyParserConsole.Entities
{
    class Currency
    {
        public int CurrencyCodeA { get; set; }
        public int CurrencyCodeB { get; set; }
        public long Date { get; set; }
        public double? RateBuy { get; set; }
        public double RateSell { get; set; }
        public double RateCross { get; set; }

        public override string ToString()
        {
            return $"CurrencyCodeA: {CurrencyCodeA} Date: {Date} RateSell: {RateSell} RateCross: {RateCross}";
        }
    }
}
