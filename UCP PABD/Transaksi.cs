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
    public partial class Transaksi : Form
    {
        public Transaksi()
        {
            InitializeComponent();
        }

        private void Prev_Click(object sender, EventArgs e)
        {
            this.Hide();

            Metode_Pembayaran mp = new Metode_Pembayaran();
            mp.ShowDialog();

            this.Close();
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
                this.Hide();          // sembunyikan form sekarang
                using (var l = new Login())   // tampilkan form login
                {
                    l.ShowDialog();
                }
                this.Close();         // tutup form setelah login ditutup
            }
        }

        private void LoadTransaksiData()
        {
            string query = "SELECT t.ID_Transaksi, c.ID_Customer, p.ID_Paket, s.ID_Pembayaran, t.Status, t.TanggalTransaksi " +
                           "FROM Transaksi t " +
                           "LEFT JOIN Customer c ON t.ID_Customer = c.ID_Customer " +
                           "LEFT JOIN Paket_TopUp p ON t.ID_Paket = p.ID_Paket " +
                           "LEFT JOIN Sistem_Pembayaran s ON t.ID_Pembayaran = s.ID_Pembayaran";

            DataTable dt = ExecuteQuery(query);

            // After loading the data, check for invalid foreign key values
            foreach (DataRow row in dt.Rows)
            {
                if (row["ID_Customer"] == DBNull.Value || row["ID_Paket"] == DBNull.Value || row["ID_Pembayaran"] == DBNull.Value)
                {
                    // Mark the status as "Gagal" if any foreign key is missing
                    row["Status"] = "Gagal";
                }
            }

            DGVTransaksi.DataSource = dt; // Bind the data to the DataGridView
        }



        // Execute query and return DataTable
        private DataTable ExecuteQuery(string query)
        {
            DataTable dt = new DataTable();
            string connectionString = "Data Source=VOLTAROU;Initial Catalog=TopUpGameOL;Integrated Security=True;"; // Updated connection string
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                da.Fill(dt);
            }
            return dt;
        }

      
        private void Tambah_Click(object sender, EventArgs e)
        {
            int idCustomer = int.Parse(Idcust.Text); // Get ID_Customer from input
            int idPaket = int.Parse(Idpaket.Text); // Get ID_Paket from input
            int idPembayaran = int.Parse(Idpembayaran.Text); // Get ID_Pembayaran from input
            string status = "Pending"; // Default status

            // Validate IDs
            if (!ValidateID(idCustomer, "Customer") || !ValidateID(idPaket, "Paket_TopUp") || !ValidateID(idPembayaran, "Sistem_Pembayaran"))
            {
                status = "Gagal"; // Set status to "Gagal" if any ID is invalid
            }

            // Insert the transaction into the database
            string query = "INSERT INTO Transaksi (ID_Customer, ID_Paket, ID_Pembayaran, Status) " +
                           "VALUES (@ID_Customer, @ID_Paket, @ID_Pembayaran, @Status)";

            using (SqlConnection con = new SqlConnection("Data Source=VOLTAROU;Initial Catalog=TopUpGameOL;Integrated Security=True;")) // Updated connection string
            {
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@ID_Customer", idCustomer);
                cmd.Parameters.AddWithValue("@ID_Paket", idPaket);
                cmd.Parameters.AddWithValue("@ID_Pembayaran", idPembayaran);
                cmd.Parameters.AddWithValue("@Status", status);

                con.Open();
                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("Transaksi berhasil ditambahkan!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Reload the DataGridView to show the updated data
            LoadTransaksiData(); // Refresh the DataGridView with the latest data, including the "Gagal" transaction
        }


        // Validate if the ID exists in the respective table
        // Validate if the ID exists in the respective table
        private bool ValidateID(int id, string tableName)
        {
            string columnName = string.Empty;

            // Set the correct column name based on the table
            switch (tableName)
            {
                case "Customer":
                    columnName = "ID_Customer";
                    break;
                case "Paket_TopUp":
                    columnName = "ID_Paket";
                    break;
                case "Sistem_Pembayaran":
                    columnName = "ID_Pembayaran";
                    break;
                default:
                    return false; // Invalid table name
            }

            string query = $"SELECT COUNT(*) FROM {tableName} WHERE {columnName} = @ID";

            using (SqlConnection con = new SqlConnection("Data Source=VOLTAROU;Initial Catalog=TopUpGameOL;Integrated Security=True;"))
            {
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@ID", id);
                con.Open();
                int count = (int)cmd.ExecuteScalar();
                return count > 0; // Return true if the ID exists, false otherwise
            }
        }


        // Edit an existing transaction (Edit_Click)
        private void Edit_Click(object sender, EventArgs e)
        {
            if (DGVTransaksi.SelectedRows.Count > 0)
            {
                int idTransaksi = Convert.ToInt32(DGVTransaksi.SelectedRows[0].Cells["ID_Transaksi"].Value);

                int idCustomer = int.Parse(Idcust.Text); // New ID_Customer
                int idPaket = int.Parse(Idpaket.Text); // New ID_Paket
                int idPembayaran = int.Parse(Idpembayaran.Text); // New ID_Pembayaran
                string status = "Sukses"; // Default status

                // Validate IDs
                if (!ValidateID(idCustomer, "Customer") || !ValidateID(idPaket, "Paket_TopUp") || !ValidateID(idPembayaran, "Sistem_Pembayaran"))
                {
                    status = "Gagal"; // Set status to "Gagal" if any ID is invalid
                }

                string query = "UPDATE Transaksi SET ID_Customer = @ID_Customer, ID_Paket = @ID_Paket, " +
                               "ID_Pembayaran = @ID_Pembayaran, Status = @Status WHERE ID_Transaksi = @ID_Transaksi";

                using (SqlConnection con = new SqlConnection("Data Source=VOLTAROU;Initial Catalog=TopUpGameOL;Integrated Security=True;")) // Updated connection string
                {
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@ID_Customer", idCustomer);
                    cmd.Parameters.AddWithValue("@ID_Paket", idPaket);
                    cmd.Parameters.AddWithValue("@ID_Pembayaran", idPembayaran);
                    cmd.Parameters.AddWithValue("@Status", status);
                    cmd.Parameters.AddWithValue("@ID_Transaksi", idTransaksi);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Transaksi berhasil diperbarui!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadTransaksiData(); // Refresh the DataGridView with the latest data
            }
            else
            {
                MessageBox.Show("Silakan pilih transaksi yang akan diedit.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        // Delete a selected transaction (Delete_Click)
        private void Delete_Click(object sender, EventArgs e)
        {
            if (DGVTransaksi.SelectedRows.Count > 0)
            {
                int idTransaksi = Convert.ToInt32(DGVTransaksi.SelectedRows[0].Cells["ID_Transaksi"].Value);

                var confirm = MessageBox.Show(
                    "Apakah Anda yakin ingin menghapus transaksi ini?",
                    "Konfirmasi Hapus",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (confirm == DialogResult.Yes)
                {
                    string query = "DELETE FROM Transaksi WHERE ID_Transaksi = @ID_Transaksi";

                    using (SqlConnection con = new SqlConnection("Data Source=VOLTAROU;Initial Catalog=TopUpGameOL;Integrated Security=True;")) // Updated connection string
                    {
                        SqlCommand cmd = new SqlCommand(query, con);
                        cmd.Parameters.AddWithValue("@ID_Transaksi", idTransaksi);

                        con.Open();
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Transaksi berhasil dihapus!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadTransaksiData(); // Refresh the DataGridView with the latest data
                }
            }
            else
            {
                MessageBox.Show("Silakan pilih transaksi yang akan dihapus.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Handle DataGridView row click to load selected transaction data for editing
        private void DGVTransaksi_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = DGVTransaksi.Rows[e.RowIndex];
                Idcust.Text = row.Cells["ID_Customer"].Value.ToString();
                Idpaket.Text = row.Cells["ID_Paket"].Value.ToString();
                Idpembayaran.Text = row.Cells["ID_Pembayaran"].Value.ToString();
            }
        }

        // Form load event, load data into the DataGridView
        private void Transaksi_Load(object sender, EventArgs e)
        {
            LoadTransaksiData();
        }

        // Event handler for Idpembayaran TextChanged
        private void Idpembayaran_TextChanged(object sender, EventArgs e)
        {
            // Add logic to handle text changes if necessary
        }

        // Event handler for Idpaket TextChanged
        private void Idpaket_TextChanged(object sender, EventArgs e)
        {
            // Add logic to handle text changes if necessary
        }

        // Event handler for Hapus (Delete) button click
        private void Hapus_Click(object sender, EventArgs e)
        {
            Delete_Click(sender, e); // Call the Delete_Click method
        }
    }
}
