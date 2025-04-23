using System;
using System.Windows.Forms;

namespace UCP_PABD
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

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
