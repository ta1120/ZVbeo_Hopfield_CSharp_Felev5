using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace keretprogram_ZVbeo
{
    abstract class Person
    {
        public int ID { get; protected set; }
        public string Name { get; protected set; }
        public int Type { get; protected set; }

        public Person(string _name)
        {
            ID = InputModel.GetInstance().GetNextPersonID();
            Name = _name;
        }

        public Person()
        {
            ID = 0;
            Name = "Dummy";
        }

        public string toString()
        {
            if(Type == 1)
            {
                Student s = (Student)this;
                return s.toString();
            }
            if(Type == 2)
            {
                Instructor i = (Instructor)this;
                return i.toString();
            }
            else return null;
        }

        public bool isPresident()
        {
            if(Type == 2)
            {
                Instructor i = (Instructor)this;
                if (i.President) return true;
            }
            return false;
        }
    }
}
