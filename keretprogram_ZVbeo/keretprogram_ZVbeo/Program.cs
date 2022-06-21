using System;

namespace keretprogram_ZVbeo
{
    class Program
    {
        static void Main(string[] args)
        {
            //Source directory for input
            string dir = @"R:\BME-VIK\felev5\temalabor\resources-and-handouts\ZVbeo_bemenetek\Input_teljesZV\";

            //Filename
            string file = "input05_beosztás_output";
            //string file = "input01_beosztas_output";
            //string file = "input08_jan eleje_output";
            //string file = "input16_2018_osz_2_output";
            //string file = "input18_2019tavasz_output";

            InputModel model = InputModel.GetInstance();

            //Read input file
            if (model.SmartInputFileRead(dir + file))
            {
                Console.WriteLine("Read OK\n");

                //Manually defining 2 rooms
                model.AddRoom(new Room(0, "Terem1"));
                model.AddRoom(new Room(1, "Terem2"));

                //Creating TSH neuron model
                NeuronModel neuronModel = new NeuronModel_TSH();

                //Setting RuleSet
                RuleSet ruleSet = new SchedulePresidents(neuronModel);

                //Evaluating neural network
                ruleSet.evaluateNet();

                //Writing results to CSV file
                ruleSet.writeToCSV();
            }
            else
            {
                Console.WriteLine("Read failed\n");
            }
        }
    }
}
