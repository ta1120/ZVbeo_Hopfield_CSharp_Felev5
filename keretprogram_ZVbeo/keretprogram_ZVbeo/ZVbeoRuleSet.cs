using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace keretprogram_ZVbeo
{
    class SchedulePresidents : RuleSet
    {
        public SchedulePresidents(NeuronModel nm) : base(nm) 
        {
            AddRule(new FillSlotsNoCollision());
            AddRule(new CheckAvailability());
        }

        //One president per timeslot per room
        class FillSlotsNoCollision : Rule
        {
            public int[,,,,,] getWeightMatrix(NeuronModel nm)
            {
                int size = nm.size();
                Neuron_TSH[,,] neurons = (Neuron_TSH[,,])nm.getNeurons();
                

                InputModel model = InputModel.GetInstance();

                int pCount = model.GetInstructors().Count + model.GetStudents().Count;
                int tCount = model.GetTimeSlots().Count;
                int rCount = model.GetRooms().Count;

                int[,,,,,] wm = new int[pCount, tCount, rCount, pCount, tCount, rCount];

                //BOOM
                for (int p1 = 0; p1 < pCount; p1++)
                {
                    for (int t1 = 0; t1 < tCount; t1++)
                    {
                        for (int r1 = 0; r1 < rCount; r1++)
                        {
                            for (int p2 = 0; p2 < pCount; p2++)
                            {
                                for (int t2 = 0; t2 < tCount; t2++)
                                {
                                    for (int r2 = 0; r2 < rCount; r2++)
                                    {
                                        if(((p1 == p2) && (t1 == t2)) || ((t1 == t2) && (r1 == r2)) && !(p1 == p2 && t1 == t2 && r1 == r2))
                                        {
                                            Person P1 = model.getPersonByID(p1);
                                            Person P2 = model.getPersonByID(p2);
                                            if (P1.Type == 2 && P2.Type == 2)
                                            {
                                                Instructor I1 = (Instructor)P1;
                                                Instructor I2 = (Instructor)P2;

                                                if (I1.President && I2.President) wm[p1, t1, r1, p2, t2, r2] = -1;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                return wm;
            }
        }

        //Instructors should only be scheduled when available
        class CheckAvailability : Rule
        {
            public int[,,,,,] getWeightMatrix(NeuronModel nm)
            {
                int size = nm.size();
                Neuron_TSH[,,] neurons = (Neuron_TSH[,,])nm.getNeurons();


                InputModel model = InputModel.GetInstance();

                int pCount = model.GetInstructors().Count + model.GetStudents().Count;
                int tCount = model.GetTimeSlots().Count;
                int rCount = model.GetRooms().Count;

                int[,,,,,] wm = new int[pCount, tCount, rCount, pCount, tCount, rCount];

               
                for (int p1 = 0; p1 < pCount; p1++)
                {
                    for (int t1 = 0; t1 < tCount; t1++)
                    {
                        for (int r1 = 0; r1 < rCount; r1++)
                        {
                            for (int p2 = 0; p2 < pCount; p2++)
                            {
                                for (int t2 = 0; t2 < tCount; t2++)
                                {
                                    for (int r2 = 0; r2 < rCount; r2++)
                                    {
                                        if (((p1 == p2) && (t1 == t2)) || ((t1 == t2) && (r1 == r2)) && !(p1 == p2 && t1 == t2 && r1 == r2))
                                        {
                                            Person P1 = model.getPersonByID(p1);
                                            if (P1.Type == 2)
                                            {
                                                Instructor I1 = (Instructor)P1;
                                                //Console.WriteLine(I1.IsAvailableAt(model.getTimeSlotByID(t1)).ToString() + "\n");
                                                if (!I1.IsAvailableAt(model.getTimeSlotByID(t1))) wm[p1, t1, r1, p2, t2, r2] = -1;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                return wm;
            }
        }
    }
}
