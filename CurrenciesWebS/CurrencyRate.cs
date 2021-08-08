using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CurrenciesWebS
{
    /// <summary>
    /// Model of Currency Rate
    /// </summary>
    public class CurrencyRate
    {
        public string Name { get; set; }
        public string Rate { get; set; }
        public string Date { get; set; }
    }
}
