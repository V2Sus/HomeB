using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CurrenciesWinS
{
    public partial class Currencies : ServiceBase
    {
        /// <summary>
        /// CancellationToken to abort tasks
        /// </summary>
        private CancellationTokenSource ts;
        public Currencies()
        {
            InitializeComponent();
        }
        /// <summary>
        /// initialization data to start service
        /// </summary>
        /// <param name="args"></param>
        protected override void OnStart(string[] args)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Currencies"].ConnectionString;
            OriginConfig originConfig = new OriginConfig();
            ts = new CancellationTokenSource();
            List<string> currenciesDuo=getCurennciesDuo(connectionString);
            //a separate task is launched for each currency duo
            foreach (string currencyDuo in currenciesDuo)
            {
                Task.Factory.StartNew(() =>
                {
                    new CurrencyDuo(currencyDuo, connectionString, originConfig).Procces();
                }, ts.Token);
            }
        }
        /// <summary>
        /// Get currencies Duo from db.
        /// To add or change currency Duo -  insert or update row in CurrenciesDuo table
        /// </summary>
        /// <param name="connectionString">Connection String to db</param>
        /// <returns></returns>
        private List<string> getCurennciesDuo(string connectionString)
        {
            List<string> currenciesDuo = new List<string>();
            using (SqlConnection myConnection = new SqlConnection(connectionString))
            {
                string command = "Select Name from CurrenciesDuo";
                SqlCommand Cmd = new SqlCommand(command, myConnection);
                myConnection.Open();
                using (SqlDataReader reader = Cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        currenciesDuo.Add(reader["Name"].ToString());
                    }
                }
                myConnection.Close();
            }
            return currenciesDuo;
        }
        /// <summary>
        /// Stop service
        /// </summary>
        protected override void OnStop()
        {
           //Abort all tasks
            ts.Cancel();
            ts.Dispose();
        }
    }
}
