using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;

namespace colarsisUserControl
{
    public class Calendar : UserControl
    {

        //***********************************************//
        //******************** VARS ********************//

        private Panel panel1;
        private int dayWidth;
        private int hoursHeight;

        private Pen linesPen;

        public delegate void EventHandler(int eventID);
        public event EventHandler EventClickEvent;

        //PROPERTIES//

        //private Color headerTextColor = Color.CadetBlue;
        private Color eventTextColor = Color.Black;
        private Color mainBackgroundColor = Color.LightGray;
        private Color headerBackgroundColor = Color.Gray;
        private SolidBrush headerTextBrush;
        private SolidBrush eventTextBrush;
        private Font headerTextFont = new Font("Microsoft Sans Serif", 12f);
        private Font eventTextFont = new Font("Microsoft Sans Serif", 8.25f);
        private Color sepColor = Color.Gray;
        private Color timeBarColor = Color.Blue;
        private int sepThickness = 1;
        private int week;

        //PROPERTIES//

        
        //LISTS//

        private List<Line> verticalLines = new List<Line>();
        private List<Line> horizontalLines = new List<Line>();
        private Timer timer1;
        private System.ComponentModel.IContainer components;

        private List<Event> events = new List<Event>();

        //LISTS//

        //******************** VARS ********************//
        //***********************************************//


        //*********************************************************//
        //******************** GETTER / SETTER ********************//

        public Font HeaderTextFont
        {
            get { return headerTextFont; }
            set { headerTextFont = value; }
        }

        public Font EventTextFont
        {
            get { return eventTextFont; }
            set { eventTextFont = value; }
        }

        //public Color HeaderTextColor
        //{
        //    get { return headerTextColor; }
        //    set { headerTextColor = value; headerTextBrush.Color = headerTextColor; }
        //}

        public Color EventTextColor
        {
            get { return eventTextColor; }
            set { eventTextColor = value; eventTextBrush.Color = eventTextColor; }
        }

        public Color SeparatorColor
        {
            get { return sepColor; }
            set { sepColor = value; }
        }

        public Color TimeBarColor
        {
            get { return timeBarColor; }
            set { timeBarColor = value; }
        }

        public Color BackgroundColor
        {
            get { return mainBackgroundColor; }
            set { mainBackgroundColor = value; panel1.BackColor = mainBackgroundColor; }
        }

        public Color HeaderBackgroundColor
        {
            get { return headerBackgroundColor; }
            set { headerBackgroundColor = value; }
        }

        public int SeparatorThickness
        {
            get { return sepThickness; }
            set { sepThickness = value; }
        }

        public int SeparatorThickness
        {
            get { return week; }
            set { week = value; }
        }

        //******************** GETTER / SETTER ********************//
        //*********************************************************//

        public Calendar()
        {
            //headerTextBrush = new SolidBrush(headerTextColor);
            eventTextBrush = new SolidBrush(eventTextColor);

            DateTime now = DateTime.Now;

            DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;

            System.Globalization.Calendar cal = dfi.Calendar;

            week = cal.GetWeekOfYear(now, dfi.CalendarWeekRule, dfi.FirstDayOfWeek);

            events.Add(new Event(0, "Ceci est unt test de longue chaine de charactère", "test2", DateTime.Now, DateTime.Now + new TimeSpan(5, 20, 0), Color.Red));
            events.Add(new Event(1, "Ceci est unt test de longue chaine de charactère", "test2", DateTime.Now + new TimeSpan(1, 0, 0, 0), DateTime.Now + new TimeSpan(1, 5, 20, 0), Color.Red));
            events.Add(new Event(2, "Ceci est unt test de longue chaine de charactère", "test2", DateTime.Now + new TimeSpan(2, 0, 0, 0), DateTime.Now + new TimeSpan(2, 5, 20, 0), Color.Red));
            events.Add(new Event(3, "Ceci est unt test de longue chaine de charactère", "test2", DateTime.Now + new TimeSpan(3, 0, 0, 0), DateTime.Now + new TimeSpan(3, 5, 20, 0), Color.Red));

            InitializeComponent();
            initializeLayout();
            updateGraphics();
            updateCursor();

            timer1.Start();
        }

        public void initializeLayout()
        {
            initDays(panel1.Width-100);
            initHours(panel1.Height-70);
        }

