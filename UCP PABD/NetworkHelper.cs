using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.NetworkInformation;
using System.Windows.Forms;

namespace UCP_PABD
{
    public static class NetworkHelper
    {
        public static bool IsNetworkAvailable()
        {
            return NetworkInterface.GetIsNetworkAvailable();
        }

        public static bool EnsureNetworkAvailable(Form currentForm)
        {
            if (!IsNetworkAvailable())
            {
                MessageBox.Show("Tidak ada koneksi jaringan.\nSilakan sambungkan ke Wi-Fi atau internet.",
                                "Koneksi Terputus",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);

                currentForm.Close(); // Tutup form jika offline
                return false;
            }
            return true;
        }
    }
}

