using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UCP_PABD
{
    public partial class All : Form
    {
        Koneksi Kn = new Koneksi(); // Membuat instance dari kelas Koneksi
        private string strKonek; // Variabel untuk menyimpan string koneksi
        public All()
        {
            InitializeComponent();
            if (!NetworkHelper.EnsureNetworkAvailable(this)) return;

        }

        private void Customer_Click(object sender, EventArgs e)
        {
            this.Hide(); // Sembunyikan form login

            // Tampilkan form customer
            FormCustomer c = new FormCustomer();
            c.ShowDialog();

            // Tutup form login setelah form customer ditutup
            this.Close();
        }

        private void Game_Click(object sender, EventArgs e)
        {
            this.Hide(); // Sembunyikan form login

            // Tampilkan form customer
            Game g = new Game();
            g.ShowDialog();

            // Tutup form login setelah form customer ditutup
            this.Close();
        }

        private void TOPUP_Click(object sender, EventArgs e)
        {
            this.Hide(); // Sembunyikan form login

            // Tampilkan form customer
            Top_UP t = new Top_UP();
            t.ShowDialog();

            // Tutup form login setelah form customer ditutup
            this.Close();
        }

        private void Metode_Click(object sender, EventArgs e)
        {
            this.Hide(); // Sembunyikan form login

            // Tampilkan form customer
            Metode_Pembayaran m = new Metode_Pembayaran();
            m.ShowDialog();

            // Tutup form login setelah form customer ditutup
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide(); // Sembunyikan form login

            // Tampilkan form customer
            Transaksi t = new Transaksi();
            t.ShowDialog();

            // Tutup form login setelah form customer ditutup
            this.Close();
        }

        private void LogOut_Click(object sender, EventArgs e)
        {
            var confirm = MessageBox.Show(
                "Apakah Anda yakin ingin logout?",
                "Konfirmasi Logout",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                this.Hide();          // sembunyikan form sekarang
                using (var l = new Login())   // tampilkan form login
                {
                    l.ShowDialog();
                }
                this.Close();
            }
        }
    }
}
