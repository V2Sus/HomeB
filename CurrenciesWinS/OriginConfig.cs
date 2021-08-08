using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrenciesWinS
{
    /// <summary>
    /// Origin configuration
    /// </summary>
    public class OriginConfig
    {
        public string OriginCurrRateUrl { get; }
        /// <summary>
        /// Filter to find rate in origin html page
        /// </summary>
        public string OriginCurrRateFilter { get; }
        /// <summary>
        /// Delay request in mc, against source blocking
        /// </summary>
        public int OriginDelayRequest { get; }
        /// <summary>
        /// constuctor
        /// Get values from config file
        /// </summary>
        public OriginConfig()
        {
            OriginCurrRateUrl = ConfigurationManager.AppSettings["OriginCurrRateUrl"];
            OriginCurrRateFilter = ConfigurationManager.AppSettings["OriginCurrRateFilter"];
            OriginDelayRequest = Int32.Parse(ConfigurationManager.AppSettings["DelayRequest"]);
        }
    }
}
