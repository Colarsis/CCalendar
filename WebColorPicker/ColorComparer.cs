using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace CCalendar
{
    class ColorComparer : IComparer<Color>
    {
        public int Compare(Color color, Color color2)
        {
            if (color.A < color2.A)
            {
                return -1;
            }
            if (color.A > color2.A)
            {
                return 1;
            }
            if (color.GetHue() < color2.GetHue())
            {
                return -1;
            }
            if (color.GetHue() > color2.GetHue())
            {
                return 1;
            }
            if (color.GetSaturation() < color2.GetSaturation())
            {
                return -1;
            }
            if (color.GetSaturation() > color2.GetSaturation())
            {
                return 1;
            }
            if (color.GetBrightness() < color2.GetBrightness())
            {
                return -1;
            }
            if (color.GetBrightness() > color2.GetBrightness())
            {
                return 1;
            }
            return 0;
        }
    }
}
