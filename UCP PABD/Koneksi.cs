using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Configuration; // Tambahkan ini untuk ConfigurationManager

namespace UCP_PABD
{
    internal class Koneksi
    {
        public string connectionStirng()
        {
            return "Server=TopUpGameOL.mssql.somee.com;" +
                   "Database=TopUpGameOL;" +
                   "User Id=VOLTAROU_SQLLogin_1;" +
                   "Password=xu782mqohm;" +
                   "Encrypt=False;" +
                   "TrustServerCertificate=True;";
        }

        public static bool IsConnectedToInternet()
        {
            try
            {
                using (var client = new System.Net.WebClient())
                using (client.OpenRead("http://clients3.google.com/generate_204"))
                    return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
