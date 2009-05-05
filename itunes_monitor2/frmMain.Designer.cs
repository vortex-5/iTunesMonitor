namespace itunes_monitor
{
    partial class frmMain
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
            this.components = new System.ComponentModel.Container();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.output = new System.Windows.Forms.Label();
            this.tmriTunesCheck = new System.Windows.Forms.Timer(this.components);
            this.tmrBlink = new System.Windows.Forms.Timer(this.components);
            this.tmriTunesGrow = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::itunes_monitor.Properties.Resources.ITunes_Icon;
            this.pictureBox1.Location = new System.Drawing.Point(170, 43);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(218, 224);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Visible = false;
            // 
            // output
            // 
            this.output.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.output.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.output.Location = new System.Drawing.Point(0, 513);
            this.output.Name = "output";
            this.output.Size = new System.Drawing.Size(554, 29);
            this.output.TabIndex = 1;
            this.output.Text = "iTunes Is Loading...";
            this.output.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tmriTunesCheck
            // 
            this.tmriTunesCheck.Interval = 8000;
            this.tmriTunesCheck.Tick += new System.EventHandler(this.tmriTunesCheck_Tick);
            // 
            // tmrBlink
            // 
            this.tmrBlink.Interval = 500;
            this.tmrBlink.Tick += new System.EventHandler(this.tmrBlink_Tick);
            // 
            // tmriTunesGrow
            // 
            this.tmriTunesGrow.Interval = 1;
            this.tmriTunesGrow.Tick += new System.EventHandler(this.tmriTunesGrow_Tick);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(554, 542);
            this.ControlBox = false;
            this.Controls.Add(this.output);
            this.Controls.Add(this.pictureBox1);
            this.DoubleBuffered = true;
            this.Enabled = false;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMain";
            this.Opacity = 0;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Monitor Window";
            this.Load += new System.EventHandler(this.frmMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label output;
        private System.Windows.Forms.Timer tmriTunesCheck;
        private System.Windows.Forms.Timer tmrBlink;
        private System.Windows.Forms.Timer tmriTunesGrow;
    }
}

