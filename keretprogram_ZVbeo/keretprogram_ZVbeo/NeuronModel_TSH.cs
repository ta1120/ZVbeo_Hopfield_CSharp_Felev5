using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace keretprogram_ZVbeo
{
    class Neuron_TSH : Neuron
    {
        public int PersonID { get; private set; }
        public int TimeSlotID { get; private set; }
        public int RoomID { get; private set; }

        public Neuron_TSH(int p, int t, int r) : base()
        {
            PersonID = p;
            TimeSlotID = t;
            RoomID = r;
        }

        public int[] getIndexes() 
        {
            int[] i = { PersonID, TimeSlotID, RoomID };
            return i; 
        }
    }

    class NeuronModel_TSH : NeuronModel
    {
        /*
        NeuronMegfeleltetés1: 
        "Időszeletek_óra" 
        Minden személy minden egyes timeslotja egy neuron 
        órás slotok 
        Tehát itt egy neuron értéke a következőt jelenti: egy adott személy egy adott teremben egy adott időszeletben be van-e osztva 
        */

        //Indexelés sorrendje: P T R
        //Person -> TimeSlot -> Room
        

        public NeuronModel_TSH()
        {
            Console.WriteLine("Creating neurons in [TSH] TimeSlotsHour model\n");

            InputModel model = InputModel.GetInstance();

            int pCount = model.GetInstructors().Count + model.GetStudents().Count;
            int tCount = model.GetTimeSlots().Count;
            int rCount = model.GetRooms().Count;

            neurons = new Neuron_TSH[pCount, tCount, rCount];

            for(int p = 0; p < pCount;p++)
            {
                for (int t = 0; t < tCount; t++)
                {
                    for (int r = 0; r < rCount; r++)
                    {
                        neurons[p, t, r] = new Neuron_TSH(p, t, r);
                    }
                }
            }
            Console.WriteLine(neurons.Length + " neurons created.\n");
        }

        
    }
}
