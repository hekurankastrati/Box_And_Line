using System;
using System.IO;
using System.Linq;

namespace Box_And_Line
{
    class Program
    {
        static void Main(string[] args)
        {
            // Read neighborhood information from the file
            var input = File.ReadAllLines("./input/p01_d.txt").ToList();

            // Initialize array with size of rows
            var neighborhood = new string[input.Count][];

            // For each row initialize array with size of columns
            for (int i = 0; i < neighborhood.Length; i++)
            {
                neighborhood[i] = new string[input.Count];
            }


            // Parse columns end set values in each of them
            for (int i = 0; i < input.Count; i++)
            {
                var row = input[i];

                var columns_unformatted = row.Split(' ');

                var columns = trimColumns(columns_unformatted, input.Count);

                for (int j = 0; j < columns.Length; j++)
                {
                    neighborhood[i][j] = columns[j];
                }
            }




            // Worker class which will do the job of trial and error algorithm
            Worker worker = new Worker();

            // Set neighborhood matrix
            worker.setNeighborhood(neighborhood);

            Result result = worker.GetSolution();


            Console.WriteLine("The best road with these cities is:");

            foreach (var item in result.cities)
            {
                Console.Write($"{item},");
            }
            Console.WriteLine("");

            Console.WriteLine($"The Distance of the result is {result.distance}");

            Console.ReadKey();
        }


        static string[] trimColumns(string[] columns, int length)
        {
            string[] _columns = new string[length];

            int newIndex = 0;

            for (int i = 0; i < columns.Length; i++)
            {
                if (columns[i].Trim() != "")
                {
                    _columns[newIndex++] = columns[i];
                }

            }

            return _columns;
        }
    }
}
