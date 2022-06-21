using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace keretprogram_ZVbeo
{
    abstract class TimeSlot
    {
        public int ID { get; protected set; }
        public DateTime Date { get; protected set; }

        public TimeSlot(string _date,bool i)
        {
            if (i) ID = InputModel.GetInstance().GetNextTimeSlotID();
            else ID = -1;
            //Console.WriteLine(ID + " handed out.\n");
            Date = DateTime.Parse(_date.Split(" ")[0]);
        }
    }
}
