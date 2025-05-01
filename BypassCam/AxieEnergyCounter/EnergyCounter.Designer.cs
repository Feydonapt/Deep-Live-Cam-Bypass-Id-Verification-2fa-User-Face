
using System.Drawing;
using System.Windows.Forms;

namespace AxieEnergyCounter
{
    partial class AxieCounter
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AxieCounter));
            this.Energy = new System.Windows.Forms.PictureBox();
            this.ShowOptions = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.Buttons = new System.Windows.Forms.ToolStripMenuItem();
            this.LockPosition = new System.Windows.Forms.ToolStripMenuItem();
            this.ShowInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.CloseButton = new System.Windows.Forms.ToolStripMenuItem();
            this.Add = new System.Windows.Forms.Button();
            this.Minus = new System.Windows.Forms.Button();
            this.Reset = new System.Windows.Forms.Button();
            this.HotKeyMenu = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.Energy)).BeginInit();
            this.ShowOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // Energy
            // 
            this.Energy.Image = global::EnergyCounter.Properties.Resources.e0;
            this.Energy.InitialImage = global::EnergyCounter.Properties.Resources.e0;
            this.Energy.Location = new System.Drawing.Point(72, 8);
            this.Energy.Name = "Energy";
            this.Energy.Size = new System.Drawing.Size(80, 80);
            this.Energy.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Energy.TabIndex = 5;
            this.Energy.TabStop = false;
            this.Energy.WaitOnLoad = true;
            this.Energy.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Energy_RightClick);
            this.Energy.MouseDown += new System.Windows.Forms.MouseEventHandler(this.AxieCounter_MouseDown);
            // 
            // ShowOptions
            // 
            this.ShowOptions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Buttons,
            this.HotKeyMenu,
            this.LockPosition,
            this.ShowInfo,
            this.CloseButton});
            this.ShowOptions.Name = "ShowOptions";
            this.ShowOptions.Size = new System.Drawing.Size(181, 136);
            this.ShowOptions.Text = "Enerhiya Application";
            // 
            // Buttons
            // 
            this.Buttons.Name = "Buttons";
            this.Buttons.Size = new System.Drawing.Size(180, 22);
            this.Buttons.Text = "Show Buttons";
            this.Buttons.Click += new System.EventHandler(this.ButtonsOptionChanged);
            // 
            // LockPosition
            // 
            this.LockPosition.Name = "LockPosition";
            this.LockPosition.Size = new System.Drawing.Size(180, 22);
            this.LockPosition.Text = "Lock : ✖";
            this.LockPosition.Click += new System.EventHandler(this.LockPosition_Click);
            // 
            // ShowInfo
            // 
            this.ShowInfo.Name = "ShowInfo";
            this.ShowInfo.Size = new System.Drawing.Size(180, 22);
            this.ShowInfo.Text = "About";
            this.ShowInfo.Click += new System.EventHandler(this.ShowInformation);
            // 
            // CloseButton
            // 
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(180, 22);
            this.CloseButton.Text = "Close";
            this.CloseButton.Click += new System.EventHandler(this.CloseApplication);
            // 
            // Add
            // 
            this.Add.BackColor = System.Drawing.Color.Transparent;
            this.Add.BackgroundImage = global::EnergyCounter.Properties.Resources.axie_btn_09;
            this.Add.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Add.FlatAppearance.BorderSize = 0;
            this.Add.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.Add.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.Add.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Add.Location = new System.Drawing.Point(16, 16);
            this.Add.Name = "Add";
            this.Add.Size = new System.Drawing.Size(40, 32);
            this.Add.TabIndex = 9;
            this.Add.UseVisualStyleBackColor = false;
            this.Add.UseWaitCursor = true;
            this.Add.Visible = false;
            this.Add.Click += new System.EventHandler(this.Add_Click);
            // 
            // Minus
            // 
            this.Minus.BackColor = System.Drawing.Color.Transparent;
            this.Minus.BackgroundImage = global::EnergyCounter.Properties.Resources.axie_btn_03;
            this.Minus.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Minus.FlatAppearance.BorderSize = 0;
            this.Minus.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.Minus.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.Minus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Minus.Location = new System.Drawing.Point(16, 56);
            this.Minus.Name = "Minus";
            this.Minus.Size = new System.Drawing.Size(40, 32);
            this.Minus.TabIndex = 10;
            this.Minus.UseVisualStyleBackColor = false;
            this.Minus.UseWaitCursor = true;
            this.Minus.Visible = false;
            this.Minus.Click += new System.EventHandler(this.Minus_Click);
            // 
            // Reset
            // 
            this.Reset.BackColor = System.Drawing.Color.Transparent;
            this.Reset.BackgroundImage = global::EnergyCounter.Properties.Resources.axie_btn_07;
            this.Reset.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Reset.FlatAppearance.BorderSize = 0;
            this.Reset.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.Reset.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.Reset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Reset.Location = new System.Drawing.Point(56, 96);
            this.Reset.Name = "Reset";
            this.Reset.Size = new System.Drawing.Size(112, 32);
            this.Reset.TabIndex = 11;
            this.Reset.UseVisualStyleBackColor = false;
            this.Reset.UseWaitCursor = true;
            this.Reset.Visible = false;
            this.Reset.Click += new System.EventHandler(this.Reset_Click);
            // 
            // HotKeyMenu
            // 
            this.HotKeyMenu.Name = "HotKeyMenu";
            this.HotKeyMenu.Size = new System.Drawing.Size(180, 22);
            this.HotKeyMenu.Text = "Hot Keys";
            this.HotKeyMenu.Click += new System.EventHandler(this.HotKeyMenu_Click);
            // 
            // AxieCounter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightYellow;
            this.ClientSize = new System.Drawing.Size(190, 155);
            this.Controls.Add(this.Reset);
            this.Controls.Add(this.Minus);
            this.Controls.Add(this.Add);
            this.Controls.Add(this.Energy);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AxieCounter";
            this.Text = "EnergyCounter";
            this.TransparencyKey = System.Drawing.Color.LightYellow;
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Energy)).EndInit();
            this.ShowOptions.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button Close;
        private System.Windows.Forms.PictureBox Energy;
        private System.Windows.Forms.ContextMenuStrip ShowOptions;
        private System.Windows.Forms.ToolStripMenuItem ShowInfo;
        private System.Windows.Forms.ToolStripMenuItem CloseButton;
        private System.Windows.Forms.ToolStripMenuItem Buttons;
        private System.Windows.Forms.Button Add;
        private System.Windows.Forms.Button Minus;
        private System.Windows.Forms.Button Reset;
        private ToolStripMenuItem LockPosition;
        private ToolStripMenuItem HotKeyMenu;
    }
}

