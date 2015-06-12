using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace colarsisUserControl
{
    internal class Line
    {

        private Point fP;
        private Point lP;

        public Point BeginPoint
        {
            get { return fP; }
            set { fP = value; }
        }

        public Point EndPoint
        {
            get { return lP; }
            set { lP = value; }
        }

        public Line(Point fP, Point lP)
        {
            this.fP = fP;
            this.lP = lP;
        }

    }
}
