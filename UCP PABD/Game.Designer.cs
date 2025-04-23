namespace UCP_PABD
{
    partial class Game
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
            this.Tambah = new System.Windows.Forms.Button();
            this.List = new System.Windows.Forms.DataGridView();
            this.Hapus = new System.Windows.Forms.Button();
            this.Edit = new System.Windows.Forms.Button();
            this.NMG1 = new System.Windows.Forms.TextBox();
            this.PBLSHR = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.prev = new System.Windows.Forms.Button();
            this.CMBGAME = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.List)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // Tambah
            // 
            this.Tambah.Location = new System.Drawing.Point(920, 56);
            this.Tambah.Name = "Tambah";
            this.Tambah.Size = new System.Drawing.Size(119, 23);
            this.Tambah.TabIndex = 13;
            this.Tambah.Text = "Tambah Game";
            this.Tambah.UseVisualStyleBackColor = true;
            this.Tambah.Click += new System.EventHandler(this.Tambah_Click);
            // 
            // List
            // 
            this.List.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.List.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.List.Location = new System.Drawing.Point(0, 299);
            this.List.Name = "List";
            this.List.RowHeadersWidth = 51;
            this.List.RowTemplate.Height = 24;
            this.List.Size = new System.Drawing.Size(1098, 312);
            this.List.TabIndex = 14;
            this.List.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.List_CellContentClick);
            // 
            // Hapus
            // 
            this.Hapus.Location = new System.Drawing.Point(920, 159);
            this.Hapus.Name = "Hapus";
            this.Hapus.Size = new System.Drawing.Size(119, 23);
            this.Hapus.TabIndex = 15;
            this.Hapus.Text = "Hapus Game";
            this.Hapus.UseVisualStyleBackColor = true;
            this.Hapus.Click += new System.EventHandler(this.Hapus_Click);
            // 
            // Edit
            // 
            this.Edit.Location = new System.Drawing.Point(920, 113);
            this.Edit.Name = "Edit";
            this.Edit.Size = new System.Drawing.Size(119, 23);
            this.Edit.TabIndex = 16;
            this.Edit.Text = "Edit Game";
            this.Edit.UseVisualStyleBackColor = true;
            this.Edit.Click += new System.EventHandler(this.Edit_Click);
            // 
            // NMG1
            // 
            this.NMG1.Location = new System.Drawing.Point(156, 60);
            this.NMG1.Name = "NMG1";
            this.NMG1.Size = new System.Drawing.Size(100, 22);
            this.NMG1.TabIndex = 17;
            this.NMG1.TextChanged += new System.EventHandler(this.NMG1_TextChanged);
            // 
            // PBLSHR
            // 
            this.PBLSHR.Location = new System.Drawing.Point(156, 117);
            this.PBLSHR.Name = "PBLSHR";
            this.PBLSHR.Size = new System.Drawing.Size(100, 22);
            this.PBLSHR.TabIndex = 18;
            this.PBLSHR.TextChanged += new System.EventHandler(this.PBLSHR_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(51, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 16);
            this.label1.TabIndex = 19;
            this.label1.Text = "Nama Game";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(60, 120);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 16);
            this.label2.TabIndex = 20;
            this.label2.Text = "Publisher";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1011, 635);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 21;
            this.button1.Text = "Next >>";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Image = global::UCP_PABD.Properties.Resources._1353722;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1098, 670);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // prev
            // 
            this.prev.Location = new System.Drawing.Point(12, 635);
            this.prev.Name = "prev";
            this.prev.Size = new System.Drawing.Size(75, 23);
            this.prev.TabIndex = 22;
            this.prev.Text = "<< Prev";
            this.prev.UseVisualStyleBackColor = true;
            this.prev.Click += new System.EventHandler(this.prev_Click);
            // 
            // CMBGAME
            // 
            this.CMBGAME.FormattingEnabled = true;
            this.CMBGAME.Location = new System.Drawing.Point(63, 183);
            this.CMBGAME.Name = "CMBGAME";
            this.CMBGAME.Size = new System.Drawing.Size(121, 24);
            this.CMBGAME.TabIndex = 23;
            this.CMBGAME.SelectedIndexChanged += new System.EventHandler(this.CMBGAME_SelectedIndexChanged);
            // 
            // Game
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1098, 670);
            this.Controls.Add(this.CMBGAME);
            this.Controls.Add(this.prev);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.PBLSHR);
            this.Controls.Add(this.NMG1);
            this.Controls.Add(this.Edit);
            this.Controls.Add(this.Hapus);
            this.Controls.Add(this.List);
            this.Controls.Add(this.Tambah);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Game";
            this.Text = "Game";
            ((System.ComponentModel.ISupportInitialize)(this.List)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button Tambah;
        private System.Windows.Forms.DataGridView List;
        private System.Windows.Forms.Button Hapus;
        private System.Windows.Forms.Button Edit;
        private System.Windows.Forms.TextBox NMG1;
        private System.Windows.Forms.TextBox PBLSHR;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button prev;
        private System.Windows.Forms.ComboBox CMBGAME;
    }
}