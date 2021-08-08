using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CurrenciesWebS.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CurrencyRateController : ControllerBase
    {
        private readonly string connectionString;
        /// <summary>
        /// Consructor
        /// </summary>
        /// <param name="configuration"></param>
        public CurrencyRateController(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        /// <summary>
        /// Get values from SQL Server
        /// </summary>
        /// <returns>List of Currency Rate</returns>
        [HttpGet]
        public IEnumerable<CurrencyRate> Get()
        {
            List<CurrencyRate> currenciesDuo = new List<CurrencyRate>();
            string command = "Select * from CurrenciesDuo";
            CurrencyRate cr;
            using (SqlConnection myConnection = new SqlConnection(connectionString))
            {
                SqlCommand Cmd = new SqlCommand(command, myConnection);
                myConnection.Open();
                using (SqlDataReader reader = Cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        cr = new CurrencyRate();
                        cr.Name = reader["Name"].ToString();
                        cr.Rate = reader["Rate"].ToString();
                        cr.Date = reader["Date"].ToString();
                        currenciesDuo.Add(cr);
                    }
                }
                myConnection.Close();
            }
            return currenciesDuo;
        }
    }
}
