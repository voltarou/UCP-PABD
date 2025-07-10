using System;
using System.Net.NetworkInformation;
using System.Windows.Forms;

namespace UCP_PABD
{
    static class Program
    {
        // Method untuk cek apakah jaringan tersedia (Wi-Fi, LAN, dll)
        public static bool IsNetworkAvailable()
        {
            return NetworkInterface.GetIsNetworkAvailable();
        }

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Cek koneksi jaringan dulu sebelum mulai aplikasi
            if (!IsNetworkAvailable())
            {
                MessageBox.Show("Tidak ada koneksi jaringan.\nSilakan sambungkan ke Wi-Fi atau LAN untuk menggunakan aplikasi.",
                                "Koneksi Terputus",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return; // hentikan aplikasi
            }

            // Lanjut ke login
            Login loginForm = new Login();
            if (loginForm.ShowDialog() == DialogResult.OK)
            {
                FormCustomer customerForm = new FormCustomer();
                if (customerForm.ShowDialog() == DialogResult.OK)
                {
                    Application.Run(new Game()); // Game jadi form utama
                }
                else
                {
                    Application.Exit();
                }
            }
            else
            {
                Application.Exit();
            }
        }
    }
}
