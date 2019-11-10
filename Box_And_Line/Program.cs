using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Box_And_Line
{
    class Program
    {
        static void Main(string[] args)
        {

           

            double alfa = 0;
            double input = 0;

            do
            {
                do
                {
                    Console.WriteLine("Please select Alfa number between 0, 0.1 and 0.25");
                    Console.WriteLine("Press -1 if you want to exit.");

                    input = double.Parse(Console.ReadLine());



                    alfa = input;
                } while (input != -1 && alfa != 0.0 && alfa != 0.1 && alfa != 0.25);

                if (input == -1)
                    break;



                // Worker class which will do the algorithm
                Worker worker = new Worker(alfa);


                List<Result> finalResult = worker.GetSolutions();

                worker.DisplaySolutions(finalResult);


            } while (true);
        }

    }
}
