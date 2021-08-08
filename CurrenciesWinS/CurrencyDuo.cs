using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CurrenciesWinS
{
    class CurrencyDuo
    {
        /// <summary>
        /// Name of currenvy duo
        /// </summary>
        public string Name { get; }
        private readonly string connectionString;
        private readonly OriginConfig originConfig;
        /// <summary>
        /// Constructor
        /// Initialize Data for proccesing
        /// </summary>
        /// <param name="name"></param>
        /// <param name="connstring"></param>
        /// <param name="orConfig"></param>
        public CurrencyDuo(string name, string connstring, OriginConfig orConfig)
        {
            Name = name;
            connectionString = connstring;
            originConfig = orConfig;
        }
        /// <summary>
        /// Proccesing. Get rate from origin and set to DB
        /// </summary>
        public void Procces()
        {
            while (true)
            {
                string rate = String.Empty;
                rate = getRate();
                if (!String.IsNullOrEmpty(rate)) saveRate(rate);
                //against source blocking
                Thread.Sleep(originConfig.OriginDelayRequest);
            }
        }
        /// <summary>
        /// Get value from origin
        /// </summary>
        /// <returns>Currensy rate</returns>
        private string getRate()
        {
            string rate = String.Empty;
            string url = String.Format(originConfig.OriginCurrRateUrl, Name.Remove(3, 1));
            string content = new System.Net.WebClient().DownloadString(url);
            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(content);
            rate = htmlDoc.DocumentNode.SelectSingleNode(originConfig.OriginCurrRateFilter).InnerText;
            return rate;
        }
        /// <summary>
        /// Save rate and update date to DB
        /// </summary>
        /// <param name="rate"></param>
        private void saveRate(string rate)
        {
            string queryString = "Update CurrenciesDuo set rate=" + rate + ", Date=getdate() where Name='" + Name + "'";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();
                command.ExecuteNonQuery();
            }
        }

    }
}
