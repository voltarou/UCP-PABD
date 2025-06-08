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
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;

namespace UCP_PABD
{
    public partial class Transaksi : Form
    {
        public Transaksi()
        {
            InitializeComponent();
            this.Load += Transaksi_Load;
            LoadComboBoxData();
            // Aktifkan MultiSelect pada DataGridView Anda di properti designer atau di sini
            DGVTransaksi.MultiSelect = true;
            DGVTransaksi.SelectionMode = DataGridViewSelectionMode.FullRowSelect; // Pastikan mode seleksi adalah seluruh baris
        }

        private void Prev_Click(object sender, EventArgs e)
        {
            this.Hide();

            All mp = new All();
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
                this.Hide();        // sembunyikan form sekarang
                using (var l = new Login())   // tampilkan form login
                {
                    l.ShowDialog();
                }
                this.Close();       // tutup form setelah login ditutup
            }
        }

        private void LoadTransaksiData()
        {
            string query = "SELECT t.ID_Transaksi, t.ID_Customer, t.ID_Paket, t.ID_Pembayaran, t.Status, t.TanggalTransaksi FROM Transaksi t";

            System.Data.DataTable dt = ExecuteQuery(query);

            foreach (DataRow row in dt.Rows)
            {
                bool isEmpty = false;
                bool isInvalid = false;

                // Cek ID_Customer
                if (row["ID_Customer"] == DBNull.Value)
                {
                    isEmpty = true;
                }
                else if (!ValidateID(Convert.ToInt32(row["ID_Customer"]), "Customer"))
                {
                    isInvalid = true;
                }

                // Cek ID_Paket
                if (row["ID_Paket"] == DBNull.Value)
                {
                    isEmpty = true;
                }
                else if (!ValidateID(Convert.ToInt32(row["ID_Paket"]), "Paket_TopUp"))
                {
                    isInvalid = true;
                }

                // Cek ID_Pembayaran
                if (row["ID_Pembayaran"] == DBNull.Value)
                {
                    isEmpty = true;
                }
                else if (!ValidateID(Convert.ToInt32(row["ID_Pembayaran"]), "Sistem_Pembayaran"))
                {
                    isInvalid = true;
                }

                // Tentukan status berdasar pengecekan
                if (isEmpty)
                {
                    row["Status"] = "Pending";
                }
                else if (isInvalid)
                {
                    row["Status"] = "Gagal";
                }
                else
                {
                    row["Status"] = "Sukses";
                }
            }

            DGVTransaksi.DataSource = dt;
        }

        // Execute query and return DataTable
        private System.Data.DataTable ExecuteQuery(string query)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

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
            string status = "Sukses";
            object idCustomer = DBNull.Value;
            object idPaket = DBNull.Value;
            object idPembayaran = DBNull.Value;

            bool isEmpty = false;
            bool isInvalid = false;

            // Validasi ID Customer
            if (!string.IsNullOrWhiteSpace(Idcust.Text) && int.TryParse(Idcust.Text, out int custId))
            {
                if (ValidateID(custId, "Customer")) idCustomer = custId;
                else isInvalid = true;
            }
            else
            {
                isEmpty = true;
            }

            // Validasi ID Paket
            if (!string.IsNullOrWhiteSpace(Idpaket.Text) && int.TryParse(Idpaket.Text, out int paketId))
            {
                if (ValidateID(paketId, "Paket_TopUp")) idPaket = paketId;
                else isInvalid = true;
            }
            else
            {
                isEmpty = true;
            }

            // Validasi ID Pembayaran
            if (!string.IsNullOrWhiteSpace(Idpembayaran.Text) && int.TryParse(Idpembayaran.Text, out int bayarId))
            {
                if (ValidateID(bayarId, "Sistem_Pembayaran")) idPembayaran = bayarId;
                else isInvalid = true;
            }
            else
            {
                isEmpty = true;
            }

            // Tentukan status akhir
            if (isEmpty)
                status = "Pending";
            else if (isInvalid)
                status = "Gagal";

            string query = "INSERT INTO Transaksi (ID_Customer, ID_Paket, ID_Pembayaran, Status) " +
                            "VALUES (@ID_Customer, @ID_Paket, @ID_Pembayaran, @Status)";

