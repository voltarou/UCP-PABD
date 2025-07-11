﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UCP_PABD
{
    public partial class Login : Form
    {
        Koneksi Kn = new Koneksi(); // Membuat instance dari kelas Koneksi
        private string strKonek; // Variabel untuk menyimpan string koneksi
        public Login()
        {
            InitializeComponent();
            strKonek = Kn.connectionStirng();
            if (!NetworkHelper.EnsureNetworkAvailable(this)) return;
        }

        private void lblUsername_Click(object sender, EventArgs e)
        {

        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblPassword_Click(object sender, EventArgs e)
        {

        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;
            txtPassword.UseSystemPasswordChar = true;
            txtPassword.UseSystemPasswordChar = true;
            // Validasi panjang password
            if (password.Length <= 8)
            {
                MessageBox.Show("Password harus lebih dari 8 karakter!", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Ganti connection string sesuai konfigurasi database kamu
            string strKonek = "Data Source=.;Initial Catalog=TopUpGameOL;Integrated Security=True"; // atau gunakan User ID & Password jika pakai SQL Auth

            using (SqlConnection conn = new SqlConnection(strKonek))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT COUNT(*) FROM UserLogin WHERE Username = @username AND Password = @password";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@password", password);

                        int count = (int)cmd.ExecuteScalar();

                        if (count > 0)
                        {
                            MessageBox.Show("Login berhasil!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Hide();

                            All fc = new All();
                            fc.ShowDialog();

                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Username atau password salah!", "Login Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Terjadi kesalahan saat koneksi ke database:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private bool passwordShown = false;


        private void Hide_Click(object sender, EventArgs e)
        {
            passwordShown = !passwordShown;
            txtPassword.UseSystemPasswordChar = !passwordShown;

            // Optional: Ubah teks tombol sesuai kondisi
            Hide.Text = passwordShown ? "Show" : "Hide";
        }
    }
}

