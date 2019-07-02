using System;
using System.Collections.Generic;
using System.Text;

namespace C18_Ex05
{
    public class Progarm
    {
        [STAThread]
        public static void Main()
        {
            FormGameSettings formGameSettings = new FormGameSettings();
            formGameSettings.ShowDialog();
        }
    }
}