        public void updateCursor()
        {
            DateTime now = DateTime.Now;

            Pen p = new Pen(new SolidBrush(timeBarColor), 1);

            Graphics g = panel1.CreateGraphics();

            switch (now.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    g.DrawLine(p, new Point(100 + sepThickness, 70 + now.Hour * hoursHeight + now.Minute * hoursHeight / 60), new Point(100 + dayWidth, 70 + now.Hour * hoursHeight + now.Minute * hoursHeight / 60));
                    g.Dispose();
                    break;
                case DayOfWeek.Tuesday:
                    g.DrawLine(p, new Point(100 + sepThickness + dayWidth, 70 + now.Hour * hoursHeight + now.Minute * hoursHeight / 60), new Point(100 + dayWidth * 2, 70 + now.Hour * hoursHeight + now.Minute * hoursHeight / 60));
                    g.Dispose();
                    break;
                case DayOfWeek.Wednesday:
                    g.DrawLine(p, new Point(100 + sepThickness + dayWidth * 2, 70 + now.Hour * hoursHeight + now.Minute * hoursHeight / 60), new Point(100 + dayWidth * 3, 70 + now.Hour * hoursHeight + now.Minute * hoursHeight / 60));
                    g.Dispose();
                    break;
                case DayOfWeek.Thursday:
                    g.DrawLine(p, new Point(100 + sepThickness + dayWidth * 3, 70 + now.Hour * hoursHeight + now.Minute * hoursHeight / 60), new Point(100 + dayWidth * 4, 70 + now.Hour * hoursHeight + now.Minute * hoursHeight / 60));
                    g.Dispose();
                    break;
                case DayOfWeek.Friday:
                    g.DrawLine(p, new Point(100 + sepThickness + dayWidth * 4, 70 + now.Hour * hoursHeight + now.Minute * hoursHeight / 60), new Point(100 + dayWidth * 5, 70 + now.Hour * hoursHeight + now.Minute * hoursHeight / 60));
                    g.Dispose();
                    break;
                case DayOfWeek.Saturday:
                    g.DrawLine(p, new Point(100 + sepThickness + dayWidth * 5, 70 + now.Hour * hoursHeight + now.Minute * hoursHeight / 60), new Point(100 + dayWidth * 6, 70 + now.Hour * hoursHeight + now.Minute * hoursHeight / 60));
                    g.Dispose();
                    break;
                case DayOfWeek.Sunday:
                    g.DrawLine(p, new Point(100 + sepThickness + dayWidth * 6, 70 + now.Hour * hoursHeight + now.Minute * hoursHeight / 60), new Point(100 + dayWidth * 7, 70 + now.Hour * hoursHeight + now.Minute * hoursHeight / 60));
                    g.Dispose();
                    break;
            }
        }

