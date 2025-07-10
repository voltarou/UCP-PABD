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
using System.Diagnostics;

namespace UCP_PABD
{
    public partial class Transaksi : Form
    {
        Koneksi Kn = new Koneksi(); // Membuat instance dari kelas Koneksi
        private string strKonek; // Variabel untuk menyimpan string koneksi

        public Transaksi()
        {
            InitializeComponent();
            this.Load += Transaksi_Load;
            DGVTransaksi.MultiSelect = true;
            DGVTransaksi.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            strKonek = Kn.connectionStirng(); // Initialize strKonek FIRST
            LoadComboBoxData(); // Then call LoadComboBoxData()
            if (!NetworkHelper.EnsureNetworkAvailable(this)) return;
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
                this.Hide();
                using (var l = new Login())
                {
                    l.ShowDialog();
                }
                this.Close();
            }
        }

        private void LoadTransaksiData()
        {
            // Query untuk mengambil data transaksi, termasuk nama dari tabel terkait
            // Memastikan alias kolom sesuai dengan nama kolom aktual di tabel master
            string query = @"
                SELECT 
                    t.ID_Transaksi, 
                    t.ID_Customer, 
                    c.Nama AS NamaCustomer, -- Menggunakan c.Nama dari tabel Customer dan alias sebagai NamaCustomer
                    t.ID_Paket, 
                    pt.NamaPaket, -- Menggunakan NamaPaket dari tabel Paket_TopUp
                    t.ID_Pembayaran, 
                    sp.Metode AS MetodePembayaran, -- Menggunakan sp.Metode dari tabel Sistem_Pembayaran dan alias sebagai MetodePembayaran
                    t.Status, 
                    t.TanggalTransaksi 
                FROM Transaksi t
                LEFT JOIN Customer c ON t.ID_Customer = c.ID_Customer
                LEFT JOIN Paket_TopUp pt ON t.ID_Paket = pt.ID_Paket
                LEFT JOIN Sistem_Pembayaran sp ON t.ID_Pembayaran = sp.ID_Pembayaran;
            ";

            System.Data.DataTable dt = ExecuteQuery(query);

            

            DGVTransaksi.DataSource = dt;
        }

