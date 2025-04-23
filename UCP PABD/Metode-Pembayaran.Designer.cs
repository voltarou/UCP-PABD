namespace UCP_PABD
{
    partial class Metode_Pembayaran
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
            this.DGVMP = new System.Windows.Forms.DataGridView();
            this.Prev = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.Metode = new System.Windows.Forms.TextBox();
            this.Tambah = new System.Windows.Forms.Button();
            this.Edit = new System.Windows.Forms.Button();
            this.Hapus = new System.Windows.Forms.Button();
            this.Next = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DGVMP)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Image = global::UCP_PABD.Properties.Resources._1353118;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1382, 646);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // DGVMP
            // 
            this.DGVMP.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.DGVMP.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGVMP.Location = new System.Drawing.Point(0, 282);
            this.DGVMP.Name = "DGVMP";
            this.DGVMP.RowHeadersWidth = 51;
            this.DGVMP.RowTemplate.Height = 24;
            this.DGVMP.Size = new System.Drawing.Size(1382, 290);
            this.DGVMP.TabIndex = 1;
            this.DGVMP.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGVMP_CellContentClick);
            // 
            // Prev
            // 
            this.Prev.Location = new System.Drawing.Point(27, 611);
            this.Prev.Name = "Prev";
            this.Prev.Size = new System.Drawing.Size(75, 23);
            this.Prev.TabIndex = 2;
            this.Prev.Text = "<< Prev";
            this.Prev.UseVisualStyleBackColor = true;
            this.Prev.Click += new System.EventHandler(this.Prev_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(37, 126);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "Metode";
            // 
            // Metode
            // 
            this.Metode.Location = new System.Drawing.Point(125, 123);
            this.Metode.Name = "Metode";
            this.Metode.Size = new System.Drawing.Size(100, 22);
            this.Metode.TabIndex = 4;
            this.Metode.TextChanged += new System.EventHandler(this.Metode_TextChanged);
            // 
            // Tambah
            // 
            this.Tambah.Location = new System.Drawing.Point(1116, 80);
            this.Tambah.Name = "Tambah";
            this.Tambah.Size = new System.Drawing.Size(136, 23);
            this.Tambah.TabIndex = 5;
            this.Tambah.Text = "Tambah Metode";
            this.Tambah.UseVisualStyleBackColor = true;
            this.Tambah.Click += new System.EventHandler(this.Tambah_Click);
            // 
            // Edit
            // 
            this.Edit.Location = new System.Drawing.Point(1116, 126);
            this.Edit.Name = "Edit";
            this.Edit.Size = new System.Drawing.Size(136, 23);
            this.Edit.TabIndex = 6;
            this.Edit.Text = "Edit Metode";
            this.Edit.UseVisualStyleBackColor = true;
            this.Edit.Click += new System.EventHandler(this.Edit_Click);
            // 
            // Hapus
            // 
            this.Hapus.Location = new System.Drawing.Point(1116, 174);
            this.Hapus.Name = "Hapus";
            this.Hapus.Size = new System.Drawing.Size(136, 23);
            this.Hapus.TabIndex = 7;
            this.Hapus.Text = "Hapus Metode";
            this.Hapus.UseVisualStyleBackColor = true;
            this.Hapus.Click += new System.EventHandler(this.Hapus_Click);
            // 
            // Next
            // 
            this.Next.Location = new System.Drawing.Point(1284, 611);
            this.Next.Name = "Next";
            this.Next.Size = new System.Drawing.Size(75, 23);
            this.Next.TabIndex = 8;
            this.Next.Text = "Next >>";
            this.Next.UseVisualStyleBackColor = true;
            this.Next.Click += new System.EventHandler(this.Next_Click);
            // 
            // Metode_Pembayaran
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1382, 646);
            this.Controls.Add(this.Next);
            this.Controls.Add(this.Hapus);
            this.Controls.Add(this.Edit);
            this.Controls.Add(this.Tambah);
            this.Controls.Add(this.Metode);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Prev);
            this.Controls.Add(this.DGVMP);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Metode_Pembayaran";
            this.Text = "Metode_Pembayaran";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DGVMP)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.DataGridView DGVMP;
        private System.Windows.Forms.Button Prev;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox Metode;
        private System.Windows.Forms.Button Tambah;
        private System.Windows.Forms.Button Edit;
        private System.Windows.Forms.Button Hapus;
        private System.Windows.Forms.Button Next;
    }
}