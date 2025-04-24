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
        public Top_UP()
        {
            InitializeComponent();
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
            string namaPaket = NamaPaket.Text;
            if (!decimal.TryParse(Hargaa.Text, out decimal harga))
            {
                MessageBox.Show("Harga harus berupa angka.");
                return;
            }

            // Validasi ID Game yang dimasukkan langsung di TextBox
            if (!int.TryParse(ID_Game.Text, out int idGame))
            {
                MessageBox.Show("ID Game harus berupa angka.");
                return;
            }

            string query = "INSERT INTO Paket_TopUp (NamaPaket, Harga, ID_Game) VALUES (@NamaPaket, @Harga, @ID_Game)";
            using (var connection = new SqlConnection("Data Source=VOLTAROU;Initial Catalog=TopUpGameOL;Integrated Security=True;"))
            {
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@NamaPaket", namaPaket);
                    command.Parameters.AddWithValue("@Harga", harga);
                    command.Parameters.AddWithValue("@ID_Game", idGame);  // Menggunakan nilai dari TextBox ID_Game

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                        MessageBox.Show("Data berhasil ditambahkan.");
                        RefreshPaketGrid();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Terjadi kesalahan: {ex.Message}");
                    }
                }
            }
        }

        private void Edit_Click(object sender, EventArgs e)
        {
            if (DGVTOPUUP.SelectedRows.Count == 0)
            {
                MessageBox.Show("Pilih baris yang ingin diedit.");
                return;
            }

            int idPaket = Convert.ToInt32(DGVTOPUUP.SelectedRows[0].Cells["ID_Paket"].Value);
            string namaPaket = NamaPaket.Text;
            if (!decimal.TryParse(Hargaa.Text, out decimal harga))
            {
                MessageBox.Show("Harga harus berupa angka.");
                return;
            }

            // Validasi ID Game yang dimasukkan langsung di TextBox
            if (!int.TryParse(ID_Game.Text, out int idGame))
            {
                MessageBox.Show("ID Game harus berupa angka.");
                return;
            }

            string query = "UPDATE Paket_TopUp SET NamaPaket = @NamaPaket, Harga = @Harga, ID_Game = @ID_Game WHERE ID_Paket = @ID";
            using (var connection = new SqlConnection("Data Source=VOLTAROU;Initial Catalog=TopUpGameOL;Integrated Security=True;"))
            {
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", idPaket);
                    command.Parameters.AddWithValue("@NamaPaket", namaPaket);
                    command.Parameters.AddWithValue("@Harga", harga);
                    command.Parameters.AddWithValue("@ID_Game", idGame);  // Menggunakan nilai dari TextBox ID_Game

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                        MessageBox.Show("Data berhasil diperbarui.");
                        RefreshPaketGrid();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Terjadi kesalahan: {ex.Message}");
                    }
                }
            }
        }


        private void Hapus_Click(object sender, EventArgs e)
        {
            if (DGVTOPUUP.SelectedRows.Count == 0)
            {
                MessageBox.Show("Pilih baris yang ingin dihapus.");
                return;
            }

            int idPaket = Convert.ToInt32(DGVTOPUUP.SelectedRows[0].Cells["ID_Paket"].Value);

            var confirmResult = MessageBox.Show("Yakin ingin menghapus data ini?", "Konfirmasi", MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                string query = "DELETE FROM Paket_TopUp WHERE ID_Paket = @ID";
                using (var connection = new SqlConnection("Data Source=VOLTAROU;Initial Catalog=TopUpGameOL;Integrated Security=True;"))
                {
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ID", idPaket);

                        try
                        {
                            connection.Open();
                            command.ExecuteNonQuery();
                            MessageBox.Show("Data berhasil dihapus.");
                            RefreshPaketGrid();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Terjadi kesalahan: {ex.Message}");
                        }
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
            }
        }

        private void RefreshPaketGrid()
        {
            string query = "SELECT * FROM Paket_TopUp";
            using (var connection = new SqlConnection(
                   "Data Source=VOLTAROU;Initial Catalog=TopUpGameOL;Integrated Security=True;"))
            using (var adapter = new SqlDataAdapter(query, connection))
            {
                var dataTable = new DataTable();
                adapter.Fill(dataTable);

                EnsureIdGameColumn();          // ⬅️ panggil di sini
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
            using (var connection = new SqlConnection("Data Source=VOLTAROU;Initial Catalog=TopUpGameOL;Integrated Security=True;"))
            {
                using (var adapter = new SqlDataAdapter(query, connection))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    CMB.DataSource = dt;
                    CMB.DisplayMember = "NamaGame";
                    CMB.ValueMember = "ID_Game";
                }
            }
        }

        private void CMB_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Prev_Click(object sender, EventArgs e)
        {
            this.Hide(); // Sembunyikan FormCustomer

            // Tampilkan form Game
            Game formGame = new Game();
            formGame.ShowDialog(); // Tunggu hingga form Game ditutup

            this.Close(); // Tutup FormCustomer setelah form Game selesai
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide(); 

           
            Metode_Pembayaran mp = new Metode_Pembayaran();
            mp.ShowDialog();

           
            this.Close();
        }
      
     
    }
}
