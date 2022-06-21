using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace keretprogram_ZVbeo
{
    class Course
    {
        public string CourseCode { get; private set; }
        public string Name { get; private set; }
        List<Instructor> instructors;

        public Course(string _code,string _name)
        {
            CourseCode = _code;
            Name = _name;
            instructors = new List<Instructor>();
        }

        public void AddInstructor(Instructor inst) { instructors.Add(inst); }
        public List<Instructor> GetInstructors() { return instructors; }

        public string toString()
        {
            string c = "Course: " + CourseCode + " " + Name;
            foreach (Instructor i in instructors) c += " " + i.Name;
            return c;
        }

        public string toCSVString()
        {
            string c = "Course;" + CourseCode + ";" + Name;
            foreach (Instructor i in instructors) c += ";" + i.Name;
            return c;
        }
    }
}
