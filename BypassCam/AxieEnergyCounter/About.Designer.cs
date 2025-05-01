
namespace AxieEnergyCounter
{
    partial class About
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(About));
            this.InformationAboutUs = new System.Windows.Forms.PictureBox();
            this.CloseAbout = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.InformationAboutUs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CloseAbout)).BeginInit();
            this.SuspendLayout();
            // 
            // InformationAboutUs
            // 
            this.InformationAboutUs.Image = global::EnergyCounter.Properties.Resources.howtouseEnergy;
            this.InformationAboutUs.Location = new System.Drawing.Point(0, 0);
            this.InformationAboutUs.Name = "InformationAboutUs";
            this.InformationAboutUs.Size = new System.Drawing.Size(608, 456);
            this.InformationAboutUs.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.InformationAboutUs.TabIndex = 0;
            this.InformationAboutUs.TabStop = false;
            // 
            // CloseAbout
            // 
            this.CloseAbout.BackColor = System.Drawing.Color.Teal;
            this.CloseAbout.Image = global::EnergyCounter.Properties.Resources.close_box_red;
            this.CloseAbout.Location = new System.Drawing.Point(576, 0);
            this.CloseAbout.Name = "CloseAbout";
            this.CloseAbout.Size = new System.Drawing.Size(32, 32);
            this.CloseAbout.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.CloseAbout.TabIndex = 1;
            this.CloseAbout.TabStop = false;
            this.CloseAbout.Click += new System.EventHandler(this.CloseAbout_Click);
            // 
            // About
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(608, 454);
            this.Controls.Add(this.CloseAbout);
            this.Controls.Add(this.InformationAboutUs);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "About";
            this.Text = "About";
            ((System.ComponentModel.ISupportInitialize)(this.InformationAboutUs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CloseAbout)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox InformationAboutUs;
        private System.Windows.Forms.PictureBox CloseAbout;
    }
}