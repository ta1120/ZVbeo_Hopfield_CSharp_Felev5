using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace keretprogram_ZVbeo
{
    interface Rule
    {
        public int[,,,,,] getWeightMatrix(NeuronModel nm);
    }
    class RuleSet
    {
        List<Rule> rules;

        int[,,,,,] weightMatrix;

        InputModel model;

        NeuronModel neuronModel;

        int size;
        int pCount;
        int tCount;
        int rCount;

        public RuleSet(NeuronModel nm)
        {
            rules = new List<Rule>();
            model = InputModel.GetInstance();
            size = model.getSize();
            neuronModel = nm;
            pCount = model.GetInstructors().Count + model.GetStudents().Count;
            tCount = model.GetTimeSlots().Count;
            rCount = model.GetRooms().Count;

            weightMatrix = new int[pCount,tCount,rCount, pCount, tCount, rCount];
        }

        public void evaluateNet()
        {
            calculateWeightMatrix();
            Console.WriteLine("Iterating neural network...\n");
            int updates = 0;
            Random r = new Random();
            int it = 1;
            while(updates >= 0)
            {
                updates = 0;
                //bool[,,] updated = new bool[pCount,tCount,rCount];
                for (int p1 = 0; p1 < pCount; p1++)
                {
                    for (int t1 = 0; t1 < tCount; t1++)
                    {
                        for (int r1 = 0; r1 < rCount; r1++)
                        {
                            if (updateNeuron((Neuron_TSH)neuronModel.getNeuronAt(p1, t1, r1))) { updates++; }
                        }
                    }
                }
                Console.WriteLine("Iteration " + it + ": "+ "# of updates: " + updates + "\n");
                it++;
                if (updates == 0) updates = -1;
            }
        }

        public void PrintDebug()
        {
            int count = 0;
            for (int t1 = 0; t1 < tCount; t1++)
            {
                for (int r1 = 0; r1 < rCount; r1++)
                {
                    for (int p1 = 0; p1 < pCount; p1++)
                    {
                        Neuron_TSH n = (Neuron_TSH)neuronModel.getNeuronAt(p1,t1,r1);
                        if(n.Value && model.getPersonByID(p1).isPresident())
                        {
                            count++;
                            Console.WriteLine(model.getPersonByID(p1).toString());
                            Console.WriteLine(model.getTimeSlotByID(t1).toString());
                            Console.WriteLine(model.getRoomByID(r1).toString());
                            Console.WriteLine("\n");
                        }
                    }
                }
            }
            Console.WriteLine("Count of selected neurons: " + count + "\n");
        }


        //Only for showcase (2 Pres. rules)
        public void writeToCSV()
        {
            StreamWriter f = File.CreateText("out.csv");

            string line = "ZVbeo";
            foreach (Room r in model.GetRooms()) line += ";" + r.Name;
            f.WriteLine(line);
            Neuron[,,] neurons = neuronModel.getNeurons();
            foreach(TimeSlotHour t in model.GetTimeSlots())
            {
                line = "";
                line += t.Date.ToString("yyyy.MM.dd") + " " + t.Hour + "h";
                foreach (Room r in model.GetRooms()) foreach (Instructor i in model.GetInstructors()) if (i.President && neurons[i.ID, t.ID, r.ID].Value) line += ";" + i.Name;
                f.WriteLine(line);
            }



            f.Close();
        }


        public bool updateNeuron(Neuron_TSH n)
        {
            int sum = 0;
            int[] i = n.getIndexes();
            for (int p1 = 0; p1 < pCount; p1++)
            {
                for (int t1 = 0; t1 < tCount; t1++)
                {
                    for (int r1 = 0; r1 < rCount; r1++)
                    {
                        if(!(p1 == i[0] && t1 == i[1] && r1 ==i[2]))
                        {
                            if(neuronModel.getNeuronAt(p1, t1, r1).Value) sum += weightMatrix[i[0], i[1], i[2], p1, t1, r1];
                        }
                    }
                }
            }
            if (sum >= 0 && !n.Value) { n.Value = true; return true; }
            else if (sum < 0 && n.Value) { n.Value = false; return true; }
            else return false;
        }

        public void calculateWeightMatrix()
        {
            Console.WriteLine("# of weights in model to be created: " + size * size + "\n");
            foreach (Rule r in rules)
            {
                int pCount = model.GetInstructors().Count + model.GetStudents().Count;
                int tCount = model.GetTimeSlots().Count;
                int rCount = model.GetRooms().Count;
                int[,,,,,] ruleWM = r.getWeightMatrix(neuronModel);
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
                                        weightMatrix[p1, t1, r1, p2, t2, r2] += ruleWM[p1, t1, r1, p2, t2, r2];
                                    }
                                }
                            }
                        }
                    }
                }
            }
            Console.WriteLine("Weightmatrix calculated.\n");
        }

        public void AddRule(Rule r) { rules.Add(r); }
    }
}