        public void updateGraphics()
        {

            Graphics g = panel1.CreateGraphics();

            foreach (Line l in verticalLines)
            {
                g.DrawLine(linesPen, l.BeginPoint, l.EndPoint);
            }

            foreach (Line l in horizontalLines)
            {
                g.DrawLine(linesPen, l.BeginPoint, l.EndPoint);
            }

            g.DrawString("Lundi", headerTextFont, headerTextBrush, new Point(100 + dayWidth / 2 - (int.Parse(headerTextFont.Size.ToString()) * "Lundi".Length / 2), 25));
            g.DrawString("Mardi", headerTextFont, headerTextBrush, new Point(100 + dayWidth / 2 + dayWidth - (int.Parse(headerTextFont.Size.ToString()) * "Mardi".Length / 2), 25));
            g.DrawString("Mercredi", headerTextFont, headerTextBrush, new Point(100 + dayWidth / 2 + dayWidth * 2 - (int.Parse(headerTextFont.Size.ToString()) * "Mercredi".Length / 2), 25));
            g.DrawString("Jeudi", headerTextFont, headerTextBrush, new Point(100 + dayWidth / 2 + dayWidth * 3 - (int.Parse(headerTextFont.Size.ToString()) * "Jeudi".Length / 2), 25));
            g.DrawString("Vendredi", headerTextFont, headerTextBrush, new Point(100 + dayWidth / 2 + dayWidth * 4 - (int.Parse(headerTextFont.Size.ToString()) * "Vendredi".Length / 2), 25));
            g.DrawString("Samedi", headerTextFont, headerTextBrush, new Point(100 + dayWidth / 2 + dayWidth * 5 - (int.Parse(headerTextFont.Size.ToString()) * "Samedi".Length / 2), 25));
            g.DrawString("Dimanche", headerTextFont, headerTextBrush, new Point(100 + dayWidth / 2 + dayWidth * 6 - (int.Parse(headerTextFont.Size.ToString()) * "Dimanche".Length / 2), 25));

            g.DrawString("00:00", headerTextFont, headerTextBrush, new Point(23, 70 + hoursHeight / 2 - int.Parse(headerTextFont.Size.ToString()) / 2));
            g.DrawString("01:00", headerTextFont, headerTextBrush, new Point(23, 70 + hoursHeight / 2 + hoursHeight - int.Parse(headerTextFont.Size.ToString()) / 2));
            g.DrawString("02:00", headerTextFont, headerTextBrush, new Point(23, 70 + hoursHeight / 2 + hoursHeight * 2 - int.Parse(headerTextFont.Size.ToString()) / 2));
            g.DrawString("03:00", headerTextFont, headerTextBrush, new Point(23, 70 + hoursHeight / 2 + hoursHeight * 3 - int.Parse(headerTextFont.Size.ToString()) / 2));
            g.DrawString("04:00", headerTextFont, headerTextBrush, new Point(23, 70 + hoursHeight / 2 + hoursHeight * 4 - int.Parse(headerTextFont.Size.ToString()) / 2));
            g.DrawString("05:00", headerTextFont, headerTextBrush, new Point(23, 70 + hoursHeight / 2 + hoursHeight * 5 - int.Parse(headerTextFont.Size.ToString()) / 2));
            g.DrawString("06:00", headerTextFont, headerTextBrush, new Point(23, 70 + hoursHeight / 2 + hoursHeight * 6 - int.Parse(headerTextFont.Size.ToString()) / 2));
            g.DrawString("07:00", headerTextFont, headerTextBrush, new Point(23, 70 + hoursHeight / 2 + hoursHeight * 7 - int.Parse(headerTextFont.Size.ToString()) / 2));
            g.DrawString("08:00", headerTextFont, headerTextBrush, new Point(23, 70 + hoursHeight / 2 + hoursHeight * 8 - int.Parse(headerTextFont.Size.ToString()) / 2));
            g.DrawString("09:00", headerTextFont, headerTextBrush, new Point(23, 70 + hoursHeight / 2 + hoursHeight * 9 - int.Parse(headerTextFont.Size.ToString()) / 2));
            g.DrawString("10:00", headerTextFont, headerTextBrush, new Point(23, 70 + hoursHeight / 2 + hoursHeight * 10 - int.Parse(headerTextFont.Size.ToString()) / 2));
            g.DrawString("11:00", headerTextFont, headerTextBrush, new Point(23, 70 + hoursHeight / 2 + hoursHeight * 11 - int.Parse(headerTextFont.Size.ToString()) / 2));
            g.DrawString("12:00", headerTextFont, headerTextBrush, new Point(23, 70 + hoursHeight / 2 + hoursHeight * 12 - int.Parse(headerTextFont.Size.ToString()) / 2));
            g.DrawString("13:00", headerTextFont, headerTextBrush, new Point(23, 70 + hoursHeight / 2 + hoursHeight * 13 - int.Parse(headerTextFont.Size.ToString()) / 2));
            g.DrawString("14:00", headerTextFont, headerTextBrush, new Point(23, 70 + hoursHeight / 2 + hoursHeight * 14 - int.Parse(headerTextFont.Size.ToString()) / 2));
            g.DrawString("15:00", headerTextFont, headerTextBrush, new Point(23, 70 + hoursHeight / 2 + hoursHeight * 15 - int.Parse(headerTextFont.Size.ToString()) / 2));
            g.DrawString("16:00", headerTextFont, headerTextBrush, new Point(23, 70 + hoursHeight / 2 + hoursHeight * 16 - int.Parse(headerTextFont.Size.ToString()) / 2));
            g.DrawString("17:00", headerTextFont, headerTextBrush, new Point(23, 70 + hoursHeight / 2 + hoursHeight * 17 - int.Parse(headerTextFont.Size.ToString()) / 2));
            g.DrawString("18:00", headerTextFont, headerTextBrush, new Point(23, 70 + hoursHeight / 2 + hoursHeight * 18 - int.Parse(headerTextFont.Size.ToString()) / 2));
            g.DrawString("19:00", headerTextFont, headerTextBrush, new Point(23, 70 + hoursHeight / 2 + hoursHeight * 19 - int.Parse(headerTextFont.Size.ToString()) / 2));
            g.DrawString("20:00", headerTextFont, headerTextBrush, new Point(23, 70 + hoursHeight / 2 + hoursHeight * 20 - int.Parse(headerTextFont.Size.ToString()) / 2));
            g.DrawString("21:00", headerTextFont, headerTextBrush, new Point(23, 70 + hoursHeight / 2 + hoursHeight * 21 - int.Parse(headerTextFont.Size.ToString()) / 2));
            g.DrawString("22:00", headerTextFont, headerTextBrush, new Point(23, 70 + hoursHeight / 2 + hoursHeight * 22 - int.Parse(headerTextFont.Size.ToString()) / 2));
            g.DrawString("23:00", headerTextFont, headerTextBrush, new Point(23, 70 + hoursHeight / 2 + hoursHeight * 23 - int.Parse(headerTextFont.Size.ToString()) / 2));

            DateTime now = DateTime.Now;

            DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;

            System.Globalization.Calendar cal = dfi.Calendar;

            foreach (Event e in events)
            {
                if (now.Year == e.Beginning.Year)
                {
                    if (cal.GetWeekOfYear(now, dfi.CalendarWeekRule, dfi.FirstDayOfWeek) == cal.GetWeekOfYear(e.Beginning, dfi.CalendarWeekRule, dfi.FirstDayOfWeek))
                    {

                        Rectangle r;

                        switch (e.Beginning.DayOfWeek)
                        {
                            case DayOfWeek.Monday:
                                r = new Rectangle(102 + int.Parse(linesPen.Width.ToString()), 70 + e.Beginning.Hour * hoursHeight + e.Beginning.Minute * hoursHeight / 60, dayWidth - 4 - int.Parse(linesPen.Width.ToString()), e.During.Hours * hoursHeight + e.During.Minutes * hoursHeight / 60);
                                g.FillRectangle(new SolidBrush(e.Color), r);
                                e.BeginPoint = new Point(102 + int.Parse(linesPen.Width.ToString()), 70 + e.Beginning.Hour * hoursHeight + e.Beginning.Minute * hoursHeight / 60);
                                e.EndPoint = new Point(102 + int.Parse(linesPen.Width.ToString()) + dayWidth - 4 - int.Parse(linesPen.Width.ToString()), 70 + e.Beginning.Hour * hoursHeight + e.Beginning.Minute * hoursHeight / 60 + e.During.Hours * hoursHeight + e.During.Minutes * hoursHeight / 60);
                                if (r.Height >= (int)eventTextFont.Size + 10)
                                {
                                    char[] cs = e.Title.ToCharArray();

                                    int maxChar = (r.Width - 10) / (int)eventTextFont.Size;

                                    if (cs.Length <= maxChar)
                                    {
                                        g.DrawString(e.Title, eventTextFont, eventTextBrush, new Point(r.X + 10, r.Y + 10));
                                    }
                                    else
                                    {
                                        string print = "";

                                        for(int i = 0; i < maxChar; i++)
                                        {
                                            print += cs[i];
                                        }

                                        print += "...";

                                        g.DrawString(print, eventTextFont, eventTextBrush, new Point(r.X + 10, r.Y + 10));
                                    }
                                }
                                break;
                            case DayOfWeek.Tuesday:
                                r = new Rectangle(102 + int.Parse(linesPen.Width.ToString()) + dayWidth, 70 + e.Beginning.Hour * hoursHeight + e.Beginning.Minute * hoursHeight / 60, dayWidth - 4 - int.Parse(linesPen.Width.ToString()), e.During.Hours * hoursHeight + e.During.Minutes * hoursHeight / 60);
                                g.FillRectangle(new SolidBrush(e.Color), r);
                                e.BeginPoint = new Point(102 + int.Parse(linesPen.Width.ToString()) + dayWidth, 70 + e.Beginning.Hour * hoursHeight + e.Beginning.Minute * hoursHeight / 60);
                                e.EndPoint = new Point(102 + int.Parse(linesPen.Width.ToString()) + dayWidth * 2 - 4 - int.Parse(linesPen.Width.ToString()), 70 + e.Beginning.Hour * hoursHeight + e.Beginning.Minute * hoursHeight / 60 + e.During.Hours * hoursHeight + e.During.Minutes * hoursHeight / 60);
                                if (r.Height >= (int)eventTextFont.Size + 10)
                                {
                                    char[] cs = e.Title.ToCharArray();

                                    int maxChar = (r.Width - 10) / (int)eventTextFont.Size;

                                    if (cs.Length <= maxChar)
                                    {
                                        g.DrawString(e.Title, eventTextFont, eventTextBrush, new Point(r.X + 10, r.Y + 10));
                                    }
                                    else
                                    {
                                        string print = "";

                                        for (int i = 0; i < maxChar; i++)
                                        {
                                            print += cs[i];
                                        }

                                        print += "...";

                                        g.DrawString(print, eventTextFont, eventTextBrush, new Point(r.X + 10, r.Y + 10));
                                    }
                                }
                                break;
                            case DayOfWeek.Wednesday:
                                r = new Rectangle(102 + int.Parse(linesPen.Width.ToString()) + dayWidth * 2, 70 + e.Beginning.Hour * hoursHeight + e.Beginning.Minute * hoursHeight / 60, dayWidth - 4 - int.Parse(linesPen.Width.ToString()), e.During.Hours * hoursHeight + e.During.Minutes * hoursHeight / 60);
                                g.FillRectangle(new SolidBrush(e.Color), r);
                                e.BeginPoint = new Point(102 + int.Parse(linesPen.Width.ToString()) + dayWidth * 2 , 70 + e.Beginning.Hour * hoursHeight + e.Beginning.Minute * hoursHeight / 60);
                                e.EndPoint = new Point(102 + int.Parse(linesPen.Width.ToString()) + dayWidth * 3 - 4 - int.Parse(linesPen.Width.ToString()), 70 + e.Beginning.Hour * hoursHeight + e.Beginning.Minute * hoursHeight / 60 + e.During.Hours * hoursHeight + e.During.Minutes * hoursHeight / 60);
                                if (r.Height >= (int)eventTextFont.Size + 10)
                                {
                                    char[] cs = e.Title.ToCharArray();

                                    int maxChar = (r.Width - 10) / (int)eventTextFont.Size;

                                    if (cs.Length <= maxChar)
                                    {
                                        g.DrawString(e.Title, eventTextFont, eventTextBrush, new Point(r.X + 10, r.Y + 10));
                                    }
                                    else
                                    {
                                        string print = "";

                                        for (int i = 0; i < maxChar; i++)
                                        {
                                            print += cs[i];
                                        }

                                        print += "...";

                                        g.DrawString(print, eventTextFont, eventTextBrush, new Point(r.X + 10, r.Y + 10));
                                    }
                                }
                                break;
                            case DayOfWeek.Thursday:
                                r = new Rectangle(102 + int.Parse(linesPen.Width.ToString()) + dayWidth * 3, 70 + e.Beginning.Hour * hoursHeight + e.Beginning.Minute * hoursHeight / 60, dayWidth - 4 - int.Parse(linesPen.Width.ToString()), e.During.Hours * hoursHeight + e.During.Minutes * hoursHeight / 60);
                                g.FillRectangle(new SolidBrush(e.Color), r);
                                e.BeginPoint = new Point(102 + int.Parse(linesPen.Width.ToString()) + dayWidth * 3, 70 + e.Beginning.Hour * hoursHeight + e.Beginning.Minute * hoursHeight / 60);
                                e.EndPoint = new Point(102 + int.Parse(linesPen.Width.ToString()) + dayWidth * 4 - 4 - int.Parse(linesPen.Width.ToString()), 70 + e.Beginning.Hour * hoursHeight + e.Beginning.Minute * hoursHeight / 60 + e.During.Hours * hoursHeight + e.During.Minutes * hoursHeight / 60);
                                if (r.Height >= (int)eventTextFont.Size + 10)
                                {
                                    char[] cs = e.Title.ToCharArray();

                                    int maxChar = (r.Width - 10) / (int)eventTextFont.Size;

                                    if (cs.Length <= maxChar)
                                    {
                                        g.DrawString(e.Title, eventTextFont, eventTextBrush, new Point(r.X + 10, r.Y + 10));
                                    }
                                    else
                                    {
                                        string print = "";

                                        for (int i = 0; i < maxChar; i++)
                                        {
                                            print += cs[i];
                                        }

                                        print += "...";

                                        g.DrawString(print, eventTextFont, eventTextBrush, new Point(r.X + 10, r.Y + 10));
                                    }
                                }
                                break;
                            case DayOfWeek.Friday:
                                r = new Rectangle(102 + int.Parse(linesPen.Width.ToString()) + dayWidth * 4, 70 + e.Beginning.Hour * hoursHeight + e.Beginning.Minute * hoursHeight / 60, dayWidth - 4 - int.Parse(linesPen.Width.ToString()), e.During.Hours * hoursHeight + e.During.Minutes * hoursHeight / 60);
                                g.FillRectangle(new SolidBrush(e.Color), r);
                                e.BeginPoint = new Point(102 + int.Parse(linesPen.Width.ToString()) + dayWidth * 4, 70 + e.Beginning.Hour * hoursHeight + e.Beginning.Minute * hoursHeight / 60);
                                e.EndPoint = new Point(102 + int.Parse(linesPen.Width.ToString()) + dayWidth * 5 - 4 - int.Parse(linesPen.Width.ToString()), 70 + e.Beginning.Hour * hoursHeight + e.Beginning.Minute * hoursHeight / 60 + e.During.Hours * hoursHeight + e.During.Minutes * hoursHeight / 60);
                                if (r.Height >= (int)eventTextFont.Size + 10)
                                {
                                    char[] cs = e.Title.ToCharArray();

                                    int maxChar = (r.Width - 10) / (int)eventTextFont.Size;

                                    if (cs.Length <= maxChar)
                                    {
                                        g.DrawString(e.Title, eventTextFont, eventTextBrush, new Point(r.X + 10, r.Y + 10));
                                    }
                                    else
                                    {
                                        string print = "";

                                        for (int i = 0; i < maxChar; i++)
                                        {
                                            print += cs[i];
                                        }

                                        print += "...";

                                        g.DrawString(print, eventTextFont, eventTextBrush, new Point(r.X + 10, r.Y + 10));
                                    }
                                }
                                break;
                            case DayOfWeek.Saturday:
                                r = new Rectangle(102 + int.Parse(linesPen.Width.ToString()) + dayWidth * 5, 70 + e.Beginning.Hour * hoursHeight + e.Beginning.Minute * hoursHeight / 60, dayWidth - 4 - int.Parse(linesPen.Width.ToString()), e.During.Hours * hoursHeight + e.During.Minutes * hoursHeight / 60);
                                g.FillRectangle(new SolidBrush(e.Color), r);
                                e.BeginPoint = new Point(102 + int.Parse(linesPen.Width.ToString()) + dayWidth * 5, 70 + e.Beginning.Hour * hoursHeight + e.Beginning.Minute * hoursHeight / 60);
                                e.EndPoint = new Point(102 + int.Parse(linesPen.Width.ToString()) + dayWidth * 6 - 4 - int.Parse(linesPen.Width.ToString()), 70 + e.Beginning.Hour * hoursHeight + e.Beginning.Minute * hoursHeight / 60 + e.During.Hours * hoursHeight + e.During.Minutes * hoursHeight / 60);
                                if (r.Height >= (int)eventTextFont.Size + 10)
                                {
                                    char[] cs = e.Title.ToCharArray();

                                    int maxChar = (r.Width - 10) / (int)eventTextFont.Size;

                                    if (cs.Length <= maxChar)
                                    {
                                        g.DrawString(e.Title, eventTextFont, eventTextBrush, new Point(r.X + 10, r.Y + 10));
                                    }
                                    else
                                    {
                                        string print = "";

                                        for (int i = 0; i < maxChar; i++)
                                        {
                                            print += cs[i];
                                        }

                                        print += "...";

                                        g.DrawString(print, eventTextFont, eventTextBrush, new Point(r.X + 10, r.Y + 10));
                                    }
                                }
                                break;
                            case DayOfWeek.Sunday:
                                r = new Rectangle(102 + int.Parse(linesPen.Width.ToString()) + dayWidth * 6, 70 + e.Beginning.Hour * hoursHeight + e.Beginning.Minute * hoursHeight / 60, dayWidth - 4 - int.Parse(linesPen.Width.ToString()), e.During.Hours * hoursHeight + e.During.Minutes * hoursHeight / 60);
                                g.FillRectangle(new SolidBrush(e.Color), r);
                                e.BeginPoint = new Point(102 + int.Parse(linesPen.Width.ToString()) + dayWidth * 6, 70 + e.Beginning.Hour * hoursHeight + e.Beginning.Minute * hoursHeight / 60);
                                e.EndPoint = new Point(102 + int.Parse(linesPen.Width.ToString()) + dayWidth * 7 - 4 - int.Parse(linesPen.Width.ToString()), 70 + e.Beginning.Hour * hoursHeight + e.Beginning.Minute * hoursHeight / 60 + e.During.Hours * hoursHeight + e.During.Minutes * hoursHeight / 60);
                                if (r.Height >= (int)eventTextFont.Size + 10)
                                {
                                    char[] cs = e.Title.ToCharArray();

                                    int maxChar = (r.Width - 10) / (int)eventTextFont.Size;

                                    if (cs.Length <= maxChar)
                                    {
                                        g.DrawString(e.Title, eventTextFont, eventTextBrush, new Point(r.X + 10, r.Y + 10));
                                    }
                                    else
                                    {
                                        string print = "";

                                        for (int i = 0; i < maxChar; i++)
                                        {
                                            print += cs[i];
                                        }

                                        print += "...";

                                        g.DrawString(print, eventTextFont, eventTextBrush, new Point(r.X + 10, r.Y + 10));
                                    }
                                }
                                break;
                        }
                    }
                }
            }

            g.Dispose();
        }

