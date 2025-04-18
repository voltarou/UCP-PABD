using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace UCP_PABD
{
    public partial class FormCustomer : Form
    {
        private DataGridView dgvCustomer;
        private TextBox txtNama;
        private TextBox txtEmail;
        private TextBox txtNoHP;
        private ComboBox cmbGame;
        private Button btnTambahCustomer;

        private readonly string connectionString = "Data Source=VOLTAROU;Initial Catalog=TopUpGameOL;Integrated Security=True;";

        public FormCustomer()
        {
            InitializeComponent();
        }

        private void FormCustomer_Load(object sender, EventArgs e)
        {
            LoadCustomerData();
            LoadGamesToComboBox();
        }

        private void LoadCustomerData()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
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
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT ID_Game, NamaGame FROM Games";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(reader);
                    cmbGame.DataSource = dt;
                    cmbGame.DisplayMember = "NamaGame";
                    cmbGame.ValueMember = "ID_Game";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal memuat data game:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnTambahCustomer_Click(object sender, EventArgs e)
        {
            if (txtNama.Text.Trim() == "" || txtEmail.Text.Trim() == "" || txtNoHP.Text.Trim() == "")
            {
                MessageBox.Show("Semua field wajib diisi!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "INSERT INTO Customer (Nama, Email, No_HP) VALUES (@Nama, @Email, @No_HP)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Nama", txtNama.Text.Trim());
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                    cmd.Parameters.AddWithValue("@No_HP", txtNoHP.Text.Trim());

                    int result = cmd.ExecuteNonQuery();
                    if (result > 0)
                    {
                        MessageBox.Show("Customer berhasil ditambahkan!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadCustomerData();
                        ClearInputFields();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal menambahkan customer:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            this.cmbGame = new System.Windows.Forms.ComboBox();
            this.btnTambahCustomer = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCustomer)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvCustomer
            // 
            this.dgvCustomer.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCustomer.Location = new System.Drawing.Point(-11, 529);
            this.dgvCustomer.Name = "dgvCustomer";
            this.dgvCustomer.Size = new System.Drawing.Size(1335, 150);
            this.dgvCustomer.TabIndex = 0;
            this.dgvCustomer.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCustomer_CellContentClick);
            // 
            // txtNama
            // 
            this.txtNama.Location = new System.Drawing.Point(12, 12);
            this.txtNama.Name = "txtNama";
            this.txtNama.Size = new System.Drawing.Size(193, 22);
            this.txtNama.TabIndex = 1;
            this.txtNama.TextChanged += new System.EventHandler(this.txtNama_TextChanged);
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(12, 66);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(193, 22);
            this.txtEmail.TabIndex = 2;
            this.txtEmail.TextChanged += new System.EventHandler(this.txtEmail_TextChanged);
            // 
            // txtNoHP
            // 
            this.txtNoHP.Location = new System.Drawing.Point(12, 121);
            this.txtNoHP.Name = "txtNoHP";
            this.txtNoHP.Size = new System.Drawing.Size(193, 22);
            this.txtNoHP.TabIndex = 3;
            this.txtNoHP.TextChanged += new System.EventHandler(this.txtNoHP_TextChanged);
            // 
            // cmbGame
            // 
            this.cmbGame.FormattingEnabled = true;
            this.cmbGame.Location = new System.Drawing.Point(1104, 29);
            this.cmbGame.Name = "cmbGame";
            this.cmbGame.Size = new System.Drawing.Size(121, 24);
            this.cmbGame.TabIndex = 4;
            this.cmbGame.SelectedIndexChanged += new System.EventHandler(this.cmbGame_SelectedIndexChanged);
            // 
            // btnTambahCustomer
            // 
            this.btnTambahCustomer.Location = new System.Drawing.Point(1080, 93);
            this.btnTambahCustomer.Name = "btnTambahCustomer";
            this.btnTambahCustomer.Size = new System.Drawing.Size(184, 23);
            this.btnTambahCustomer.TabIndex = 5;
            this.btnTambahCustomer.Text = "Tambah Customer";
            this.btnTambahCustomer.UseVisualStyleBackColor = true;
            this.btnTambahCustomer.Click += new System.EventHandler(this.btnTambahCustomer_Click);
            // 
            // FormCustomer
            // 
            this.ClientSize = new System.Drawing.Size(1322, 671);
            this.Controls.Add(this.btnTambahCustomer);
            this.Controls.Add(this.cmbGame);
            this.Controls.Add(this.txtNoHP);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.txtNama);
            this.Controls.Add(this.dgvCustomer);
            this.Name = "FormCustomer";
            this.Load += new System.EventHandler(this.FormCustomer_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCustomer)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        // Event tambahan
        private void dgvCustomer_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void txtNama_TextChanged(object sender, EventArgs e) { }
        private void txtEmail_TextChanged(object sender, EventArgs e) { }
        private void txtNoHP_TextChanged(object sender, EventArgs e) { }
        private void cmbGame_SelectedIndexChanged(object sender, EventArgs e) { }
    }
}
