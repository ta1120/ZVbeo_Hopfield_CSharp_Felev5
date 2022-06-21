using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace keretprogram_ZVbeo
{
    abstract class NeuronModel
    {
        protected Neuron[,,] neurons;
        public int size() { return InputModel.GetInstance().getSize(); }

        public Neuron[,,] getNeurons() { return neurons; }

        public Neuron getNeuronAt(int p, int t, int r) { return neurons[p, t, r]; }
    }
}
