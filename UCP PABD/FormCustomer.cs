
using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace UCP_PABD
{
    public partial class FormCustomer : Form
    {
        Koneksi Kn = new Koneksi(); // Membuat instance dari kelas Koneksi
        private string strKonek; // Variabel untuk menyimpan string koneksi

        private DataGridView dgvCustomer;
        private int selectedCustomerId = -1;
        private TextBox txtNama;
        private TextBox txtEmail;
        private TextBox txtNoHP;
        private Button btnTambahCustomer;
        private PictureBox pictureBox1;
        private Label label1;
        private Label label2;
        private Label label3;
        private Button Hapus;
        private Button Edit;
        private Button Next;
        private Button Analisis;
        private Button Prev;
        // The connectionString variable is no longer needed as we will use strKonek
        // private readonly string connectionString = "Data Source=VOLTAROU;Initial Catalog=TopUpGameOL;Integrated Security=True;";

        public FormCustomer()
        {
            InitializeComponent();
            this.Load += FormCustomer_Load;
            strKonek = Kn.connectionStirng(); // Mendapatkan string koneksi dari kelas Koneksi
            if (!NetworkHelper.EnsureNetworkAvailable(this)) return;
        }

        private void FormCustomer_Load(object sender, EventArgs e)
        {
            LoadCustomerData();
            LoadGamesToComboBox();
        }

        private void LoadCustomerData()
        {
            using (SqlConnection conn = new SqlConnection(strKonek)) // Menggunakan strKonek
            {
                try
                {
                    conn.Open();
                    string query = "SELECT ID_Customer, Nama, Email, No_HP FROM Customer";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgvCustomer.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal memuat data customer:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void LoadGamesToComboBox()
        {
            using (SqlConnection conn = new SqlConnection(strKonek)) // Menggunakan strKonek
            {
                try
                {
                    conn.Open();
                    string query = "SELECT ID_Game, NamaGame FROM Games";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(reader);
                    // If you have a ComboBox for games, you would bind it here.
                    // Example: cmbGame.DataSource = dt; cmbGame.DisplayMember = "NamaGame"; cmbGame.ValueMember = "ID_Game";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal memuat data game:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnTambahCustomer_Click(object sender, EventArgs e)
        {
            if (!NetworkHelper.EnsureNetworkAvailable(this)) return;
            // Validasi inputan dari user (tetap sama)
            if (txtNama.Text.Trim() == "" || txtEmail.Text.Trim() == "" || txtNoHP.Text.Trim() == "")
            {
                MessageBox.Show("Semua field wajib diisi!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!txtEmail.Text.Contains("@"))
            {
                MessageBox.Show("Email harus mengandung karakter '@'.", "Validasi Email", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!txtNoHP.Text.All(char.IsDigit))
            {
                MessageBox.Show("No HP hanya boleh berisi angka.", "Validasi No HP", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (txtNoHP.Text.Length < 10 || txtNoHP.Text.Length > 14)
            {
                MessageBox.Show("No HP harus memiliki panjang antara 10 sampai 14 karakter.", "Validasi No HP", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!txtNoHP.Text.StartsWith("08"))
            {
                MessageBox.Show("No HP harus dimulai dengan '08'.", "Validasi No HP", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (txtNama.Text.Any(char.IsDigit))
            {
                MessageBox.Show("Nama tidak boleh mengandung angka.", "Validasi Nama", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection conn = new SqlConnection(strKonek)) // Menggunakan strKonek
            {
                try
                {
                    conn.Open();
                    // Ganti INSERT INTO dengan pemanggilan Stored Procedure
                    SqlCommand cmd = new SqlCommand("usp_InsertCustomer", conn);
                    cmd.CommandType = CommandType.StoredProcedure; // Penting: Menentukan tipe command

                    cmd.Parameters.AddWithValue("@Nama", txtNama.Text.Trim());
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                    cmd.Parameters.AddWithValue("@No_HP", txtNoHP.Text.Trim());

                    // Untuk mengambil hasil dari Stored Procedure (ResultCode dan ResultMessage)
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        int resultCode = Convert.ToInt32(reader["ResultCode"]);
                        string resultMessage = reader["ResultMessage"].ToString();

                        if (resultCode == 1)
                        {
                            MessageBox.Show(resultMessage, "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadCustomerData();
                            ClearInputFields();
                        }
                        else
                        {
                            MessageBox.Show(resultMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Terjadi kesalahan saat menambahkan customer:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ClearInputFields()
        {
            txtNama.Clear();
            txtEmail.Clear();
            txtNoHP.Clear();
            txtNama.Focus();
        }

        private void InitializeComponent()
        {
            this.dgvCustomer = new System.Windows.Forms.DataGridView();
            this.txtNama = new System.Windows.Forms.TextBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.txtNoHP = new System.Windows.Forms.TextBox();
            this.btnTambahCustomer = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.Hapus = new System.Windows.Forms.Button();
            this.Edit = new System.Windows.Forms.Button();
            this.Next = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.Prev = new System.Windows.Forms.Button();
            this.Analisis = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCustomer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvCustomer
            // 
            this.dgvCustomer.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvCustomer.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCustomer.Location = new System.Drawing.Point(0, 298);
            this.dgvCustomer.Name = "dgvCustomer";
            this.dgvCustomer.RowHeadersWidth = 51;
            this.dgvCustomer.Size = new System.Drawing.Size(1322, 301);
            this.dgvCustomer.TabIndex = 0;
            this.dgvCustomer.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCustomer_CellContentClick);
            // 
            // txtNama
            // 
            this.txtNama.Location = new System.Drawing.Point(119, 59);
            this.txtNama.Name = "txtNama";
            this.txtNama.Size = new System.Drawing.Size(193, 22);
            this.txtNama.TabIndex = 1;
            this.txtNama.TextChanged += new System.EventHandler(this.txtNama_TextChanged);
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(119, 108);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(193, 22);
            this.txtEmail.TabIndex = 2;
            this.txtEmail.TextChanged += new System.EventHandler(this.txtEmail_TextChanged);
            // 
            // txtNoHP
            // 
            this.txtNoHP.Location = new System.Drawing.Point(119, 154);
            this.txtNoHP.Name = "txtNoHP";
            this.txtNoHP.Size = new System.Drawing.Size(193, 22);
            this.txtNoHP.TabIndex = 3;
            this.txtNoHP.TextChanged += new System.EventHandler(this.txtNoHP_TextChanged);
            // 
            // btnTambahCustomer
            // 
            this.btnTambahCustomer.Location = new System.Drawing.Point(1049, 134);
            this.btnTambahCustomer.Name = "btnTambahCustomer";
            this.btnTambahCustomer.Size = new System.Drawing.Size(184, 23);
            this.btnTambahCustomer.TabIndex = 5;
            this.btnTambahCustomer.Text = "Tambah Customer";
            this.btnTambahCustomer.UseVisualStyleBackColor = true;
            this.btnTambahCustomer.Click += new System.EventHandler(this.btnTambahCustomer_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(44, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 16);
            this.label1.TabIndex = 7;
            this.label1.Text = "Nama";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(44, 108);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 16);
            this.label2.TabIndex = 8;
            this.label2.Text = "Email";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(44, 154);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 16);
            this.label3.TabIndex = 9;
            this.label3.Text = "No HP";
            // 
            // Hapus
            // 
            this.Hapus.Location = new System.Drawing.Point(1049, 221);
            this.Hapus.Name = "Hapus";
            this.Hapus.Size = new System.Drawing.Size(184, 23);
            this.Hapus.TabIndex = 10;
            this.Hapus.Text = "Hapus Customer";
            this.Hapus.UseVisualStyleBackColor = true;
            this.Hapus.Click += new System.EventHandler(this.Hapus_Click);
            // 
            // Edit
            // 
            this.Edit.Location = new System.Drawing.Point(1049, 177);
            this.Edit.Name = "Edit";
            this.Edit.Size = new System.Drawing.Size(184, 23);
            this.Edit.TabIndex = 11;
            this.Edit.Text = "Edit Customer";
            this.Edit.UseVisualStyleBackColor = true;
            this.Edit.Click += new System.EventHandler(this.Edit_Click);
            // 
            // Next
            // 
            this.Next.Location = new System.Drawing.Point(1222, 625);
            this.Next.Name = "Next";
            this.Next.Size = new System.Drawing.Size(75, 23);
            this.Next.TabIndex = 12;
            this.Next.Text = "Log Out";
            this.Next.UseVisualStyleBackColor = true;
            this.Next.Click += new System.EventHandler(this.Next_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::UCP_PABD.Properties.Resources._1438547_honkai_star_rail_anime_image_board;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1322, 671);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // Prev
            // 
            this.Prev.Location = new System.Drawing.Point(16, 625);
            this.Prev.Name = "Prev";
            this.Prev.Size = new System.Drawing.Size(75, 23);
            this.Prev.TabIndex = 13;
            this.Prev.Text = "<< Prev";
            this.Prev.UseVisualStyleBackColor = true;
            this.Prev.Click += new System.EventHandler(this.Prev_Click);
            // 
            // Analisis
            // 
            this.Analisis.Location = new System.Drawing.Point(47, 221);
            this.Analisis.Name = "Analisis";
            this.Analisis.Size = new System.Drawing.Size(75, 23);
            this.Analisis.TabIndex = 14;
            this.Analisis.Text = "Analisis";
            this.Analisis.UseVisualStyleBackColor = true;
            this.Analisis.Click += new System.EventHandler(this.Analisis_Click);
            // 
            // FormCustomer
            // 
            this.ClientSize = new System.Drawing.Size(1322, 671);
            this.Controls.Add(this.Analisis);
            this.Controls.Add(this.Prev);
            this.Controls.Add(this.Next);
            this.Controls.Add(this.Edit);
            this.Controls.Add(this.Hapus);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnTambahCustomer);
            this.Controls.Add(this.txtNoHP);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.txtNama);
            this.Controls.Add(this.dgvCustomer);
            this.Controls.Add(this.pictureBox1);
            this.Name = "FormCustomer";
            this.Load += new System.EventHandler(this.FormCustomer_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCustomer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        // Event tambahan
        private void dgvCustomer_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvCustomer.Rows[e.RowIndex];
                selectedCustomerId = Convert.ToInt32(row.Cells["ID_Customer"].Value);
                txtNama.Text = row.Cells["Nama"].Value.ToString();
                txtEmail.Text = row.Cells["Email"].Value.ToString();
                txtNoHP.Text = row.Cells["No_HP"].Value.ToString();
            }
        }
        private void txtNama_TextChanged(object sender, EventArgs e) { }
        private void txtEmail_TextChanged(object sender, EventArgs e) { }
        private void txtNoHP_TextChanged(object sender, EventArgs e) { }
        private void cmbGame_SelectedIndexChanged(object sender, EventArgs e) { }

        private void Edit_Click(object sender, EventArgs e)
        {
            if (!NetworkHelper.EnsureNetworkAvailable(this)) return;
            if (selectedCustomerId == -1)
            {
                MessageBox.Show("Pilih data customer terlebih dahulu.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validasi inputan dari user (tetap sama)
            if (txtNama.Text.Trim() == "" || txtEmail.Text.Trim() == "" || txtNoHP.Text.Trim() == "")
            {
                MessageBox.Show("Semua field wajib diisi!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!txtEmail.Text.Contains("@"))
            {
                MessageBox.Show("Email harus mengandung karakter '@'.", "Validasi Email", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!txtNoHP.Text.All(char.IsDigit))
            {
                MessageBox.Show("No HP hanya boleh berisi angka.", "Validasi No HP", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (txtNoHP.Text.Length < 10 || txtNoHP.Text.Length > 14)
            {
                MessageBox.Show("No HP harus memiliki panjang antara 10 sampai 14 karakter.", "Validasi No HP", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!txtNoHP.Text.StartsWith("08"))
            {
                MessageBox.Show("No HP harus dimulai dengan '08'.", "Validasi No HP", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (txtNama.Text.Any(char.IsDigit))
            {
                MessageBox.Show("Nama tidak boleh mengandung angka.", "Validasi Nama", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection conn = new SqlConnection(strKonek)) // Menggunakan strKonek
            {
                try
                {
                    conn.Open();
                    // Ganti UPDATE dengan pemanggilan Stored Procedure
                    SqlCommand cmd = new SqlCommand("usp_UpdateCustomer", conn);
                    cmd.CommandType = CommandType.StoredProcedure; // Penting: Menentukan tipe command

                    cmd.Parameters.AddWithValue("@ID_Customer", selectedCustomerId); // Tambahkan parameter ID
                    cmd.Parameters.AddWithValue("@Nama", txtNama.Text.Trim());
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                    cmd.Parameters.AddWithValue("@No_HP", txtNoHP.Text.Trim());

                    // Untuk mengambil hasil dari Stored Procedure (ResultCode dan ResultMessage)
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        int resultCode = Convert.ToInt32(reader["ResultCode"]);
                        string resultMessage = reader["ResultMessage"].ToString();

                        if (resultCode == 1)
                        {
                            MessageBox.Show(resultMessage, "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadCustomerData();
                            ClearInputFields();
                            selectedCustomerId = -1; // Reset selectedId
                        }
                        else
                        {
                            MessageBox.Show(resultMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Terjadi kesalahan saat memperbarui customer:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void Hapus_Click(object sender, EventArgs e)
        {
            if (!NetworkHelper.EnsureNetworkAvailable(this)) return;
            if (selectedCustomerId == -1)
            {
                MessageBox.Show("Pilih data customer yang ingin dihapus.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show("Yakin ingin menghapus data customer ini?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                using (SqlConnection conn = new SqlConnection(strKonek)) // Menggunakan strKonek
                {
                    try
                    {
                        conn.Open();
                        // Ganti DELETE dengan pemanggilan Stored Procedure
                        SqlCommand cmd = new SqlCommand("usp_DeleteCustomer", conn);
                        cmd.CommandType = CommandType.StoredProcedure; // Penting: Menentukan tipe command

                        cmd.Parameters.AddWithValue("@ID_Customer", selectedCustomerId);

                        // Untuk mengambil hasil dari Stored Procedure (ResultCode dan ResultMessage)
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            int resultCode = Convert.ToInt32(reader["ResultCode"]);
                            string resultMessage = reader["ResultMessage"].ToString();

                            if (resultCode == 1)
                            {
                                MessageBox.Show(resultMessage, "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LoadCustomerData();
                                ClearInputFields();
                                selectedCustomerId = -1; // Reset selectedId
                            }
                            else
                            {
                                MessageBox.Show(resultMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Terjadi kesalahan saat menghapus data:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
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
                this.Hide();         // sembunyikan form sekarang
                using (var l = new Login())   // tampilkan form login
                {
                    l.ShowDialog();
                }
                this.Close();
            }

        }

        private void Prev_Click(object sender, EventArgs e)
        {

            this.Hide(); // Sembunyikan form customer

            // Tampilkan form All
            All fc = new All();
            fc.ShowDialog();

            // Tutup form customer setelah form All ditutup
            this.Close();

        }


        private void Analisis_Click(object sender, EventArgs e)
        {
            CheckIndexingStatus();
            CheckQueryPerformance();
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
    }
}
