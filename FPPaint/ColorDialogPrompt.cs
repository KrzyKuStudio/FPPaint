using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace FPPaint
{
    /// <summary>
    /// Color dialog box class
    /// </summary>
    public static class ColorDialog
    {
        public static SolidColorBrush PromptQestion()
        {
            System.Windows.Forms.ColorDialog c = new System.Windows.Forms.ColorDialog();
            if (c.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                SolidColorBrush myBrush = new SolidColorBrush(Color.FromArgb(c.Color.A, c.Color.R, c.Color.G, c.Color.B));
                return myBrush;
            }
            else
            {
                throw new Exception("Color not changed");
            }
        }
    }
}
