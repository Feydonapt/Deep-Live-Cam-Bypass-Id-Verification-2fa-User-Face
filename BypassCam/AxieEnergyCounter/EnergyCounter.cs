using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Gma.System.MouseKeyHook;
using EnergyCounter;

namespace AxieEnergyCounter
{
    public partial class AxieCounter : Form
    {
        private static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
        private const UInt32 SWP_NOSIZE = 0x0001;
        private const UInt32 SWP_NOMOVE = 0x0002;
        private const UInt32 TOPMOST_FLAGS = SWP_NOMOVE | SWP_NOSIZE;

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;



        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        public static bool ShowBtns = false;
        public static bool LockLocation = false;
        public AxieCounter()
        {
            InitializeComponent();
            SetWindowPos(this.Handle, HWND_TOPMOST, 0, 0, 0, 0, TOPMOST_FLAGS);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            EnergyHelper.Energy = Energy;
            EnergyHelper.ResetCounter();
            EnergyHelper.LoadSettings();
            HotKeyService.Subscribe();
            if (EnergyHelper.ShowHelpInitial)
            {
                About abt = new About();
                abt.StartPosition = FormStartPosition.CenterParent;
                abt.ShowDialog(this);
            }
            var scr = Screen.FromPoint(this.Location);
            this.Location = new Point(scr.WorkingArea.Right - this.Width, scr.WorkingArea.Top + (this.Height - 30));
        }

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        private void AxieCounter_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && !LockLocation)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void CloseApplication(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Add_Click(object sender, EventArgs e)
        {
            EnergyHelper.AddCounter();
        }

        private void Minus_Click(object sender, EventArgs e)
        {
            EnergyHelper.MinusCounter();
        }

        private void Reset_Click(object sender, EventArgs e)
        {
            EnergyHelper.ResetCounter();
        }

        private void ButtonsOptionChanged(object sender, EventArgs e)
        {
            if (Buttons.Text == "Show Buttons")
            {
                Add.Visible = true;
                Minus.Visible = true;
                Reset.Visible = true;
                Buttons.Text = "Hide Buttons";
            }
            else
            {
                Add.Visible = false;
                Minus.Visible = false;
                Reset.Visible = false;
                Buttons.Text = "Show Buttons";
            }
        }

        private void ShowInformation(object sender, EventArgs e)
        {
            About abt = new About();
            abt.ShowDialog();
        }

        private void Energy_RightClick(object sender, EventArgs e)
        {
            var Event = e as MouseEventArgs;
            if (Event.Button == MouseButtons.Right)
            {
                var mPointWhenClicked = new Point(Event.X, Event.Y);
                ShowOptions.Show(Energy, mPointWhenClicked);
            }
        }

        private void LockPosition_Click(object sender, EventArgs e)
        {
            if (LockPosition.Text == "Lock : ✖")
            {
                LockLocation = true;
                LockPosition.Text = "Lock : ✓";
            }
            else
            {
                LockLocation = false;
                LockPosition.Text = "Lock : ✖";
            }
        }

        private void HotKeyMenu_Click(object sender, EventArgs e)
        {
            HotKeySettings hks = new HotKeySettings();
            hks.StartPosition = FormStartPosition.CenterParent;
            hks.ShowDialog(this);
        }
    }
}
