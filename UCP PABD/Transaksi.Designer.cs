namespace UCP_PABD
{
    partial class Transaksi
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.DGVTransaksi = new System.Windows.Forms.DataGridView();
            this.Prev = new System.Windows.Forms.Button();
            this.Idcust = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.Edit = new System.Windows.Forms.Button();
            this.Tambah = new System.Windows.Forms.Button();
            this.Idpembayaran = new System.Windows.Forms.TextBox();
            this.Idpaket = new System.Windows.Forms.TextBox();
            this.Hapus = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DGVTransaksi)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Image = global::UCP_PABD.Properties.Resources._7;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1389, 689);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // DGVTransaksi
            // 
            this.DGVTransaksi.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.DGVTransaksi.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGVTransaksi.Location = new System.Drawing.Point(0, 282);
            this.DGVTransaksi.Name = "DGVTransaksi";
            this.DGVTransaksi.RowHeadersWidth = 51;
            this.DGVTransaksi.RowTemplate.Height = 24;
            this.DGVTransaksi.Size = new System.Drawing.Size(1389, 334);
            this.DGVTransaksi.TabIndex = 1;
            this.DGVTransaksi.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGVTransaksi_CellContentClick);
            // 
            // Prev
            // 
            this.Prev.Location = new System.Drawing.Point(33, 654);
            this.Prev.Name = "Prev";
            this.Prev.Size = new System.Drawing.Size(75, 23);
            this.Prev.TabIndex = 2;
            this.Prev.Text = "<< Prev";
            this.Prev.UseVisualStyleBackColor = true;
            this.Prev.Click += new System.EventHandler(this.Prev_Click);
            // 
            // Idcust
            // 
            this.Idcust.Location = new System.Drawing.Point(200, 79);
            this.Idcust.Name = "Idcust";
            this.Idcust.Size = new System.Drawing.Size(100, 22);
            this.Idcust.TabIndex = 4;
            
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(1302, 654);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 5;
            this.button2.Text = "Log Out";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Edit
            // 
            this.Edit.Location = new System.Drawing.Point(1161, 127);
            this.Edit.Name = "Edit";
            this.Edit.Size = new System.Drawing.Size(150, 23);
            this.Edit.TabIndex = 7;
            this.Edit.Text = "Edit Transaksi";
            this.Edit.UseVisualStyleBackColor = true;
            this.Edit.Click += new System.EventHandler(this.Edit_Click);
            // 
            // Tambah
            // 
            this.Tambah.Location = new System.Drawing.Point(1161, 82);
            this.Tambah.Name = "Tambah";
            this.Tambah.Size = new System.Drawing.Size(150, 23);
            this.Tambah.TabIndex = 8;
            this.Tambah.Text = "Tambah Transaksi";
            this.Tambah.UseVisualStyleBackColor = true;
            this.Tambah.Click += new System.EventHandler(this.Tambah_Click);
            // 
            // Idpembayaran
            // 
            this.Idpembayaran.Location = new System.Drawing.Point(200, 178);
            this.Idpembayaran.Name = "Idpembayaran";
            this.Idpembayaran.Size = new System.Drawing.Size(100, 22);
            this.Idpembayaran.TabIndex = 11;
            this.Idpembayaran.TextChanged += new System.EventHandler(this.Idpembayaran_TextChanged);
            // 
            // Idpaket
            // 
            this.Idpaket.Location = new System.Drawing.Point(200, 134);
            this.Idpaket.Name = "Idpaket";
            this.Idpaket.Size = new System.Drawing.Size(100, 22);
            this.Idpaket.TabIndex = 12;
            this.Idpaket.TextChanged += new System.EventHandler(this.Idpaket_TextChanged);
            // 
            // Hapus
            // 
            this.Hapus.Location = new System.Drawing.Point(1161, 177);
            this.Hapus.Name = "Hapus";
            this.Hapus.Size = new System.Drawing.Size(150, 23);
            this.Hapus.TabIndex = 13;
            this.Hapus.Text = "Hapus Transaksi";
            this.Hapus.UseVisualStyleBackColor = true;
            this.Hapus.Click += new System.EventHandler(this.Hapus_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(65, 82);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(104, 16);
            this.label4.TabIndex = 14;
            this.label4.Text = "ID_CUSTOMER";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(65, 184);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(121, 16);
            this.label1.TabIndex = 15;
            this.label1.Text = "ID_PEMBAYARAN";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(65, 140);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 16);
            this.label2.TabIndex = 16;
            this.label2.Text = "ID_PAKET";
            // 
            // Transaksi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1389, 689);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.Hapus);
            this.Controls.Add(this.Idpaket);
            this.Controls.Add(this.Idpembayaran);
            this.Controls.Add(this.Tambah);
            this.Controls.Add(this.Edit);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.Idcust);
            this.Controls.Add(this.Prev);
            this.Controls.Add(this.DGVTransaksi);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Transaksi";
            this.Text = "Transaksi";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DGVTransaksi)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.DataGridView DGVTransaksi;
        private System.Windows.Forms.Button Prev;
        private System.Windows.Forms.TextBox Idcust;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button Edit;
        private System.Windows.Forms.Button Tambah;
        private System.Windows.Forms.TextBox Idpembayaran;
        private System.Windows.Forms.TextBox Idpaket;
        private System.Windows.Forms.Button Hapus;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}