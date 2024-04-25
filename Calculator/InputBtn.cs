using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    internal class InputBtn : Button
    {
        private static List<InputBtn> allButtons = new List<InputBtn>();

        public InputBtn()
        {
            allButtons.Add(this);
        }

        public static void Change_Btncolor(Color color)
        {
            foreach (InputBtn button in allButtons)
            {
                button.ForeColor = color;
            }
        }
    }
}
