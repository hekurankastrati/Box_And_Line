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
           
            // Worker class which will do the algorithm
            Worker worker = new Worker();

            List<Result> finalResult = worker.GetSolutions();

            worker.DisplaySolutions(finalResult);

            Console.ReadKey();
        }

    }
}
