using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace EnergyCounter
{
    public static class EnergyHelper
    {
        public static EnergyModel EM = new EnergyModel() { EnergyCount = 3 };
        public static HotKeys hotkey { get; set; }
        public static bool ShowHelpInitial { get; set; } = true;
        public static PictureBox Energy { get; set; }
        public static void ResetCounter()
        {
            Energy.Image = Properties.Resources.e3;
            EM.EnergyCount = 3;
        }

        public static void AddCounter()
        {
            if (EM.EnergyCount < 10)
            {
                Energy.Image = (Image)Properties.Resources.ResourceManager.GetObject($"e{EM.EnergyCount + 1}");
                EM.EnergyCount++;
            }
        }

        public static void MinusCounter()
        {
            if (EM.EnergyCount > 0)
            {
                Energy.Image = (Image)Properties.Resources.ResourceManager.GetObject($"e{EM.EnergyCount - 1}");
                EM.EnergyCount--;
            }
        }

        public static void LoadSettings()
        {
            RegistryKey key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\EnergyCounter");
            var InitialOpen = key.GetValue("InitialOpen");
            if (InitialOpen == null)
            {
                key.SetValue("InitialOpen", false);
                ShowHelpInitial = true;
            }
            else
            {
                ShowHelpInitial = InitialOpen.ToString() == "True" ? true : false;
            }
            var Reset1st = key.GetValue("Reset1st");
            if (Reset1st == null)
            {
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
                    SubtractAlternative = "KeysDown"
                };
                hotkey = hk;
            }
            else
            {
                HotKeys hk = new HotKeys()
                {
                    Reset1st = key.GetValue("Reset1st").ToString(),
                    Reset2nd = key.GetValue("Reset2nd").ToString(),
                    AddMain = key.GetValue("AddMain").ToString(),
                    AddAlternative = key.GetValue("AddAlternative").ToString(),
                    SubtractMain = key.GetValue("SubtractMain").ToString(),
                    SubtractAlternative = key.GetValue("SubtractAlternative").ToString(),
                };
                hotkey = hk;
            }
        }
    }
}
