using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace ColarsisUserControls
{
    public class Event
    {

        //***********************************************//
        //******************** VARS *********************//

        private int id;
        private DateTime begining;
        private DateTime ending;
        private string title;
        private string description;
        private Color color;

        private Point p1;
        private Point p2;

        private TimeSpan duringTime;

        private delegate void EventHandler();
        private event EventHandler EventUpdated;

        //******************** VARS *********************//
        //***********************************************//


        //*********************************************************//
        //******************** GETTER / SETTER ********************//

        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        public Point BeginPoint
        {
            get { return p1; }
            set { p1 = value; }
        }

        public Point EndPoint
        {
            get { return p2; }
            set { p2 = value; }
        }

        public DateTime Beginning
        {
            get { return begining; }
            set { begining = value; updateEvent(); }
        }

        public DateTime Ending
        {
            get { return ending; }
            set { ending = value; updateEvent(); }
        }

        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public Color Color
        {
            get { return color; }
            set { color = value;}
        }

        public TimeSpan During
        {
            get { return duringTime; }
        }

        //******************** GETTER / SETTER ********************//
        //*********************************************************//


        //public Event(string title, string desc, DateTime begin, DateTime end, Color color)
        //{
        //    if (end.CompareTo(begin) < 1)
        //    {
        //        throw new TimeIntervalException("The beginning DateTime couldn't be after the ending DateTime");
        //    }
        //    else
        //    {
        //        this.title = title;
        //        this.description = desc;
        //        this.begining = begin;
        //        this.ending = end;
        //        this.color = color;
        //
        //        updateEvent();
        //    }
        //}

        public Event(int id, string title, string desc, DateTime begin, DateTime end, Color color)
        {
            if (end.CompareTo(begin) < 1)
            {
                throw new TimeIntervalException("The beginning DateTime couldn't be after the ending DateTime");
            }
            else
            {
                this.title = title;
                this.description = desc;
                this.begining = begin;
                this.ending = end;
                this.color = color;
                this.id = id;

                updateEvent();
            }
        }

        public void updateEvent()
        {
            duringTime = ending - begining;
        }
    }
}
