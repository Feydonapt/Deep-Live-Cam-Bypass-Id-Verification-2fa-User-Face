using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AxieEnergyCounter
{
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent(); 
        }

        private void CloseAbout_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
