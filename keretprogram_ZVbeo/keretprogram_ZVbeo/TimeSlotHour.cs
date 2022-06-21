using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace keretprogram_ZVbeo
{
    class TimeSlotHour : TimeSlot
    {
        public int Hour { get; private set; }

        public TimeSlotHour(string _date, string _hour, bool i) : base(_date,i)
        {
            Hour = Int32.Parse(_hour.Split(":")[0]);
        }

        public string toString()
        {
            return ("TSH: " + Date.ToString("yyyy'.'MM'.'dd") + " " + Hour);
        }

        public string toCSVString()
        {
            return ("TSH;" + Date.ToString("yyyy'.'MM'.'dd") + ";" + Hour);
        }

        public string toSCSVString()
        {
            return (Date.ToString("yyyy'.'MM'.'dd") + ";" + Hour);
        }

        public bool Matches(TimeSlotHour t)
        {
            if (t.Date == Date && t.Hour == Hour) return true;
            return false;
        }
    }
}
