using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace keretprogram_ZVbeo
{
    class Neuron
    {
        public bool Value { get; set; }

        public Neuron() 
        {
            //Value = false; 

            Random r = new Random();
            Value = (r.Next(2) == 1);
        }
    }
}
