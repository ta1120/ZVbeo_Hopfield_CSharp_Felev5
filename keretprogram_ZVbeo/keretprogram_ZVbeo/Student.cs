using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace keretprogram_ZVbeo
{
    class Student : Person
    {
        public string Neptun { get; private set; }
        public string DegreeLvl { get; private set; }
        public string Program { get; private set; }
        public Instructor Supervisor { get; private set; }
        public Course Course1 { get; private set; }
        public Course Course2 { get; private set; }

        public Student(string _name,string _neptun,string _degreeLvl,string _program,string _supervisor,string _courseCode1, string _courseCode2) : base(_name)
        {
            Type = 1;
            InputModel model = InputModel.GetInstance();
            Neptun = _neptun;
            DegreeLvl = _degreeLvl;
            Program = _program;
            Supervisor = model.GetInstructorByName(_supervisor);
            if (Supervisor == null) Supervisor = new Instructor();
            Course1 = model.GetCourseByCode(_courseCode1);
            if (Course1 == null) Course2 = new Course("ProblemC1", "n/a");
            Course2 = model.GetCourseByCode(_courseCode2);
            if (Course2 == null) Course2 = new Course("n/a", "n/a");
        }

        public string toString()
        {
            return ("Student: " + Name + " " + Neptun + " " + DegreeLvl + " " + Program + " " + Supervisor.Name + " " + Course1.CourseCode + " " + Course2.CourseCode); 
        }

        public string toCSVString()
        {
            return ("Student;" + Name + ";" + Neptun + ";" + DegreeLvl + ";" + Program + ";" + Supervisor.Name + ";" + Course1.CourseCode + ";" + Course2.CourseCode);
        }
    }
}
