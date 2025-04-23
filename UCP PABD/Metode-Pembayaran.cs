using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace UCP_PABD
{
    public partial class Metode_Pembayaran : Form
    {
        private readonly SqlConnection _conn = new SqlConnection("Server=VOLTAROU;Database=TopUpGameOL;Trusted_Connection=True;TrustServerCertificate=True");
        private SqlDataAdapter _adapter;
        private DataTable _table;
        private int _selectedId = -1;

        public Metode_Pembayaran()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                _adapter = new SqlDataAdapter("SELECT * FROM Sistem_Pembayaran", _conn);
                _table = new DataTable();
                _adapter.Fill(_table);
                DGVMP.DataSource = _table;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Gagal memuat data: {ex.Message}");
            }
        }

        private void ClearInputs()
        {
            Metode.Clear();
            _selectedId = -1;
            Tambah.Enabled = true;
        }

        private void Metode_TextChanged(object sender, EventArgs e)
        {
            if (_table == null) return;
            string safe = Metode.Text.Replace("'", "''");
            _table.DefaultView.RowFilter = $"Metode LIKE '%{safe}%'";
        }

        private void Tambah_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Metode.Text))
            {
                MessageBox.Show("Metode wajib diisi");
                return;
            }

            using (SqlCommand cmd = new SqlCommand("INSERT INTO Sistem_Pembayaran (Metode) VALUES (@Metode)", _conn))
            {
                cmd.Parameters.AddWithValue("@Metode", Metode.Text);
                _conn.Open();
                cmd.ExecuteNonQuery();
                _conn.Close();
            }
            LoadData();
            ClearInputs();
            MessageBox.Show("Data berhasil ditambahkan!");  // Success notification
        }

        private void Edit_Click(object sender, EventArgs e)
        {
            if (_selectedId == -1)
            {
                MessageBox.Show("Pilih data yang akan diedit");
                return;
            }

            using (SqlCommand cmd = new SqlCommand("UPDATE Sistem_Pembayaran SET Metode=@Metode WHERE ID_Pembayaran=@ID", _conn))
            {
                cmd.Parameters.AddWithValue("@Metode", Metode.Text);
                cmd.Parameters.AddWithValue("@ID", _selectedId);
                _conn.Open();
                cmd.ExecuteNonQuery();
                _conn.Close();
            }
            LoadData();
            ClearInputs();
            MessageBox.Show("Data berhasil diperbarui!");  // Success notification
        }

        private void Hapus_Click(object sender, EventArgs e)
        {
            if (_selectedId == -1)
            {
                MessageBox.Show("Pilih data yang akan dihapus");
                return;
            }

            if (MessageBox.Show("Yakin hapus?", "Konfirmasi", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                using (SqlCommand cmd = new SqlCommand("DELETE FROM Sistem_Pembayaran WHERE ID_Pembayaran=@ID", _conn))
                {
                    cmd.Parameters.AddWithValue("@ID", _selectedId);
                    _conn.Open();
                    cmd.ExecuteNonQuery();
                    _conn.Close();
                }
                LoadData();
                ClearInputs();
                MessageBox.Show("Data berhasil dihapus!");  // Success notification
            }
        }

        private void DGVMP_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = DGVMP.Rows[e.RowIndex];
                _selectedId = Convert.ToInt32(row.Cells["ID_Pembayaran"].Value);
                Metode.Text = row.Cells["Metode"].Value.ToString();
                Tambah.Enabled = false;
            }
        }

        private void Prev_Click(object sender, EventArgs e)
        {
            this.Hide(); // Sembunyikan form login

            // Tampilkan form customer
            Top_UP tu = new Top_UP();
            tu.ShowDialog();

            // Tutup form login setelah form customer ditutup
            this.Close();
        }

        private void Next_Click(object sender, EventArgs e)
        {
            this.Hide(); // Sembunyikan form login

            // Tampilkan form customer
            Transaksi t = new Transaksi();
            t.ShowDialog();

            // Tutup form login setelah form customer ditutup
            this.Close();
        }
    }
}
