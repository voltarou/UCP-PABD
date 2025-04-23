namespace UCP_PABD
{
    partial class Top_UP
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.DGVTOPUUP = new System.Windows.Forms.DataGridView();
            this.Prev = new System.Windows.Forms.Button();
            this.NamaPaket = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.Harga = new System.Windows.Forms.Label();
            this.Hargaa = new System.Windows.Forms.TextBox();
            this.Hapus = new System.Windows.Forms.Button();
            this.Edit = new System.Windows.Forms.Button();
            this.Tambah = new System.Windows.Forms.Button();
            this.CMB = new System.Windows.Forms.ComboBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.DGVTOPUUP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // DGVTOPUUP
            // 
            this.DGVTOPUUP.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.DGVTOPUUP.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGVTOPUUP.Location = new System.Drawing.Point(0, 258);
            this.DGVTOPUUP.Name = "DGVTOPUUP";
            this.DGVTOPUUP.RowHeadersWidth = 51;
            this.DGVTOPUUP.RowTemplate.Height = 24;
            this.DGVTOPUUP.Size = new System.Drawing.Size(1379, 275);
            this.DGVTOPUUP.TabIndex = 1;
            this.DGVTOPUUP.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGVTOPUUP_CellContentClick);
            // 
            // Prev
            // 
            this.Prev.Location = new System.Drawing.Point(23, 571);
            this.Prev.Name = "Prev";
            this.Prev.Size = new System.Drawing.Size(75, 23);
            this.Prev.TabIndex = 2;
            this.Prev.Text = "<< Prev";
            this.Prev.UseVisualStyleBackColor = true;
            this.Prev.Click += new System.EventHandler(this.Prev_Click);
            // 
            // NamaPaket
            // 
            this.NamaPaket.Location = new System.Drawing.Point(163, 65);
            this.NamaPaket.Name = "NamaPaket";
            this.NamaPaket.Size = new System.Drawing.Size(100, 22);
            this.NamaPaket.TabIndex = 3;
            this.NamaPaket.TextChanged += new System.EventHandler(this.NamaPaket_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(54, 68);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "Nama Paket";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(1292, 571);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 5;
            this.button2.Text = "Next >>";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Harga
            // 
            this.Harga.AutoSize = true;
            this.Harga.Location = new System.Drawing.Point(54, 117);
            this.Harga.Name = "Harga";
            this.Harga.Size = new System.Drawing.Size(45, 16);
            this.Harga.TabIndex = 6;
            this.Harga.Text = "Harga";
            // 
            // Hargaa
            // 
            this.Hargaa.Location = new System.Drawing.Point(163, 111);
            this.Hargaa.Name = "Hargaa";
            this.Hargaa.Size = new System.Drawing.Size(100, 22);
            this.Hargaa.TabIndex = 7;
            this.Hargaa.TextChanged += new System.EventHandler(this.Hargaa_TextChanged);
            // 
            // Hapus
            // 
            this.Hapus.Location = new System.Drawing.Point(1200, 150);
            this.Hapus.Name = "Hapus";
            this.Hapus.Size = new System.Drawing.Size(143, 23);
            this.Hapus.TabIndex = 8;
            this.Hapus.Text = "Hapus Paket";
            this.Hapus.UseVisualStyleBackColor = true;
            this.Hapus.Click += new System.EventHandler(this.Hapus_Click);
            // 
            // Edit
            // 
            this.Edit.Location = new System.Drawing.Point(1200, 110);
            this.Edit.Name = "Edit";
            this.Edit.Size = new System.Drawing.Size(143, 23);
            this.Edit.TabIndex = 9;
            this.Edit.Text = "Edit Paket";
            this.Edit.UseVisualStyleBackColor = true;
            this.Edit.Click += new System.EventHandler(this.Edit_Click);
            // 
            // Tambah
            // 
            this.Tambah.Location = new System.Drawing.Point(1200, 64);
            this.Tambah.Name = "Tambah";
            this.Tambah.Size = new System.Drawing.Size(143, 23);
            this.Tambah.TabIndex = 10;
            this.Tambah.Text = "Tambah Paket";
            this.Tambah.UseVisualStyleBackColor = true;
            this.Tambah.Click += new System.EventHandler(this.Tambah_Click);
            // 
            // CMB
            // 
            this.CMB.FormattingEnabled = true;
            this.CMB.Location = new System.Drawing.Point(57, 167);
            this.CMB.Name = "CMB";
            this.CMB.Size = new System.Drawing.Size(121, 24);
            this.CMB.TabIndex = 11;
            this.CMB.SelectedIndexChanged += new System.EventHandler(this.CMB_SelectedIndexChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Image = global::UCP_PABD.Properties.Resources.Kafka;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1379, 616);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // Top_UP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1379, 616);
            this.Controls.Add(this.CMB);
            this.Controls.Add(this.Tambah);
            this.Controls.Add(this.Edit);
            this.Controls.Add(this.Hapus);
            this.Controls.Add(this.Hargaa);
            this.Controls.Add(this.Harga);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.NamaPaket);
            this.Controls.Add(this.Prev);
            this.Controls.Add(this.DGVTOPUUP);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Top_UP";
            this.Text = "Top_UP";
            ((System.ComponentModel.ISupportInitialize)(this.DGVTOPUUP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.DataGridView DGVTOPUUP;
        private System.Windows.Forms.Button Prev;
        private System.Windows.Forms.TextBox NamaPaket;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label Harga;
        private System.Windows.Forms.TextBox Hargaa;
        private System.Windows.Forms.Button Hapus;
        private System.Windows.Forms.Button Edit;
        private System.Windows.Forms.Button Tambah;
        private System.Windows.Forms.ComboBox CMB;
    }
}