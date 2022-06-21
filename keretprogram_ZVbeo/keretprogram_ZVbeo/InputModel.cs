using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace keretprogram_ZVbeo
{
    class InputModel
    {
        private int NextPersonID;
        private int NextTimeSlotID;
        List<Student> students;
        List<TimeSlotHour> timeSlots;
        List<Instructor> instructors;
        List<Course> courses;
        List<Room> rooms;

        private static InputModel instance;

        private static readonly object writeLock = new object();

        private InputModel() 
        {
            NextPersonID = 0;
            NextTimeSlotID = 0;
            students = new List<Student>();
            timeSlots = new List<TimeSlotHour>();
            instructors = new List<Instructor>();
            courses = new List<Course>();
            rooms = new List<Room>();
        }

        public static InputModel GetInstance()
        {
            if (instance == null)
            {
                lock(writeLock)
                {
                    if(instance == null) instance = new InputModel();
                }
            }
            return instance;
        }

        public Instructor GetInstructorByName(string supervisor)
        {
            foreach(Instructor i in instructors)
            {
                if (i.Name.Equals(supervisor)) return i;
            }
            return null;
        }

        public Course GetCourseByCode(string courseCode)
        {
            foreach(Course c in courses)
            {
                if (c.CourseCode.Equals(courseCode)) return c;
            }
            return null;
        }

        public int GetNextPersonID()
        {
            return NextPersonID++;
        }

        public int GetNextTimeSlotID()
        {
            return NextTimeSlotID++;
        }

        public void DebugPrint()
        {
            foreach(Instructor i in instructors)
            {
                Console.WriteLine(i.toString() + "\n");
            }
            foreach (Course c in courses)
            {
                Console.WriteLine(c.toString() + "\n");
            }
            foreach (Student s in students)
            {
                Console.WriteLine(s.toString() + "\n");
            }
            foreach (TimeSlotHour t in timeSlots)
            {
                Console.WriteLine(t.toString() + "\n");
            }
        }

        public void toCSV(string filename)
        {
            if (!File.Exists(filename.Split(".")[0] + ".csv"))
            {
                StreamWriter f = File.CreateText(filename.Split(".")[0] + ".csv");

                foreach (Instructor i in instructors) f.WriteLine(i.toCSVString());

                foreach (Course c in courses) f.WriteLine(c.toCSVString());

                foreach (Student s in students) f.WriteLine(s.toCSVString());

                foreach(Room r in rooms) f.WriteLine(r.toCSVString());

                //foreach (TimeSlotFiveMin t in timeSlots) f.WriteLine(t.toCSVString());

                foreach (Instructor i in instructors) f.WriteLine(i.avbToCSVString());

                f.Close();
            }
        }

        public bool SmartInputFileRead(string file)
        {
            if (File.Exists(file.Split(".")[0] + ".csv")) { Console.WriteLine("Reading from CSV file:\n" + file + "\n"); new CSVReader(file); }
            else if (File.Exists(file.Split(".")[0] + ".xlsx")) { Console.WriteLine("Reading from XLSX file... This may take some time.\n" + file + "\n" + "CSV fájlba írás\n"); new XlsxReader(file); toCSV(file); }
            else
            {
                Console.WriteLine("Missing file\n"); 
                return false;
            }
            return true;
        }

        public void AddStudent(Student s) { students.Add(s); }
        public void AddTimeSlot(TimeSlotHour t) { timeSlots.Add(t); }
        public void AddInstructor(Instructor i) { instructors.Add(i); }
        public void AddCourse(Course c) { courses.Add(c); }
        public void AddRoom(Room r) { rooms.Add(r); }

        public List<Student> GetStudents() { return students; }
        public List<TimeSlotHour> GetTimeSlots() { return timeSlots; }
        public List<Instructor> GetInstructors() { return instructors; }
        public List<Course> GetCourses() { return courses; }
        public List<Room> GetRooms() { return rooms; }
        
        public Person getPersonByID(int _ID)
        {
            /*
            if(_ID < instructors.Count)
            {
                if (instructors[_ID].ID == _ID) return instructors[_ID];
            }
            if (_ID-instructors.Count < students.Count)
            {
                if (students[_ID-instructors.Count].ID == _ID) return students[_ID];
            }
            */

            //Should not come to this point, could use different search algorithm in that case
            foreach (Instructor i in instructors) if (i.ID == _ID) return i;

            foreach (Student s in students) if (s.ID == _ID) return s;
            
            return null;
        }

        public TimeSlotHour getTimeSlotByID(int _ID)
        {
            if (_ID < timeSlots.Count)
            {
                if (timeSlots[_ID].ID == _ID) return timeSlots[_ID];
            }

            //Should not come to this point, could use different search algorithm in that case
            foreach (TimeSlotHour t in timeSlots) { if (t.ID == _ID) return t; }

            return null;
        }

        public Room getRoomByID(int _ID)
        {
            if (_ID < rooms.Count)
            {
                if (rooms[_ID].ID == _ID) return rooms[_ID];
            }

            //Should not come to this point, could use different search algorithm in that case
            foreach (Room r in rooms) if (r.ID == _ID) return r;

            return null;
        }

        public int getSize() { return ((instructors.Count + students.Count) * timeSlots.Count * rooms.Count); }
    }
}
