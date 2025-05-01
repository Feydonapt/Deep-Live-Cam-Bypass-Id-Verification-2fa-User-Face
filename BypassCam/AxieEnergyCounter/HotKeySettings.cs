using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EnergyCounter
{
    public partial class HotKeySettings : Form
    {
        public HotKeySettings()
        {
            InitializeComponent();
            Reset1stCombination.Text = EnergyHelper.hotkey.Reset1st;
            Reset2ndCombination.Text = EnergyHelper.hotkey.Reset2nd;
            AddMain.Text = EnergyHelper.hotkey.AddMain;
            AddAlternative.Text = EnergyHelper.hotkey.AddAlternative;
            SubtractMain.Text = EnergyHelper.hotkey.SubtractMain;
            SubtractAlternative.Text = EnergyHelper.hotkey.SubtractAlternative;
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (Reset1stCombination.Text.Length == 0)
            {
                MessageBox.Show("Reset Control is not Exist", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (Reset2ndCombination.Text.Length == 0)
            {
                MessageBox.Show("Reset Letters is not Exist", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (AddMain.Text.Length == 0)
            {
                MessageBox.Show("Add Main Hot Key is not Exist", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (AddAlternative.Text.Length == 0)
            {
                MessageBox.Show("Add Alternative is not Exist", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (SubtractMain.Text.Length == 0)
            {
                MessageBox.Show("Subtract Main Hot Key is not Exist", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (SubtractAlternative.Text.Length == 0)
            {
                MessageBox.Show("Subtract Alternative is not Exist", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                RegistryKey key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\EnergyCounter");
                key.SetValue("Reset1st", Reset1stCombination.Text);
                key.SetValue("Reset2nd", Reset2ndCombination.Text);
                key.SetValue("AddMain", AddMain.Text);
                key.SetValue("AddAlternative", AddAlternative.Text);
                key.SetValue("SubtractMain", SubtractMain.Text);
                key.SetValue("SubtractAlternative", SubtractAlternative.Text);

                HotKeys hk = new HotKeys()
                {
                    Reset1st = Reset1stCombination.Text,
                    Reset2nd = Reset2ndCombination.Text,
                    AddMain = AddMain.Text,
                    AddAlternative = AddAlternative.Text,
                    SubtractMain = SubtractMain.Text,
                    SubtractAlternative = SubtractAlternative.Text,
                };
                EnergyHelper.hotkey = hk;
                DialogResult dialog = MessageBox.Show("Hot Keys Successfully Saved.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (dialog == DialogResult.OK)
                {
                    HotKeyService.Unsubscribe();
                    HotKeyService.Subscribe();
                    this.Close();
                }
            }

        }

        private void Reset1stCombination_KeyUp(object sender, KeyEventArgs e)
        { 
            var Exist = ListKeys.LK.Where(x => x.Values.First() == (e.KeyCode == Keys.Menu ? Keys.Alt : e.KeyCode));
            if (Exist.Any())
            {
                Reset1stCombination.Text = Exist.SingleOrDefault().Keys.First();
            }
            else
            {
                Reset2ndCombination.Text = EnergyHelper.hotkey.Reset1st;
                MessageBox.Show("Key not Applicable", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void Reset2ndCombination_KeyUp(object sender, KeyEventArgs e)
        {
            var Exist = ListKeys.LK.Where(x => x.Values.First() == e.KeyCode);
            if (Exist.Any())
            {
                Reset2ndCombination.Text = Exist.SingleOrDefault().Keys.First();
            }
            else
            {
                Reset2ndCombination.Text = EnergyHelper.hotkey.Reset2nd;
                MessageBox.Show("Key not Applicable", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void AddMain_KeyUp(object sender, KeyEventArgs e)
        {
            var Exist = ListKeys.LK.Where(x => x.Values.First() == e.KeyCode);
            if (Exist.Any())
            {
                AddMain.Text = Exist.SingleOrDefault().Keys.First();
            }
            else
            {
                AddMain.Text = EnergyHelper.hotkey.AddMain;
                MessageBox.Show("Key not Applicable", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void AddAlternative_KeyUp(object sender, KeyEventArgs e)
        {
            var Exist = ListKeys.LK.Where(x => x.Values.First() == e.KeyCode);
            if (Exist.Any())
            {
                AddAlternative.Text = Exist.SingleOrDefault().Keys.First();
            }
            else
            {
                AddAlternative.Text = EnergyHelper.hotkey.AddAlternative;
                MessageBox.Show("Key not Applicable", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void SubtractMain_KeyUp(object sender, KeyEventArgs e)
        {
            var Exist = ListKeys.LK.Where(x => x.Values.First() == e.KeyCode);
            if (Exist.Any())
            {
                SubtractMain.Text = Exist.SingleOrDefault().Keys.First();
            }
            else
            {
                SubtractMain.Text = EnergyHelper.hotkey.SubtractMain;
                MessageBox.Show("Key not Applicable", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void SubtractAlternative_KeyUp(object sender, KeyEventArgs e)
        {
            var Exist = ListKeys.LK.Where(x => x.Values.First() == e.KeyCode);
            if (Exist.Any())
            {
                SubtractAlternative.Text = Exist.SingleOrDefault().Keys.First();
            }
            else
            {
                SubtractAlternative.Text = EnergyHelper.hotkey.SubtractAlternative;
                MessageBox.Show("Key not Applicable", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            RegistryKey key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\EnergyCounter");
            key.SetValue("Reset1st", "Alt");
            key.SetValue("Reset2nd", "R");
            key.SetValue("AddMain", "KeysRight");
            key.SetValue("AddAlternative", "KeysUp");
            key.SetValue("SubtractMain", "KeysLeft");
            key.SetValue("SubtractAlternative", "KeysDown");

            HotKeys hk = new HotKeys()
            {
                Reset1st = "Alt",
                Reset2nd = "R",
                AddMain = "KeysRight",
                AddAlternative = "KeysUp",
                SubtractMain = "KeysLeft",
                SubtractAlternative = "KeysDown",
            };
            EnergyHelper.hotkey = hk;

            Reset1stCombination.Text = EnergyHelper.hotkey.Reset1st;
            Reset2ndCombination.Text = EnergyHelper.hotkey.Reset2nd;
            AddMain.Text = EnergyHelper.hotkey.AddMain;
            AddAlternative.Text = EnergyHelper.hotkey.AddAlternative;
            SubtractMain.Text = EnergyHelper.hotkey.SubtractMain;
            SubtractAlternative.Text = EnergyHelper.hotkey.SubtractAlternative;

            DialogResult dialog = MessageBox.Show("Hot Keys successfully reset to default Keys.", "Reset", MessageBoxButtons.OK, MessageBoxIcon.Information);
            if (dialog == DialogResult.OK)
            {
                HotKeyService.Unsubscribe();
                HotKeyService.Subscribe();
            }
        }
    }
}
