
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;

namespace keretprogram_ZVbeo
{
    class XlsxReader //reference used:https://coderwall.com/p/app3ya/read-excel-file-in-c
                     //some of the code was copied from the article, since I do not know much about working with these kinds of files
    {
        //----------------------
        bool PrintDebug = false;
        //----------------------

        string filename;

        public XlsxReader(string fileName)
        {
            filename = fileName.Split('.')[0]+".xlsx";
            InputModel model = InputModel.GetInstance();
            //due to the way I have created some of the model's constructors, data from the sheets has to be read in the following order:
            //Instructors->Courses->Students
            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(@fileName);

            if (PrintDebug) Console.WriteLine("Reading instructors\n");
            //Reading instructors
            //-------------------------------------------------------------------------------------------------------------------------
            Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[2];
            Excel.Range xlRange = xlWorksheet.UsedRange;

            int rowCount = xlRange.Rows.Count;
            int colCount = xlRange.Columns.Count;
            if (PrintDebug) Console.WriteLine("Rows: " + rowCount + " Cols: " + colCount + "\n");

            if (PrintDebug) Console.WriteLine("Registering timeslots\n");
            //registering timeslots in the model
            List<TimeSlotHour> timeSlotsAv = new List<TimeSlotHour>();
            string day="";

            for (int j = 7; j <= colCount; j++)
            {
                for (int i = 1; i <= 2; i++)
                {
                    if (xlRange.Cells[i, j] != null && xlRange.Cells[i, j].Value2 != null)
                    {
                        if (i == 1 && (j+3) % 10 == 0) day = xlRange.Cells[i, j].Value2.ToString();
                        if (i == 2)
                        {
                            string hour = xlRange.Cells[i, j].Value2.ToString();
                            timeSlotsAv.Add(new TimeSlotHour(day, hour,true));
                            if (PrintDebug) Console.WriteLine("Adding timeslot Hour " + day + " " + hour + "\n");
                          /*
                            for (int x = 0; x < 12; x++) 
                            {
                                model.AddTimeSlot(new TimeSlotFiveMin(day, hour, x));
                                if (PrintDebug) Console.WriteLine("Adding timeslot 5min " + day + " " + hour + " " + x + "\n");
                            }
                          */
                        }
                    }
                }
            }
            foreach (TimeSlotHour t in timeSlotsAv) model.AddTimeSlot(t);

            string iname;
            bool pres;
            bool member;
            bool secr;
            bool cs;
            bool ee;

            for (int i = 3; i <= rowCount; i++)
            {
                iname = ""; pres = false; member = false; secr = false; cs = false; ee = false;
                Instructor newInst = new Instructor();
                bool created = false;

                for (int j = 1; j <= colCount; j++)
                {
                    if (xlRange.Cells[i, j] != null)
                    {
                        if (j == 1 && xlRange.Cells[i, j].Value2 != null) iname = xlRange.Cells[i, j].Value2.ToString();
                        else if (j == 2) { if (xlRange.Cells[i, j].Value2 != null) pres = true; }
                        else if (j == 3) { if (xlRange.Cells[i, j].Value2 != null) member = true; }
                        else if (j == 4) { if (xlRange.Cells[i, j].Value2 != null) secr = true; }
                        else if (j == 5) { if (xlRange.Cells[i, j].Value2 != null) cs = true; }
                        else if (j == 6) { if (xlRange.Cells[i, j].Value2 != null) ee = true; }
                        else
                        {
                            if (!created) 
                            {
                                newInst = new Instructor(iname, pres, member, secr, cs, ee); created = true;
                                if (PrintDebug) Console.WriteLine(newInst.toString());
                            }
                            TimeSlotHour ts = timeSlotsAv[j - 7];
                            if (xlRange.Cells[i, j].Value2 != null) newInst.AddAvailabilitySlot(ts, true);
                            else newInst.AddAvailabilitySlot(ts, false);
                        }
                    }
                }
                model.AddInstructor(newInst);
            }

            if (PrintDebug) Console.WriteLine("Reading courses\n");
            //Reading courses
            //-------------------------------------------------------------------------------------------------------------------------
            xlWorksheet = xlWorkbook.Sheets[3];
            xlRange = xlWorksheet.UsedRange;

            rowCount = xlRange.Rows.Count;
            colCount = xlRange.Columns.Count;
            if (PrintDebug) Console.WriteLine("Rows: " + rowCount + " Cols: " + colCount + "\n");


            string code;
            string cname;
            Instructor inst;

            for (int j = 1; j <= colCount; j++)
            {
                code = "";
                cname = "";
                inst = new Instructor();
                Course newCourse = new Course("", "");

                for (int i = 1; i <= rowCount; i++)
                {
                    if (xlRange.Cells[i, j] != null && xlRange.Cells[i, j].Value2 != null)
                    {
                        if(i == 1) code = xlRange.Cells[i, j].Value2.ToString();
                        if (i == 2) { cname = xlRange.Cells[i, j].Value2.ToString(); newCourse = new Course(code, cname); }
                        else
                        {
                            inst = model.GetInstructorByName(xlRange.Cells[i, j].Value2.ToString());
                            if (inst != null) newCourse.AddInstructor(inst);
                        }
                    }  
                }
                model.AddCourse(newCourse);
            }

            if (PrintDebug) Console.WriteLine("Reading students\n");
            //Reading students
            //-------------------------------------------------------------------------------------------------------------------------
            xlWorksheet = xlWorkbook.Sheets[1];
            xlRange = xlWorksheet.UsedRange;

            rowCount = xlRange.Rows.Count;
            colCount = xlRange.Columns.Count;
            if (PrintDebug) Console.WriteLine("Rows: " + rowCount + " Cols: " + colCount + "\n");

            string sname;
            string neptun;
            string dgrlvl;
            string program;
            string sup;
            string c1;
            string c2;

            for (int i = 2; i <= rowCount; i++)
            {
                sname = "";
                neptun = "";
                dgrlvl = "";
                program = "";
                sup = "";
                c1 = "";
                c2 = "";

                for (int j = 1; j <= colCount; j++)
                {
                    if (xlRange.Cells[i, j] != null && xlRange.Cells[i, j].Value2 != null)
                    {
                        if (j == 1) sname = xlRange.Cells[i, j].Value2.ToString();
                        else if (j == 2) neptun = xlRange.Cells[i, j].Value2.ToString();
                        else if (j == 3) dgrlvl = xlRange.Cells[i, j].Value2.ToString();
                        else if (j == 4) program = xlRange.Cells[i, j].Value2.ToString();
                        else if (j == 5) sup = xlRange.Cells[i, j].Value2.ToString();
                        else if (j == 7) c1 = xlRange.Cells[i, j].Value2.ToString();
                        else if (j == 9) c2 = xlRange.Cells[i, j].Value2.ToString();
                    } 
                }
                Student newStudent = new Student(sname, neptun, dgrlvl, program, sup, c1, c2);
                model.AddStudent(newStudent);
                if (PrintDebug) Console.WriteLine(newStudent.toString() + "\n");
            }

            GC.Collect();
            GC.WaitForPendingFinalizers();

            Marshal.ReleaseComObject(xlRange);
            Marshal.ReleaseComObject(xlWorksheet);

            xlWorkbook.Close();
            Marshal.ReleaseComObject(xlWorkbook);

            xlApp.Quit();
            Marshal.ReleaseComObject(xlApp);
        }

        
        
    }
}