        private System.Data.DataTable ExecuteQuery(string query)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            using (SqlConnection con = new SqlConnection(strKonek))
            {
                try
                {
                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(query, con);
                    da.Fill(dt);
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Error database saat mengambil data: " + ex.Message, "Error Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Terjadi kesalahan: " + ex.Message, "Error Umum", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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
            if (!NetworkHelper.EnsureNetworkAvailable(this)) return;
            // Mengambil ID dari ComboBox, jika dipilih
            // Jika ComboBox kosong, fallback ke TextBox
            if (CmbCust.SelectedItem != null && int.TryParse(CmbCust.SelectedItem.ToString(), out int custIdFromCmb))
            {
                idCustomer = custIdFromCmb;
                if (!ValidateID((int)idCustomer, "Customer")) isInvalid = true;
            }
            else if (!string.IsNullOrWhiteSpace(Idcust.Text) && int.TryParse(Idcust.Text, out int custIdFromText))
            {
                idCustomer = custIdFromText;
                if (!ValidateID((int)idCustomer, "Customer")) isInvalid = true;
            }
            else
            {
                isEmpty = true;
            }

            if (CmbGame.SelectedItem != null && int.TryParse(CmbGame.SelectedItem.ToString(), out int paketIdFromCmb))
            {
                idPaket = paketIdFromCmb;
                if (!ValidateID((int)idPaket, "Paket_TopUp")) isInvalid = true;
            }
            else if (!string.IsNullOrWhiteSpace(Idpaket.Text) && int.TryParse(Idpaket.Text, out int paketIdFromText))
            {
                idPaket = paketIdFromText;
                if (!ValidateID((int)idPaket, "Paket_TopUp")) isInvalid = true;
            }
            else
            {
                isEmpty = true;
            }

            if (CmbPembayaran.SelectedItem != null && int.TryParse(CmbPembayaran.SelectedItem.ToString(), out int bayarIdFromCmb))
            {
                idPembayaran = bayarIdFromCmb;
                if (!ValidateID((int)idPembayaran, "Sistem_Pembayaran")) isInvalid = true;
            }
            else if (!string.IsNullOrWhiteSpace(Idpembayaran.Text) && int.TryParse(Idpembayaran.Text, out int bayarIdFromText))
            {
                idPembayaran = bayarIdFromText;
                if (!ValidateID((int)idPembayaran, "Sistem_Pembayaran")) isInvalid = true;
            }
            else
            {
                isEmpty = true;
            }

            if (isEmpty)
                status = "Pending";
            else if (isInvalid)
                status = "Gagal";
            else
                status = "Sukses"; // Pastikan status menjadi Sukses jika semua valid

            using (SqlConnection con = new SqlConnection(strKonek))
            {
                con.Open();
                SqlTransaction transaction = con.BeginTransaction(); // Memulai transaksi

                try
                {
                    SqlCommand cmd = new SqlCommand("sp_InsertTransaksi", con, transaction); // Menggunakan stored procedure
                    cmd.CommandType = CommandType.StoredProcedure; // Mengatur CommandType menjadi StoredProcedure

                    cmd.Parameters.AddWithValue("@ID_Customer", idCustomer);
                    cmd.Parameters.AddWithValue("@ID_Paket", idPaket);
                    cmd.Parameters.AddWithValue("@ID_Pembayaran", idPembayaran);
                    cmd.Parameters.AddWithValue("@Status", status); // Status ditentukan di C# dan akan diproses oleh trigger server-side

                    cmd.ExecuteNonQuery();
                    transaction.Commit(); // Commit transaksi jika berhasil

                    MessageBox.Show("Transaksi berhasil ditambahkan dengan status: " + status, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadTransaksiData();
                    ClearFields();
                }
                catch (SqlException ex)
                {
                    transaction.Rollback(); // Rollback transaksi jika terjadi error SQL
                    MessageBox.Show("Gagal menambahkan transaksi (SQL Error): " + ex.Message, "Error Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    transaction.Rollback(); // Rollback transaksi jika terjadi error umum
                    MessageBox.Show("Gagal menambahkan transaksi (General Error): " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private bool ValidateID(int id, string tableName)
        {
            string columnName = string.Empty;

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
                    return false;
            }

            string query = $"SELECT COUNT(*) FROM {tableName} WHERE {columnName} = @ID";

            using (SqlConnection con = new SqlConnection(strKonek))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@ID", id);
                    con.Open();
                    int count = (int)cmd.ExecuteScalar();
                    return count > 0;
                }
                catch (SqlException ex)
                {
                    MessageBox.Show($"Error saat memvalidasi ID di tabel {tableName}: " + ex.Message, "Error Validasi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Terjadi kesalahan saat memvalidasi ID: " + ex.Message, "Error Umum", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }

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

                // Mengambil ID dari ComboBox, jika dipilih
                // Jika ComboBox kosong, fallback ke TextBox

                if (!NetworkHelper.EnsureNetworkAvailable(this)) return;

                if (CmbCust.SelectedItem != null && int.TryParse(CmbCust.SelectedItem.ToString(), out int custIdFromCmb))
                {
                    idCustomer = custIdFromCmb;
                    if (!ValidateID((int)idCustomer, "Customer")) isInvalid = true;
                }
                else if (!string.IsNullOrWhiteSpace(Idcust.Text) && int.TryParse(Idcust.Text, out int custIdFromText))
                {
                    idCustomer = custIdFromText;
                    if (!ValidateID((int)idCustomer, "Customer")) isInvalid = true;
                }
                else
                {
                    isEmpty = true;
                }

                if (CmbGame.SelectedItem != null && int.TryParse(CmbGame.SelectedItem.ToString(), out int paketIdFromCmb))
                {
                    idPaket = paketIdFromCmb;
                    if (!ValidateID((int)idPaket, "Paket_TopUp")) isInvalid = true;
                }
                else if (!string.IsNullOrWhiteSpace(Idpaket.Text) && int.TryParse(Idpaket.Text, out int paketIdFromText))
                {
                    idPaket = paketIdFromText;
                    if (!ValidateID((int)idPaket, "Paket_TopUp")) isInvalid = true;
                }
                else
                {
                    isEmpty = true;
                }



                if (CmbPembayaran.SelectedItem != null && int.TryParse(CmbPembayaran.SelectedItem.ToString(), out int bayarIdFromCmb))
                {
                    idPembayaran = bayarIdFromCmb;
                    if (!ValidateID((int)idPembayaran, "Sistem_Pembayaran")) isInvalid = true;
                }
                else if (!string.IsNullOrWhiteSpace(Idpembayaran.Text) && int.TryParse(Idpembayaran.Text, out int bayarIdFromText))
                {
                    idPembayaran = bayarIdFromText;
                    if (!ValidateID((int)idPembayaran, "Sistem_Pembayaran")) isInvalid = true;
                }
                else
                {
                    isEmpty = true;
                }

                if (isEmpty) status = "Pending";
                else if (isInvalid) status = "Gagal";
                else status = "Sukses"; // Pastikan status menjadi Sukses jika semua valid

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

                using (SqlConnection con = new SqlConnection(strKonek))
                {
                    con.Open();
                    SqlTransaction transaction = con.BeginTransaction(); // Memulai transaksi

                    try
                    {
                        SqlCommand cmd = new SqlCommand("sp_UpdateTransaksi", con, transaction); // Menggunakan stored procedure
                        cmd.CommandType = CommandType.StoredProcedure; // Mengatur CommandType menjadi StoredProcedure

                        cmd.Parameters.AddWithValue("@ID_Transaksi", idTransaksi);
                        cmd.Parameters.AddWithValue("@ID_Customer", idCustomer);
                        cmd.Parameters.AddWithValue("@ID_Paket", idPaket);
                        cmd.Parameters.AddWithValue("@ID_Pembayaran", idPembayaran);
                        cmd.Parameters.AddWithValue("@Status", status);

                        cmd.ExecuteNonQuery();
                        transaction.Commit(); // Commit transaksi jika berhasil

                        MessageBox.Show("Transaksi berhasil diperbarui!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadTransaksiData();
                        ClearFields();
                    }
                    catch (SqlException ex)
                    {
                        transaction.Rollback(); // Rollback transaksi jika terjadi error SQL
                        MessageBox.Show("Gagal memperbarui transaksi (SQL Error): " + ex.Message, "Error Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback(); // Rollback transaksi jika terjadi error umum
                        MessageBox.Show("Gagal memperbarui transaksi (General Error): " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Silakan pilih transaksi yang akan diedit.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            if (DGVTransaksi.SelectedRows.Count > 0)
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

                if (!NetworkHelper.EnsureNetworkAvailable(this)) return;

                if (confirm == DialogResult.Yes)
                {
                    using (SqlConnection con = new SqlConnection(strKonek))
                    {
                        con.Open();
                        SqlTransaction transaction = con.BeginTransaction(); // Memulai transaksi

                        try
                        {
                            foreach (DataGridViewRow row in DGVTransaksi.SelectedRows)
                            {
                                if (!row.IsNewRow)
                                {
                                    int idTransaksi = Convert.ToInt32(row.Cells["ID_Transaksi"].Value);

                                    SqlCommand cmd = new SqlCommand("sp_DeleteTransaksi", con, transaction); // Menggunakan stored procedure
                                    cmd.CommandType = CommandType.StoredProcedure; // Mengatur CommandType menjadi StoredProcedure
                                    cmd.Parameters.AddWithValue("@ID_Transaksi", idTransaksi);
                                    cmd.ExecuteNonQuery();
                                }
                            }
                            transaction.Commit(); // Commit transaksi jika berhasil
                            MessageBox.Show($"{DGVTransaksi.SelectedRows.Count} transaksi berhasil dihapus!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (SqlException ex)
                        {
                            transaction.Rollback(); // Rollback transaksi jika terjadi error SQL
                            MessageBox.Show("Gagal menghapus transaksi (SQL Error): " + ex.Message, "Error Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback(); // Rollback transaksi jika terjadi error umum
                            MessageBox.Show("Gagal menghapus transaksi (General Error): " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }

                    LoadTransaksiData();
                    ClearFields();
                }
            }
            else
            {
                MessageBox.Show("Silakan pilih transaksi yang akan dihapus.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void DGVTransaksi_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = DGVTransaksi.Rows[e.RowIndex];
                // Mengisi ComboBoxes dan TextBoxes dengan nilai ID dari baris yang dipilih
                // ComboBoxes akan menampilkan ID, dan TextBoxes akan terisi secara otomatis
                CmbCust.Text = row.Cells["ID_Customer"].Value.ToString();
                CmbGame.Text = row.Cells["ID_Paket"].Value.ToString();
                CmbPembayaran.Text = row.Cells["ID_Pembayaran"].Value.ToString();

                // Pastikan Idcust, Idpaket, Idpembayaran juga terisi dengan ID numerik
                // karena combobox SelectedIndexChanged akan memperbarui ini
                Idcust.Text = row.Cells["ID_Customer"].Value.ToString();
                Idpaket.Text = row.Cells["ID_Paket"].Value.ToString();
                Idpembayaran.Text = row.Cells["ID_Pembayaran"].Value.ToString();
            }
        }

        private void Transaksi_Load(object sender, EventArgs e)
        {
            LoadTransaksiData();
        }

        private void Idpembayaran_TextChanged(object sender, EventArgs e)
        {
            // Tidak ada perubahan yang diperlukan di sini, ComboBox akan memperbarui ini
        }

        private void Idpaket_TextChanged(object sender, EventArgs e)
        {
            // Tidak ada perubahan yang diperlukan di sini, ComboBox akan memperbarui ini
        }

        private void Hapus_Click(object sender, EventArgs e)
        {
            Delete_Click(sender, e);
        }

        private void Idcust_TextChanged(object sender, EventArgs e)
        {
            // Tidak ada perubahan yang diperlukan di sini, ComboBox akan memperbarui ini
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

                for (int i = 0; i < dgv.Columns.Count; i++)
                {
                    worksheet.Cells[1, i + 1] = dgv.Columns[i].HeaderText;
                }

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
            try
            {
                Document pdfDoc = new Document(PageSize.A4, 10, 10, 10, 10);
                PdfWriter.GetInstance(pdfDoc, new FileStream(filePath, FileMode.Create));
                pdfDoc.Open();

                PdfPTable table = new PdfPTable(dgv.Columns.Count);
                table.WidthPercentage = 100; // Mengatur lebar tabel agar memenuhi halaman

                // Menambahkan header tabel
                foreach (DataGridViewColumn column in dgv.Columns)
                {
                    PdfPCell headerCell = new PdfPCell(new Phrase(column.HeaderText, FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 8)));
                    headerCell.BackgroundColor = BaseColor.LIGHT_GRAY;
                    headerCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(headerCell);
                }

                // Menambahkan data baris
                foreach (DataGridViewRow row in dgv.SelectedRows)
                {
                    if (!row.IsNewRow)
                    {
                        foreach (DataGridViewCell cell in row.Cells)
                        {
                            PdfPCell dataCell = new PdfPCell(new Phrase(cell.Value?.ToString() ?? "", FontFactory.GetFont(FontFactory.HELVETICA, 8)));
                            dataCell.HorizontalAlignment = Element.ALIGN_LEFT;
                            table.AddCell(dataCell);
                        }
                    }
                }

                pdfDoc.Add(table);
                pdfDoc.Close();

                MessageBox.Show("Export ke PDF berhasil!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal export ke PDF: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Export_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Excel Files (*.xlsx)|*.xlsx|PDF Files (*.pdf)|*.pdf",
                Title = "Export Data Transaksi",
                FileName = "DataTransaksi"
            };

            if (!NetworkHelper.EnsureNetworkAvailable(this)) return;

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
            using (SqlConnection con = new SqlConnection(strKonek))
            {
                try
                {
                    con.Open();

                    // Memuat ID_Customer ke CmbCust
                    SqlCommand cmdCustomer = new SqlCommand("SELECT ID_Customer FROM Customer", con);
                    SqlDataReader readerCustomer = cmdCustomer.ExecuteReader();
                    CmbCust.Items.Clear();
                    while (readerCustomer.Read())
                    {
                        CmbCust.Items.Add(readerCustomer["ID_Customer"].ToString());
                    }
                    readerCustomer.Close();

                    // Memuat ID_Paket ke CmbGame
                    SqlCommand cmdPaket = new SqlCommand("SELECT ID_Paket FROM Paket_TopUp", con);
                    SqlDataReader readerPaket = cmdPaket.ExecuteReader();
                    CmbGame.Items.Clear();
                    while (readerPaket.Read())
                    {
                        CmbGame.Items.Add(readerPaket["ID_Paket"].ToString());
                    }
                    readerPaket.Close();

                    // Memuat ID_Pembayaran ke CmbPembayaran
                    SqlCommand cmdPayment = new SqlCommand("SELECT ID_Pembayaran FROM Sistem_Pembayaran", con);
                    SqlDataReader readerPayment = cmdPayment.ExecuteReader();
                    CmbPembayaran.Items.Clear();
                    while (readerPayment.Read())
                    {
                        CmbPembayaran.Items.Add(readerPayment["ID_Pembayaran"].ToString());
                    }
                    readerPayment.Close();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Error saat memuat data ComboBox (SQL Error): " + ex.Message, "Error Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Terjadi kesalahan saat memuat data ComboBox: " + ex.Message, "Error Umum", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void CmbCust_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CmbCust.SelectedItem != null)
            {
                Idcust.Text = CmbCust.SelectedItem.ToString(); // Mengisi Idcust.Text dengan nilai yang dipilih dari CmbCust
            }
            else
            {
                Idcust.Text = ""; // Kosongkan jika tidak ada pilihan
            }
        }

        private void CmbGame_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CmbGame.SelectedItem != null)
            {
                Idpaket.Text = CmbGame.SelectedItem.ToString(); // Mengisi Idpaket.Text dengan nilai yang dipilih dari CmbGame
            }
            else
            {
                Idpaket.Text = ""; // Kosongkan jika tidak ada pilihan
            }
        }

        private void CmbPembayaran_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CmbPembayaran.SelectedItem != null)
            {
                Idpembayaran.Text = CmbPembayaran.SelectedItem.ToString(); // Mengisi Idpembayaran.Text dengan nilai yang dipilih dari CmbPembayaran
            }
            else
            {
                Idpembayaran.Text = ""; // Kosongkan jika tidak ada pilihan
            }
        }

        private void ClearFields()
        {
            Idcust.Text = "";
            Idpaket.Text = "";
            Idpembayaran.Text = "";
            CmbCust.SelectedIndex = -1; // Mengosongkan pilihan ComboBox
            CmbGame.SelectedIndex = -1; // Mengosongkan pilihan ComboBox
            CmbPembayaran.SelectedIndex = -1; // Mengosongkan pilihan ComboBox
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            // Tidak ada logika yang perlu diubah di sini
        }

        private void CheckIndexingStatus()
        {
            string query = @"
        SELECT 
            t.name AS TableName,
            c.name AS ColumnName,
            i.name AS IndexName,
            i.type_desc AS IndexType,
            i.is_primary_key,
            i.is_unique
        FROM sys.indexes i
        INNER JOIN sys.index_columns ic ON i.object_id = ic.object_id AND i.index_id = ic.index_id
        INNER JOIN sys.columns c ON ic.object_id = c.object_id AND c.column_id = ic.column_id
        INNER JOIN sys.tables t ON i.object_id = t.object_id
        WHERE t.name = 'Transaksi' AND c.name IN ('ID_Customer', 'ID_Paket', 'ID_Pembayaran');
    ";

            using (SqlConnection con = new SqlConnection(strKonek))
            {
                try
                {
                    con.Open();

                    // Mulai stopwatch
                    Stopwatch stopwatch = new Stopwatch();
                    stopwatch.Start();

                    SqlCommand cmd = new SqlCommand(query, con);
                    SqlDataReader reader = cmd.ExecuteReader();

                    stopwatch.Stop(); // Hentikan stopwatch
                    long elapsedMs = stopwatch.ElapsedMilliseconds;

                    StringBuilder result = new StringBuilder();
                    result.AppendLine($"Durasi Eksekusi Query: {elapsedMs} ms");
                    result.AppendLine("----------------------------------------");

                    while (reader.Read())
                    {
                        result.AppendLine($"Kolom: {reader["ColumnName"]}, " +
                                          $"Index: {reader["IndexName"]}, " +
                                          $"Tipe: {reader["IndexType"]}, " +
                                          $"Unique: {reader["is_unique"]}, PrimaryKey: {reader["is_primary_key"]}");
                    }

                    if (result.ToString().Contains("Kolom:") == false)
                    {
                        MessageBox.Show("Tidak ada index ditemukan pada kolom ID_Customer, ID_Paket, atau ID_Pembayaran.\n" +
                                        $"Durasi: {elapsedMs} ms", "Analisis Indexing", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        MessageBox.Show(result.ToString(), "Analisis Indexing", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal menganalisis indexing: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void CheckQueryPerformance()
        {
            string query = @"
        SELECT 
            t.ID_Transaksi, 
            t.ID_Customer, 
            c.Nama AS NamaCustomer,
            t.ID_Paket, 
            pt.NamaPaket,
            t.ID_Pembayaran, 
            sp.Metode AS MetodePembayaran,
            t.Status, 
            t.TanggalTransaksi 
        FROM Transaksi t
        LEFT JOIN Customer c ON t.ID_Customer = c.ID_Customer
        LEFT JOIN Paket_TopUp pt ON t.ID_Paket = pt.ID_Paket
        LEFT JOIN Sistem_Pembayaran sp ON t.ID_Pembayaran = sp.ID_Pembayaran;
    ";

            using (SqlConnection con = new SqlConnection(strKonek))
            {
                try
                {
                    con.Open();

                    Stopwatch stopwatch = new Stopwatch();
                    stopwatch.Start();

                    SqlCommand cmd = new SqlCommand(query, con);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    System.Data.DataTable dt = new System.Data.DataTable();
                    adapter.Fill(dt);

                    stopwatch.Stop();
                    long elapsedMs = stopwatch.ElapsedMilliseconds;

                    MessageBox.Show($"Query berhasil dijalankan.\nJumlah baris: {dt.Rows.Count}\nDurasi eksekusi: {elapsedMs} ms",
                                    "Analisis Performa Query", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal menjalankan query: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void Analisis_Click(object sender, EventArgs e)
        {
            CheckIndexingStatus();
            CheckQueryPerformance();
        }
    }
}
