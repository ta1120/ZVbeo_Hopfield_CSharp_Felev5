using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace keretprogram_ZVbeo
{
    class CSVReader
    {
        string filename;

        public CSVReader(string fileName)
        {
            InputModel model = InputModel.GetInstance();

            filename = fileName;

            StreamReader f = null;

            if (File.Exists(filename.Split('.')[0] + ".csv"))
            {
                bool TSHAdded = false;

                f = new StreamReader(File.OpenRead(filename.Split('.')[0] + ".csv"));

                while (!f.EndOfStream)
                {
                    string[] data = f.ReadLine().Split(';');

                    if (data[0].Equals("Instructor"))
                    {
                        model.AddInstructor(new Instructor(data[1], bool.Parse(data[2]), bool.Parse(data[3]), bool.Parse(data[4]), bool.Parse(data[5]), bool.Parse(data[6])));
                    }
                    else if (data[0].Equals("Course"))
                    {
                        Course newCourse = new Course(data[1], data[2]);
                        for (int i = 3; i < data.Length; i++) newCourse.AddInstructor(model.GetInstructorByName(data[i]));
                        model.AddCourse(newCourse);
                    }
                    else if (data[0].Equals("Student"))
                    {
                        model.AddStudent(new Student(data[1], data[2], data[3], data[4], data[5], data[6], data[7]));
                    }
                    else if (data[0].Equals("Availability"))
                    {
                        Instructor inst = model.GetInstructorByName(data[1]);
                        //int n = (data.Length - 2) / 3;
                        for (int i = 2; i < data.Length; i += 3)
                        {
                            inst.AddAvailabilitySlot(new TimeSlotHour(data[i], data[i + 1],false), bool.Parse(data[i + 2]));
                            if (!TSHAdded) model.AddTimeSlot(new TimeSlotHour(data[i], data[i + 1],true));
                        }
                        if (!TSHAdded) TSHAdded = true;
                    }
                }
                f.Close();
            }
        }
    }
}
