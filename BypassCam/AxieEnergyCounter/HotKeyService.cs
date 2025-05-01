using Gma.System.MouseKeyHook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EnergyCounter
{
    public static class HotKeyService
    {
        private static IKeyboardMouseEvents m_GlobalHook;
        public static void Subscribe()
        {
            m_GlobalHook = Hook.GlobalEvents();
            m_GlobalHook.KeyPress += GlobalHookKeyPress;

            var reset = Combination.FromString($"{EnergyHelper.hotkey.Reset1st}+{EnergyHelper.hotkey.Reset2nd}");
            Action actionUndo = EnergyHelper.ResetCounter;
            Action AddEnergy = EnergyHelper.AddCounter;
            Action LessEnergy = EnergyHelper.MinusCounter;

            var assignment = new Dictionary<Combination, Action>
            {
                {reset, actionUndo},
                {Combination.TriggeredBy(ListKeys.LK.Where(x=> x.ContainsKey(EnergyHelper.hotkey.AddMain)).SingleOrDefault().Values.First()), AddEnergy},
                {Combination.TriggeredBy(ListKeys.LK.Where(x=> x.ContainsKey(EnergyHelper.hotkey.SubtractMain)).SingleOrDefault().Values.First()), LessEnergy},
                {Combination.TriggeredBy(ListKeys.LK.Where(x=> x.ContainsKey(EnergyHelper.hotkey.AddAlternative)).SingleOrDefault().Values.First()), AddEnergy},
                {Combination.TriggeredBy(ListKeys.LK.Where(x=> x.ContainsKey(EnergyHelper.hotkey.SubtractAlternative)).SingleOrDefault().Values.First()), LessEnergy}
            };
            m_GlobalHook.OnCombination(assignment);
        }

        private static void GlobalHookKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '[')
                EnergyHelper.MinusCounter();
            if (e.KeyChar == ']')
                EnergyHelper.AddCounter();
        }

        public static void Unsubscribe()
        {
            m_GlobalHook.KeyPress -= GlobalHookKeyPress;
            m_GlobalHook.Dispose();
        }
    }
}
