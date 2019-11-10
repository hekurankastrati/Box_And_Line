using System;
using System.Collections.Generic;
using System.Linq;

namespace Box_And_Line
{
    public class Worker
    {

        const int MAX_PARENTS = 7;
        const int MAX_ITERATIONS = 70;
        const int NO_NUMBERS = 15;

        // Model Parent
        int[] Model = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14 };

      
        double alfa = 0.25;

        //Random object to generate random numbers
        public Random random;

        public Worker()
        {
            this.random = new Random();
        }

        // Main function that makes the job done
        public List<Result> GetSolutions()
        {

            int current = 1;
                   

            List<Result> finalSolutions = GenerateParents();

            List<Result> solutions = GenerateParents();

            finalSolutions.AddRange(solutions);


            while(current <= MAX_ITERATIONS)
            {
                random = new Random();

                for (int i = 0; i < MAX_PARENTS; i++)
                {

                    int position1, position2;

                    position1 = random.Next(0, solutions.Count);

                    do
                    {
                        position2 = random.Next(0, solutions.Count);

                    } while (position2 == position1);


                    Result child = CrossOver(solutions[position1], solutions[position2]);

                    solutions.Add(child);
                    finalSolutions.Add(child);

                }

                solutions.OrderBy(x => random.Next()).ToList();
                current++;
            }


            return finalSolutions;
        }

        // Method which does tha main job of algorithm
        private Result CrossOver(Result parent1, Result parent2)
        {
            int[] numbers;


            // Try combinations
            // Until the array has unique values for cities
            do
            {
                numbers = new int[NO_NUMBERS];

                //Generate a random number
                double u = DoubleNumberBetween(0, 1 + 2 * alfa);

                for (int i = 0; i < NO_NUMBERS; i++)
                {
                    // Calculate value using formula
                    double currentNumber = (parent1.numbers[i] - alfa) + u * (parent2.numbers[i] - parent1.numbers[i]);

                    int currentNumberInt = (int)currentNumber;

                    // Set bottom limit to 0
                    // And upper limit to 14 
                    if (currentNumberInt < 0)
                        currentNumberInt = 0;
                    else if (currentNumberInt > 14)
                        currentNumberInt = 14;

                    numbers[i] = (int)currentNumberInt;

                }

            } while (!hasUniqueValues(numbers));


            // Prepare result to return
            Result result = new Result();

            result.numbers = numbers;

            return result;
        }

        // Function to generate parents
        public List<Result> GenerateParents()
        {

            List<Result> response = new List<Result>();


            for (int i = 0; i < MAX_PARENTS; i++)
            {
                Result parent = new Result();

                var numbers = Enumerable.Range(0, 15).ToArray();

                numbers =  numbers.Shuffle(random).ToArray<int>();

                parent.numbers = numbers;

                response.Add(parent);
            }

            return response;
        }

       

        // Function that checks if an array has unique values
        private bool hasUniqueValues(int[] numbers)
        {

            for(int i=0; i< NO_NUMBERS; i++)
            {
                for (int j = 0; j < NO_NUMBERS; j++)
                {
                    if (i != j && numbers[i] == numbers[j])
                        return false;
                }
            }

            return true;
        }

        // Function that generates a random 'Double' number between two values
        private double DoubleNumberBetween(double minValue, double maxValue)
        {
            Random random1 = new Random();

            return minValue + (random1.NextDouble() * (maxValue - minValue));
        }

        // Function that displays solutions
        public void DisplaySolutions(List<Result> solutions)
        {
            for (int i = 0; i < solutions.Count; i++)
            {
                Console.WriteLine($"Solution {i}:");

                foreach(var item in solutions[i].numbers)
                {
                    Console.Write($"{item},");
                }

                Console.WriteLine();    
            } 

        }
    }

}
