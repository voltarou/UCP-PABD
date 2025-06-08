namespace UCP_PABD
{
    partial class All
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
            this.Customer = new System.Windows.Forms.Button();
            this.Game = new System.Windows.Forms.Button();
            this.TOPUP = new System.Windows.Forms.Button();
            this.Metode = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.LogOut = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Image = global::UCP_PABD.Properties.Resources.Nyantai;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1350, 652);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // Customer
            // 
            this.Customer.Location = new System.Drawing.Point(291, 109);
            this.Customer.Name = "Customer";
            this.Customer.Size = new System.Drawing.Size(98, 36);
            this.Customer.TabIndex = 1;
            this.Customer.Text = "Customer";
            this.Customer.UseVisualStyleBackColor = true;
            this.Customer.Click += new System.EventHandler(this.Customer_Click);
            // 
            // Game
            // 
            this.Game.Location = new System.Drawing.Point(515, 109);
            this.Game.Name = "Game";
            this.Game.Size = new System.Drawing.Size(98, 36);
            this.Game.TabIndex = 2;
            this.Game.Text = "Game";
            this.Game.UseVisualStyleBackColor = true;
            this.Game.Click += new System.EventHandler(this.Game_Click);
            // 
            // TOPUP
            // 
            this.TOPUP.Location = new System.Drawing.Point(711, 109);
            this.TOPUP.Name = "TOPUP";
            this.TOPUP.Size = new System.Drawing.Size(98, 36);
            this.TOPUP.TabIndex = 3;
            this.TOPUP.Text = "Top-UP";
            this.TOPUP.UseVisualStyleBackColor = true;
            this.TOPUP.Click += new System.EventHandler(this.TOPUP_Click);
            // 
            // Metode
            // 
            this.Metode.Location = new System.Drawing.Point(291, 192);
            this.Metode.Name = "Metode";
            this.Metode.Size = new System.Drawing.Size(98, 36);
            this.Metode.TabIndex = 4;
            this.Metode.Text = "Metode";
            this.Metode.UseVisualStyleBackColor = true;
            this.Metode.Click += new System.EventHandler(this.Metode_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(515, 192);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(98, 36);
            this.button1.TabIndex = 5;
            this.button1.Text = "Transaksi";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // LogOut
            // 
            this.LogOut.Location = new System.Drawing.Point(711, 192);
            this.LogOut.Name = "LogOut";
            this.LogOut.Size = new System.Drawing.Size(98, 36);
            this.LogOut.TabIndex = 6;
            this.LogOut.Text = "Log Out";
            this.LogOut.UseVisualStyleBackColor = true;
            this.LogOut.Click += new System.EventHandler(this.LogOut_Click);
            // 
            // All
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1350, 652);
            this.Controls.Add(this.LogOut);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.Metode);
            this.Controls.Add(this.TOPUP);
            this.Controls.Add(this.Game);
            this.Controls.Add(this.Customer);
            this.Controls.Add(this.pictureBox1);
            this.Name = "All";
            this.Text = "All";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button Customer;
        private System.Windows.Forms.Button Game;
        private System.Windows.Forms.Button TOPUP;
        private System.Windows.Forms.Button Metode;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button LogOut;
    }
}