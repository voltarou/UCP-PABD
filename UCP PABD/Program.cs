using System;
using System.Windows.Forms;

namespace UCP_PABD
{
    static class Program
    {
        /// <summary>
        /// Titik masuk utama untuk aplikasi.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormCustomer()); // Ganti nama form kalau form utamanya berbeda
        }
    }
}
