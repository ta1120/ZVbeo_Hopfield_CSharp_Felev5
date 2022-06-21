using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace keretprogram_ZVbeo
{
    class Exam : IComparable<Exam>
    {
        public TimeSlotFiveMin TimeSlot { get; private set; }
        public Room Room { get; private set; }
        public int Length { get; private set; }
        public Student Student { get; private set; }
        public Course Course { get; private set; }
        public Instructor President { get; private set; }
        public Instructor Secretary { get; private set; }
        public Instructor Member { get; private set; }
        public Instructor Examiner1 { get; private set; }
        public Instructor Examiner2 { get; private set; }

        public Exam(TimeSlotFiveMin _timeSlot, int _length, Room _room, Course _course, Student _student, Instructor _president, Instructor _secretary, Instructor _member, Instructor _examiner1, Instructor _examiner2)
        {
            TimeSlot = _timeSlot;
            Length = _length;
            President = _president;
            Room = _room;
            Student = _student;
            Course = _course;
            Secretary = _secretary;
            Member = _member;
            Examiner1 = _examiner1;
            Examiner2 = _examiner2;
        }

        public int CompareTo(Exam _exam)
        {
            if (this.Room.ID < _exam.Room.ID) return 1;
            else if (this.Room.ID < _exam.Room.ID) return -1;
            else
            {
                if (this.TimeSlot.Date < _exam.TimeSlot.Date) return 1;
                else if (this.TimeSlot.Date > _exam.TimeSlot.Date) return -1;
                else
                {
                    if (this.TimeSlot.Hour < _exam.TimeSlot.Hour) return 1;
                    else if (this.TimeSlot.Hour > _exam.TimeSlot.Hour) return -1;
                    else
                    {
                        if (this.TimeSlot.Offset < _exam.TimeSlot.Offset) return 1;
                        else if (this.TimeSlot.Offset > _exam.TimeSlot.Offset) return -1;
                        else return 0;
                    }
                }
            }
        }

        public override string ToString() // csúnya tesztmegoldás
        {
            return (
            "###Exam\n" +
            "Room: " + Room.Name + '\n' +
            "Start: " + TimeSlot.ToString() + '\n' +
            "Length: " + Length.ToString() + " min" + '\n' +
            "Student: " + Student.Name + '\n' +
            "Course: " + Course.Name + '\n' +
            "President: " + President.Name + '\n' +
            "Secretary: " + Secretary.Name + '\n' +
            "Member: " + Member.Name + '\n' +
            "Examiner1: " + Examiner1.Name + '\n' +
            "Examiner2: " + Examiner2.Name + '\n' + '\n'
            );
        }

        public string ToCSVString() // csúnya tesztmegoldás
        {
            return (
            Room.Name + ';' +
            TimeSlot.ToString() + ';' +
            Length.ToString() + " min" + ';' +
            Student.Name + ';' +
            Course.Name + ';' +
            President.Name + ';' +
            Secretary.Name + ';' +
            Member.Name + ';' +
            Examiner1.Name + ';' +
            Examiner2.Name
            );
        }
    }
}
