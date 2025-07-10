using System;
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
    public partial class Top_UP : Form
    {
        // Tambahkan variable untuk mendeklarasikan class koneksi
        Koneksi Kn = new Koneksi(); // Membuat instance dari kelas Koneksi
        private string strKonek;

        public Top_UP()
        {
            InitializeComponent();
            this.Load += Top_UP_Load;
            // Ubah seluruh konstruktor SqlConnection
            strKonek = Kn.connectionStirng();
            if (!NetworkHelper.EnsureNetworkAvailable(this)) return;
        }

        private void NamaPaket_TextChanged(object sender, EventArgs e)
        {
            // Validasi real-time jika diperlukan
        }

        private void Hargaa_TextChanged(object sender, EventArgs e)
        {
            // Validasi angka jika perlu
        }

        private void ID_Game_TextChanged(object sender, EventArgs e)
        {
            // Sinkronkan isian ID_Game (TextBox) dengan ComboBox CMB
            if (int.TryParse(ID_Game.Text, out int idGame))
            {
                // Jika ID valid, pilih item yang cocok di ComboBox
                CMB.SelectedValue = idGame;
            }
            else
            {
                // Jika teks bukan angka, kosongkan pilihan ComboBox
                CMB.SelectedIndex = -1;
            }
        }

        private void Tambah_Click(object sender, EventArgs e)
        {
            string namaPaket = NamaPaket.Text.Trim();
            if (!NetworkHelper.EnsureNetworkAvailable(this)) return;
            if (string.IsNullOrWhiteSpace(namaPaket))
            {
                MessageBox.Show("Nama Paket tidak boleh kosong.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(Hargaa.Text.Trim(), out decimal harga) || harga <= 0)
            {
                MessageBox.Show("Harga harus berupa angka positif.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(ID_Game.Text.Trim(), out int idGame))
            {
                MessageBox.Show("ID Game harus berupa angka.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Ubah seluruh konstruktor SqlConnection
            using (var connection = new SqlConnection(strKonek))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("usp_InsertPaketTopUp", connection);
                    command.CommandType = CommandType.StoredProcedure; // Penting: Menentukan tipe command

                    command.Parameters.AddWithValue("@NamaPaket", namaPaket);
                    command.Parameters.AddWithValue("@Harga", harga);
                    command.Parameters.AddWithValue("@ID_Game", idGame);

                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        int resultCode = Convert.ToInt32(reader["ResultCode"]);
                        string resultMessage = reader["ResultMessage"].ToString();

                        if (resultCode == 1)
                        {
                            MessageBox.Show(resultMessage, "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            RefreshPaketGrid();
                            // Clear input fields (optional, add a ClearFields() method if needed)
                            NamaPaket.Clear();
                            Hargaa.Clear();
                            ID_Game.Clear();
                            NamaPaket.Focus();
                        }
                        else
                        {
                            MessageBox.Show(resultMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Terjadi kesalahan saat menambahkan data paket: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Edit_Click(object sender, EventArgs e)
        {

            if (!NetworkHelper.EnsureNetworkAvailable(this)) return;
            if (DGVTOPUUP.SelectedRows.Count == 0)
            {
                MessageBox.Show("Pilih baris yang ingin diedit.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idPaket = Convert.ToInt32(DGVTOPUUP.SelectedRows[0].Cells["ID_Paket"].Value);
            string namaPaket = NamaPaket.Text.Trim();
            if (string.IsNullOrWhiteSpace(namaPaket))
            {
                MessageBox.Show("Nama Paket tidak boleh kosong.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(Hargaa.Text.Trim(), out decimal harga) || harga <= 0)
            {
                MessageBox.Show("Harga harus berupa angka positif.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(ID_Game.Text.Trim(), out int idGame))
            {
                MessageBox.Show("ID Game harus berupa angka.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Ubah seluruh konstruktor SqlConnection
            using (var connection = new SqlConnection(strKonek))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("usp_UpdatePaketTopUp", connection);
                    command.CommandType = CommandType.StoredProcedure; // Penting: Menentukan tipe command

                    command.Parameters.AddWithValue("@ID_Paket", idPaket);
                    command.Parameters.AddWithValue("@NamaPaket", namaPaket);
                    command.Parameters.AddWithValue("@Harga", harga);
                    command.Parameters.AddWithValue("@ID_Game", idGame);

                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        int resultCode = Convert.ToInt32(reader["ResultCode"]);
                        string resultMessage = reader["ResultMessage"].ToString();

                        if (resultCode == 1)
                        {
                            MessageBox.Show(resultMessage, "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            RefreshPaketGrid();
                            // Clear input fields (optional)
                            NamaPaket.Clear();
                            Hargaa.Clear();
                            ID_Game.Clear();
                            NamaPaket.Focus();
                        }
                        else
                        {
                            MessageBox.Show(resultMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Terjadi kesalahan saat memperbarui data paket: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Hapus_Click(object sender, EventArgs e)
        {
            if (!NetworkHelper.EnsureNetworkAvailable(this)) return;
            if (DGVTOPUUP.SelectedRows.Count == 0)
            {
                MessageBox.Show("Pilih baris yang ingin dihapus.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idPaket = Convert.ToInt32(DGVTOPUUP.SelectedRows[0].Cells["ID_Paket"].Value);

            var confirmResult = MessageBox.Show("Yakin ingin menghapus data ini?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmResult == DialogResult.Yes)
            {
                // Ubah seluruh konstruktor SqlConnection
                using (var connection = new SqlConnection(strKonek))
                {
                    try
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand("usp_DeletePaketTopUp", connection);
                        command.CommandType = CommandType.StoredProcedure; // Penting: Menentukan tipe command

                        command.Parameters.AddWithValue("@ID_Paket", idPaket);

                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            int resultCode = Convert.ToInt32(reader["ResultCode"]);
                            string resultMessage = reader["ResultMessage"].ToString();

                            if (resultCode == 1)
                            {
                                MessageBox.Show(resultMessage, "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                RefreshPaketGrid();
                                // Clear input fields (optional)
                                NamaPaket.Clear();
                                Hargaa.Clear();
                                ID_Game.Clear();
                                NamaPaket.Focus();
                            }
                            else
                            {
                                MessageBox.Show(resultMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        reader.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Terjadi kesalahan saat menghapus data paket: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void DGVTOPUUP_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = DGVTOPUUP.Rows[e.RowIndex];
                NamaPaket.Text = row.Cells["NamaPaket"].Value.ToString();
                Hargaa.Text = row.Cells["Harga"].Value.ToString();
                ID_Game.Text = row.Cells["ID_Game"].Value.ToString(); // Menambahkan ini untuk mengisi ID_Game
            }
        }

        private void RefreshPaketGrid()
        {
            string query = "SELECT * FROM Paket_TopUp";
            // Ubah seluruh konstruktor SqlConnection
            using (var connection = new SqlConnection(strKonek))
            using (var adapter = new SqlDataAdapter(query, connection))
            {
                var dataTable = new DataTable();
                adapter.Fill(dataTable);

                EnsureIdGameColumn();       // ⬅️ panggil di sini
                DGVTOPUUP.DataSource = dataTable;
            }
        }

        private void EnsureIdGameColumn()
        {
            if (!DGVTOPUUP.Columns.Contains("ID_Game"))
            {
                var col = new DataGridViewTextBoxColumn
                {
                    Name = "ID_Game",
                    HeaderText = "ID Game",
                    DataPropertyName = "ID_Game",
                    ReadOnly = true
                };
                DGVTOPUUP.Columns.Add(col);
            }
        }

        private void Top_UP_Load(object sender, EventArgs e)
        {
            string query = "SELECT ID_Game, NamaGame FROM Games";
            // Ubah seluruh konstruktor SqlConnection
            using (var connection = new SqlConnection(strKonek))
            {
                using (var adapter = new SqlDataAdapter(query, connection))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    CMB.DataSource = dt;
                    CMB.DisplayMember = "NamaGame";
                    CMB.ValueMember = "ID_Game";

                    RefreshPaketGrid();
                }
            }
        }

        private void CMB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CMB.SelectedValue != null)
            {
                ID_Game.Text = CMB.SelectedValue.ToString();
            }
            else
            {
                ID_Game.Clear();
            }
        }

        private void Prev_Click(object sender, EventArgs e)
        {
            this.Hide(); // Sembunyikan FormCustomer

            // Tampilkan form Game
            All formGame = new All();
            formGame.ShowDialog(); // Tunggu hingga form Game ditutup

            this.Close(); // Tutup FormCustomer setelah form Game selesai
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var confirm = MessageBox.Show(
                   "Apakah Anda yakin ingin logout?",
                   "Konfirmasi Logout",
                   MessageBoxButtons.YesNo,
                   MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                this.Hide();        // sembunyikan form sekarang
                using (var l = new Login())   // tampilkan form login
                {
                    l.ShowDialog();
                }
                this.Close();
            }
        }
    }
}