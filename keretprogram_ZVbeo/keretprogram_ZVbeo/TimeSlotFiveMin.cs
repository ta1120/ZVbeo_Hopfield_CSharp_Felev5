using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace keretprogram_ZVbeo
{
    class TimeSlotFiveMin : TimeSlotHour
    {
        public int Offset { get; private set; } //5min increments from 0 to 11

        public TimeSlotFiveMin(string _date, string _hour, int _offset,bool i) : base(_date,_hour,i)
        {
            Offset = _offset;
        }

        public int GetMinutes() { return 5 * Offset; }

        public override string ToString()
        {
            return (Hour.ToString() + ":" + GetMinutes().ToString());
        }

        public string toString()
        {
            return ("TSFM: " + Date.ToString("yyyy'.'MM'.'dd") + " " + Hour + " " + Offset);
        }

        public string toCSVString()
        {
            return ("TSFM;" + Date.ToString("yyyy'.'MM'.'dd") + ";" + Hour + ";" + Offset);
        }
    }
}