            using (SqlConnection con = new SqlConnection("Data Source=VOLTAROU;Initial Catalog=TopUpGameOL;Integrated Security=True;"))
            {
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@ID_Customer", idCustomer);
                cmd.Parameters.AddWithValue("@ID_Paket", idPaket);
                cmd.Parameters.AddWithValue("@ID_Pembayaran", idPembayaran);
                cmd.Parameters.AddWithValue("@Status", status);

                con.Open();
                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("Transaksi berhasil ditambahkan dengan status: " + status, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadTransaksiData();
            ClearFields(); // Panggil fungsi untuk membersihkan field setelah menambah data
        }

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

                string status = "Sukses";
                object idCustomer = DBNull.Value;
                object idPaket = DBNull.Value;
                object idPembayaran = DBNull.Value;
                bool isInvalid = false;
                bool isEmpty = false;

                if (!string.IsNullOrWhiteSpace(Idcust.Text))
                {
                    // Pastikan input adalah angka sebelum parsing
                    if (int.TryParse(Idcust.Text, out int parsedIdCust))
                    {
                        idCustomer = parsedIdCust;
                        if (!ValidateID((int)idCustomer, "Customer")) isInvalid = true;
                    }
                    else
                    {
                        MessageBox.Show("ID Customer harus berupa angka.", "Error Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return; // Hentikan proses edit jika input tidak valid
                    }
                }
                else
                {
                    isEmpty = true;
                }

                if (!string.IsNullOrWhiteSpace(Idpaket.Text))
                {
                    // Pastikan input adalah angka sebelum parsing
                    if (int.TryParse(Idpaket.Text, out int parsedIdPaket))
                    {
                        idPaket = parsedIdPaket;
                        if (!ValidateID((int)idPaket, "Paket_TopUp")) isInvalid = true;
                    }
                    else
                    {
                        MessageBox.Show("ID Paket harus berupa angka.", "Error Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return; // Hentikan proses edit jika input tidak valid
                    }
                }
                else
                {
                    isEmpty = true;
                }

                if (!string.IsNullOrWhiteSpace(Idpembayaran.Text))
                {
                    // Pastikan input adalah angka sebelum parsing
                    if (int.TryParse(Idpembayaran.Text, out int parsedIdPembayaran))
                    {
                        idPembayaran = parsedIdPembayaran;
                        if (!ValidateID((int)idPembayaran, "Sistem_Pembayaran")) isInvalid = true;
                    }
                    else
                    {
                        MessageBox.Show("ID Pembayaran harus berupa angka.", "Error Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return; // Hentikan proses edit jika input tidak valid
                    }
                }
                else
                {
                    isEmpty = true;
                }

                // Tentukan status akhir
                if (isEmpty) status = "Pending";
                else if (isInvalid) status = "Gagal";

                // Tindakan khusus berdasarkan status
                if (status == "Pending")
                {
                    MessageBox.Show("Data masih ada yang kosong. Status akan di-set menjadi Pending.", "Status Pending", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (status == "Gagal")
                {
                    MessageBox.Show("Data tidak valid ditemukan. Status akan di-set menjadi Gagal.", "Status Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Data valid. Status di-set menjadi Sukses.", "Status Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                // Update ke database
                string query = "UPDATE Transaksi SET ID_Customer = @ID_Customer, ID_Paket = @ID_Paket, " +
                               "ID_Pembayaran = @ID_Pembayaran, Status = @Status WHERE ID_Transaksi = @ID_Transaksi";

                using (SqlConnection con = new SqlConnection("Data Source=VOLTAROU;Initial Catalog=TopUpGameOL;Integrated Security=True;"))
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

                LoadTransaksiData();
                ClearFields(); // Panggil fungsi untuk membersihkan field setelah mengedit data
            }
            else
            {
                MessageBox.Show("Silakan pilih transaksi yang akan diedit.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        // Delete multiple selected transactions (Delete_Click)
        private void Delete_Click(object sender, EventArgs e)
        {
            if (DGVTransaksi.SelectedRows.Count > 0) // Memastikan ada baris yang terpilih
            {
                DialogResult confirm;
                if (DGVTransaksi.SelectedRows.Count > 1)
                {
                    confirm = MessageBox.Show(
                        $"Apakah Anda yakin ingin menghapus {DGVTransaksi.SelectedRows.Count} transaksi yang dipilih?",
                        "Konfirmasi Hapus Multiple",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning);
                }
                else
                {
                    confirm = MessageBox.Show(
                        "Apakah Anda yakin ingin menghapus transaksi ini?",
                        "Konfirmasi Hapus",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning);
                }


                if (confirm == DialogResult.Yes)
                {
                    using (SqlConnection con = new SqlConnection("Data Source=VOLTAROU;Initial Catalog=TopUpGameOL;Integrated Security=True;"))
                    {
                        con.Open();
                        SqlTransaction transaction = con.BeginTransaction(); // Mulai transaksi

                        try
                        {
                            foreach (DataGridViewRow row in DGVTransaksi.SelectedRows)
                            {
                                // Pastikan baris bukan baris kosong "New Row"
                                if (!row.IsNewRow)
                                {
                                    int idTransaksi = Convert.ToInt32(row.Cells["ID_Transaksi"].Value);

                                    // Query SQL untuk menghapus data
                                    string query = "DELETE FROM Transaksi WHERE ID_Transaksi = @ID_Transaksi";

                                    SqlCommand cmd = new SqlCommand(query, con, transaction); // Gunakan transaksi
                                    cmd.Parameters.AddWithValue("@ID_Transaksi", idTransaksi); // Menambahkan parameter ID_Transaksi
                                    cmd.ExecuteNonQuery(); // Menjalankan perintah DELETE
                                }
                            }
                            transaction.Commit(); // Komit transaksi jika semua berhasil
                            MessageBox.Show($"{DGVTransaksi.SelectedRows.Count} transaksi berhasil dihapus!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback(); // Rollback transaksi jika ada kesalahan
                            MessageBox.Show("Gagal menghapus transaksi: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }

                    LoadTransaksiData(); // Memuat ulang data untuk menampilkan perubahan
                    ClearFields(); // Membersihkan bidang input setelah penghapusan
                }
            }
            else
            {
                // Memberi tahu pengguna jika tidak ada baris yang dipilih
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

        private void Idcust_TextChanged(object sender, EventArgs e)
        {

        }

        private void ExportToExcel(DataGridView dgv, string filePath)
        {
            Excel.Application excelApp = null;
            Excel.Workbook workbook = null;
            Excel.Worksheet worksheet = null;

            try
            {
                excelApp = new Excel.Application();
                workbook = excelApp.Workbooks.Add(Type.Missing);
                worksheet = (Excel.Worksheet)workbook.Sheets[1];

                // Tulis header kolom
                for (int i = 0; i < dgv.Columns.Count; i++)
                {
                    worksheet.Cells[1, i + 1] = dgv.Columns[i].HeaderText;
                }

                // Tulis hanya baris yang terseleksi
                int rowIndex = 2;
                foreach (DataGridViewRow row in dgv.SelectedRows)
                {
                    if (!row.IsNewRow)
                    {
                        for (int j = 0; j < dgv.Columns.Count; j++)
                        {
                            worksheet.Cells[rowIndex, j + 1] = row.Cells[j].Value?.ToString() ?? "";
                        }
                        rowIndex++;
                    }
                }

                workbook.SaveAs(filePath);
                workbook.Close(false);
                excelApp.Quit();

                MessageBox.Show("Export ke Excel berhasil!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal export ke Excel: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (worksheet != null) Marshal.ReleaseComObject(worksheet);
                if (workbook != null) Marshal.ReleaseComObject(workbook);
                if (excelApp != null) Marshal.ReleaseComObject(excelApp);

                worksheet = null;
                workbook = null;
                excelApp = null;

                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }


        private void ExportToPdf(DataGridView dgv, string filePath)
        {
            Document pdfDoc = new Document(PageSize.A4, 10, 10, 10, 10);
            PdfWriter.GetInstance(pdfDoc, new FileStream(filePath, FileMode.Create));
            pdfDoc.Open();

            PdfPTable table = new PdfPTable(dgv.Columns.Count);

            // Header kolom
            foreach (DataGridViewColumn column in dgv.Columns)
            {
                table.AddCell(new Phrase(column.HeaderText));
            }

            // Data baris hanya dari SelectedRows
            foreach (DataGridViewRow row in dgv.SelectedRows)
            {
                if (!row.IsNewRow)
                {
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        table.AddCell(new Phrase(cell.Value?.ToString() ?? ""));
                    }
                }
            }

            pdfDoc.Add(table);
            pdfDoc.Close();

            MessageBox.Show("Export ke PDF berhasil!");
        }


        private void Export_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Excel Files (*.xlsx)|*.xlsx|PDF Files (*.pdf)|*.pdf",
                Title = "Export Data Transaksi",
                FileName = "DataTransaksi"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = saveFileDialog.FileName;

                if (filePath.EndsWith(".xlsx"))
                {
                    ExportToExcel(DGVTransaksi, filePath);
                }
                else if (filePath.EndsWith(".pdf"))
                {
                    ExportToPdf(DGVTransaksi, filePath);
                }
            }
        }


        private void LoadComboBoxData()
        {
            string connectionString = "Data Source=VOLTAROU;Initial Catalog=TopUpGameOL;Integrated Security=True;";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                // Load Customer IDs
                SqlCommand cmdCustomer = new SqlCommand("SELECT ID_Customer FROM Customer", con);
                SqlDataReader readerCustomer = cmdCustomer.ExecuteReader();
                CmbCust.Items.Clear(); // Bersihkan item lama sebelum menambahkan yang baru
                while (readerCustomer.Read())
                {
                    CmbCust.Items.Add(readerCustomer["ID_Customer"].ToString());
                }
                readerCustomer.Close();

                // Load Paket IDs (ganti 'Games' jika ID Paket Anda ada di tabel lain)
                SqlCommand cmdPaket = new SqlCommand("SELECT ID_Paket FROM Paket_TopUp", con); // Mengambil dari Paket_TopUp
                SqlDataReader readerPaket = cmdPaket.ExecuteReader();
                CmbGame.Items.Clear(); // Bersihkan item lama
                while (readerPaket.Read())
                {
                    CmbGame.Items.Add(readerPaket["ID_Paket"].ToString());
                }
                readerPaket.Close();

                // Load Payment Method IDs
                SqlCommand cmdPayment = new SqlCommand("SELECT ID_Pembayaran FROM Sistem_Pembayaran", con);
                SqlDataReader readerPayment = cmdPayment.ExecuteReader();
                CmbPembayaran.Items.Clear(); // Bersihkan item lama
                while (readerPayment.Read())
                {
                    CmbPembayaran.Items.Add(readerPayment["ID_Pembayaran"].ToString());
                }
                readerPayment.Close();
            }
        }

        private void CmbCust_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Ketika item dipilih di cmbCust, Anda bisa memperbarui Idcust.Text jika masih membutuhkannya
            // Namun, lebih disarankan untuk langsung menggunakan nilai dari CmbCust.SelectedItem di Tambah_Click atau Edit_Click
            // jika Idcust adalah TextBox. Jika Idcust adalah nama properti ComboBox, maka tidak perlu baris ini.
            if (CmbCust.SelectedItem != null)
            {
                // Contoh jika Anda masih ingin mengisi Idcust (TextBox) saat ComboBox dipilih
                // Idcust.Text = CmbCust.SelectedItem.ToString();
            }
        }

        private void CmbGame_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CmbGame.SelectedItem != null)
            {
                // Idpaket.Text = CmbGame.SelectedItem.ToString();
            }
        }

        private void CmbPembayaran_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CmbPembayaran.SelectedItem != null)
            {
                // Idpembayaran.Text = CmbPembayaran.SelectedItem.ToString();
            }
        }

        /// <summary>
        /// Membersihkan semua field input (TextBoxes) pada form.
        /// </summary>
        private void ClearFields()
        {
            Idcust.Text = "";
            Idpaket.Text = "";
            Idpembayaran.Text = "";
            // Jika ada ComboBox yang perlu dibersihkan, tambahkan di sini
            CmbCust.SelectedIndex = -1; // Menghapus pilihan yang sedang dipilih
            CmbGame.SelectedIndex = -1;
            CmbPembayaran.SelectedIndex = -1;
        }
    }
}