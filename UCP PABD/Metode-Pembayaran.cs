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
            string metodeValue = Metode.Text.Trim();
            if (string.IsNullOrWhiteSpace(metodeValue))
            {
                MessageBox.Show("Metode wajib diisi");
                return;
            }
            if (System.Text.RegularExpressions.Regex.IsMatch(metodeValue, @"\d"))
            {
                MessageBox.Show("Metode tidak boleh mengandung angka");
                return;
            }

            using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Sistem_Pembayaran WHERE Metode = @Metode", _conn))
            {
                cmd.Parameters.AddWithValue("@Metode", metodeValue);
                _conn.Open();
                int count = (int)cmd.ExecuteScalar();
                _conn.Close();

                if (count > 0)
                {
                    MessageBox.Show("Metode sudah ada, tidak boleh duplikat");
                    return;
                }
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

            string metodeValue = Metode.Text.Trim();
            if (string.IsNullOrWhiteSpace(metodeValue))
            {
                MessageBox.Show("Metode wajib diisi");
                return;
            }
            if (System.Text.RegularExpressions.Regex.IsMatch(metodeValue, @"\d"))
            {
                MessageBox.Show("Metode tidak boleh mengandung angka");
                return;
            }

            using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Sistem_Pembayaran WHERE Metode = @Metode", _conn))
            {
                cmd.Parameters.AddWithValue("@Metode", metodeValue);
                _conn.Open();
                int count = (int)cmd.ExecuteScalar();
                _conn.Close();

                if (count > 0)
                {
                    MessageBox.Show("Metode sudah ada, tidak boleh duplikat");
                    return;
                }
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
            All tu = new All();
            tu.ShowDialog();

            // Tutup form login setelah form customer ditutup
            this.Close();
        }

        private void Next_Click(object sender, EventArgs e)
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
