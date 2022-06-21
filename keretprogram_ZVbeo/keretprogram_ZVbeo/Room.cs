using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace keretprogram_ZVbeo
{
    class Room
    {
        public int ID { get; private set; }
        public string Name { get; private set; }

        public Room(int _id, string _name)
        {
            ID = _id;
            Name = _name;
        }

        public string toString()
        {
            return ("Room: " + Name);
        }

        public string toCSVString()
        {
            return ("Roon;" + Name);
        }
    }
}