        private void initDays(int width)
        {
            dayWidth = int.Parse((width / 7).ToString());

            linesPen = new Pen(sepColor, sepThickness);

            for (int i = 0; i < 7; i++)
            {
                Point topP = new Point(100 + i * dayWidth, 0);
                Point botP = new Point(100 + i * dayWidth, panel1.Height);

                Line l = new Line(topP, botP);

                verticalLines.Add(l);
            }
        }

        private void initHours(int height)
        {
            hoursHeight = int.Parse((height / 24).ToString());

            linesPen = new Pen(sepColor, sepThickness);

            for (int i = 0; i < 24; i++)
            {
                Point leftP = new Point(0 , 70 + i * hoursHeight);
                Point rightP = new Point(panel1.Width, 70 + i * hoursHeight);

                Line l = new Line(leftP, rightP);

                horizontalLines.Add(l);
            }
        }



        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.MinimumSize = new System.Drawing.Size(1150, 1320);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1150, 1320);
            this.panel1.TabIndex = 0;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            this.panel1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDoubleClick);
            this.panel1.Resize += new System.EventHandler(this.panel1_Resize);
            // 
            // timer1
            // 
            this.timer1.Interval = 60000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Calendar
            // 
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Controls.Add(this.panel1);
            this.Name = "Calendar";
            this.Size = new System.Drawing.Size(1150, 1320);
            this.SizeChanged += new System.EventHandler(this.Calendar_SizeChanged);
            this.ResumeLayout(false);

        }

        //******************** EVENTS ********************//
        //************************************************//

        private void Calendar_SizeChanged(object sender, EventArgs e)
        {
            if(this.Height >= panel1.MinimumSize.Height)
            {
                panel1.Height = this.Height;

                //TODO
                //
                //Add size change for days and hours
                //
                //
            }

            if (this.Width >= panel1.MinimumSize.Width)
            {
                panel1.Width = this.Width;

                //TODO
                //
                //Add size change for days and hours
                //
                //
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            updateGraphics();
            updateCursor();
        }

        private void panel1_Resize(object sender, EventArgs e)
        {
            verticalLines.Clear();
            horizontalLines.Clear();

            initDays(panel1.Width - 100);
            initHours(panel1.Height - 70);

            panel1.Invalidate();
        }

        private void panel1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;

            System.Globalization.Calendar cal = dfi.Calendar;

            foreach (Event ev in events)
            {
                if (cal.GetWeekOfYear(ev.Beginning, dfi.CalendarWeekRule, dfi.FirstDayOfWeek) == cal.GetWeekOfYear(DateTime.Now, dfi.CalendarWeekRule, dfi.FirstDayOfWeek))
                {
                    if (ev.BeginPoint.X <= e.X && ev.BeginPoint.Y <= e.Y && ev.EndPoint.X >= e.X && ev.EndPoint.Y >= e.Y)
                    {
                        EventClickEvent(ev.ID);
                    }
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            verticalLines.Clear();
            horizontalLines.Clear();

            initDays(panel1.Width - 100);
            initHours(panel1.Height - 70);

            panel1.Invalidate();
        }

        //************************************************//
        //******************** EVENTS ********************//

    }
}
