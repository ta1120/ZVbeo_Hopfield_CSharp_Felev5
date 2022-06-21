using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace keretprogram_ZVbeo
{
    class Instructor : Person
    {
        public bool President { get; private set; }
        public bool Member { get; private set; }
        public bool Secretary { get; private set; }
        public bool CS { get; private set; }
        public bool EE { get; private set; }
        List<KeyValuePair<TimeSlotHour, bool>> availability;

        public Instructor(string _name, bool _pres, bool _memb, bool _secr, bool _cs, bool _ee) : base(_name)
        {
            Type = 2;
            President = _pres;
            Member = _memb;
            Secretary = _secr;
            CS = _cs;
            EE = _ee;
            availability = new List<KeyValuePair<TimeSlotHour, bool>>();
        }

        public Instructor()
        {
            Type = 2;
            Name = "Dummy->problemInstreuctor";
            President = false;
            Member = false;
            Secretary = false;
            CS = false;
            EE = false;
            availability = new List<KeyValuePair<TimeSlotHour, bool>>();
        }

        public void AddAvailabilitySlot(TimeSlotHour t, bool a) 
        {
            foreach (KeyValuePair<TimeSlotHour, bool> ca in availability)
            {
                if (ca.Key.Equals(t)) return;
            }
            availability.Add(new KeyValuePair<TimeSlotHour, bool>(t, a)); 
        }

        public bool IsAvailableAt(TimeSlotHour t)
        {
            foreach (KeyValuePair<TimeSlotHour, bool> ca in availability)
            {
                if (ca.Key.Matches(t)) return ca.Value;
            }
            return false;
        }

        public bool IsAvailableAt(TimeSlotFiveMin t)
        {
            foreach (KeyValuePair<TimeSlotHour, bool> ca in availability)
            {
                if (ca.Key.Hour.Equals(t.Hour)) return ca.Value;
            }
            return false;
        }

        public string toString()
        {
            return ("Instructor: " + Name + " " + President.ToString() + " " + Member.ToString() + " " + Secretary.ToString() + " " + EE.ToString() + " " + CS.ToString());
        }

        public string toCSVString()
        {
            return ("Instructor;" + Name + ";" + President.ToString() + ";" + Member.ToString() + ";" + Secretary.ToString() + ";" + EE.ToString() + ";" + CS.ToString());
        }

        public string avbToString()
        {
            string a = "Availability: " + Name;
            foreach (KeyValuePair<TimeSlotHour, bool> avb in availability) a += " | " + avb.Key.toString() + " " +avb.Value.ToString();
            return a;
        }

        public string avbToCSVString()
        {
            string a = "Availability;" + Name;
            foreach (KeyValuePair<TimeSlotHour, bool> avb in availability) a += ";" + avb.Key.toSCSVString() + ";" + avb.Value.ToString();
            return a;
        }
    }
}
