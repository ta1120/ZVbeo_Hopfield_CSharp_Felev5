using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace keretprogram_ZVbeo
{
    class OutputModel
    {
        List<Exam> exams;
        
        public void SortExams()
        {
            exams.Sort();
        }

        public void PrintExamsToStdOut()
        {
            SortExams();
            foreach (Exam e in exams) Console.WriteLine(e.ToString());
        }

        public void WriteToCSV()
        {
            SortExams();
            StreamWriter csv = File.CreateText(DateTime.Now.ToString() + ".csv");
            foreach (Exam e in exams) csv.WriteLine(e.ToCSVString());
            csv.Close();
        }
    }

    
}
