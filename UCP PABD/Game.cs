using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient; // Pastikan namespace ini ada
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace UCP_PABD
{
    public partial class Game : Form
    {
        Koneksi Kn = new Koneksi(); // Membuat instance dari kelas Koneksi
        private string strKonek; // Variabel untuk menyimpan string koneksi

        public Game()
        {
            InitializeComponent();
            this.Load += Game_Load;
            strKonek = Kn.connectionStirng(); // Mendapatkan string koneksi dari kelas Koneksi
            // Pastikan NetworkHelper.EnsureNetworkAvailable ada dan berfungsi dengan baik
            if (!NetworkHelper.EnsureNetworkAvailable(this)) return;
        }

        private void Tambah_Click(object sender, EventArgs e)
        {
            if (!NetworkHelper.EnsureNetworkAvailable(this)) return;
            string namaGame = NMG1.Text;
            string publisher = PBLSHR.Text;

            if (string.IsNullOrEmpty(namaGame) || string.IsNullOrEmpty(publisher))
            {
                MessageBox.Show("Semua field harus diisi!");
                return;
            }

            string query = "INSERT INTO Games (NamaGame, Publisher) VALUES (@NamaGame, @Publisher)";
            using (var connection = new SqlConnection(strKonek)) // Menggunakan strKonek
            {
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@NamaGame", namaGame);
                    command.Parameters.AddWithValue("@Publisher", publisher);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                        MessageBox.Show("Data berhasil ditambahkan.");
                        RefreshDataGridView();
                    }
                    catch (Exception ex)
                    {
                        // *** BAGIAN INI YANG PENTING UNTUK MENANGANI DUPLIKASI ***
                        // Memeriksa apakah exception adalah SqlException dan nomor errornya 2627 (Unique Key Violation)
                        if (ex is SqlException sqlEx && sqlEx.Number == 2627)
                        {
                            MessageBox.Show("Nama game ini sudah ada. Mohon masukkan nama game yang berbeda.");
                        }
                        else
                        {
                            // Untuk error lainnya, tampilkan pesan error asli
                            MessageBox.Show($"Terjadi kesalahan: {ex.Message}");
                        }
                    }
                }
            }
        }

        private void Hapus_Click(object sender, EventArgs e)
        {
            if (!NetworkHelper.EnsureNetworkAvailable(this)) return;
            if (List.SelectedRows.Count == 0)
            {
                MessageBox.Show("Pilih baris yang ingin dihapus.");
                return;
            }

            var cellValue = List.SelectedRows[0].Cells["ID_Game"].Value;
            if (cellValue == null || !int.TryParse(cellValue.ToString(), out int idGame))
            {
                MessageBox.Show("ID game tidak valid atau tidak ditemukan.");
                return;
            }

            var confirmResult = MessageBox.Show("Apakah Anda yakin ingin menghapus data ini?", "Konfirmasi", MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                string query = "DELETE FROM Games WHERE ID_Game = @ID_Game";
                using (var connection = new SqlConnection(strKonek))
                {
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ID_Game", idGame);

                        try
                        {
                            connection.Open();
                            command.ExecuteNonQuery();
                            MessageBox.Show("Data berhasil dihapus.");
                            RefreshDataGridView();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Terjadi kesalahan: {ex.Message}");
                        }
                    }
                }
            }
        }

        private void Edit_Click(object sender, EventArgs e)
        {
            if (!NetworkHelper.EnsureNetworkAvailable(this)) return;
            if (List.SelectedRows.Count == 0)
            {
                MessageBox.Show("Pilih baris yang ingin diedit.");
                return;
            }

            int idGame = Convert.ToInt32(List.SelectedRows[0].Cells["ID_Game"].Value);
            string namaGame = NMG1.Text;
            string publisher = PBLSHR.Text;

            if (string.IsNullOrEmpty(namaGame) || string.IsNullOrEmpty(publisher))
            {
                MessageBox.Show("Semua field harus diisi!");
                return;
            }

            string query = "UPDATE Games SET NamaGame = @NamaGame, Publisher = @Publisher WHERE ID_Game = @ID_Game";
            using (var connection = new SqlConnection(strKonek))
            {
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID_Game", idGame);
                    command.Parameters.AddWithValue("@NamaGame", namaGame);
                    command.Parameters.AddWithValue("@Publisher", publisher);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                        MessageBox.Show("Data berhasil diperbarui.");
                        RefreshDataGridView();
                    }
                    catch (Exception ex)
                    {
                        // *** BAGIAN INI JUGA PENTING UNTUK MENANGANI DUPLIKASI SAAT EDIT ***
                        // Memeriksa apakah exception adalah SqlException dan nomor errornya 2627 (Unique Key Violation)
                        if (ex is SqlException sqlEx && sqlEx.Number == 2627)
                        {
                            MessageBox.Show("Nama game yang Anda masukkan sudah digunakan oleh game lain. Mohon pilih nama yang berbeda.");
                        }
                        else
                        {
                            // Untuk error lainnya, tampilkan pesan error asli
                            MessageBox.Show($"Terjadi kesalahan: {ex.Message}");
                        }
                    }
                }
            }
        }

        private void List_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = List.Rows[e.RowIndex];
                NMG1.Text = row.Cells["NamaGame"].Value.ToString();
                PBLSHR.Text = row.Cells["Publisher"].Value.ToString();
            }
        }

        private void NMG1_TextChanged(object sender, EventArgs e)
        {
            // Implementasi event jika diperlukan
        }

        private void PBLSHR_TextChanged(object sender, EventArgs e)
        {
            // Implementasi event jika diperlukan
        }

        private void RefreshDataGridView()
        {
            string query = "SELECT ID_Game, NamaGame, Publisher FROM Games";
            using (var connection = new SqlConnection(strKonek))
            {
                using (var command = new SqlCommand(query, connection))
                {
                    using (var adapter = new SqlDataAdapter(command))
                    {
                        var dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        List.DataSource = dataTable;
                    }
                }
            }
        }

        private void prev_Click(object sender, EventArgs e)
        {
            this.Hide();
            All fc = new All();
            fc.ShowDialog();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var confirm = MessageBox.Show(
                "Apakah Anda yakin ingin logout?",
                "Konfirmasi Logout",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                this.Hide();
                using (var l = new Login())
                {
                    l.ShowDialog();
                }
                this.Close();
            }
        }

        private void Game_Load(object sender, EventArgs e)
        {
            RefreshDataGridView();
        }

        private void CMBGAME_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}