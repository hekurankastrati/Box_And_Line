using System;
using System.Collections.Generic;
using System.Linq;

namespace Box_And_Line
{
    public class Worker
    {

        const int MAX_PARENTS = 7;
        const int MAX_ITERATIONS = 70;
        const int NO_CITIES = 15;

        // Model Parent
        int[] Model = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14 };

        // Neighborhood matrix
        private string[][] neighborhood;

        double alfa = 0.25;

        //Random object to generate random numbers
        public Random random;

        public Worker()
        {
            this.random = new Random();
        }



        //Method that instantiates neighborhood with correspodent distances
        public void setNeighborhood(string[][] _neighborhood)
        {
            this.neighborhood = _neighborhood;
        }


        // Main function that makes the job done
        public Result GetSolution()
        {

            int current = 1;

            Result bestSolution = new Result();

            List<Result> solutions = GenerateParents();

            bestSolution.distance = GetLowestDistance(solutions);
            bestSolution.cities = solutions[0].cities;


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

                    if (child.distance < bestSolution.distance)
                    { 
                        bestSolution.cities = child.cities;
                        bestSolution.distance = child.distance; 
                    }

                }

                solutions.OrderBy(x => random.Next()).ToList();
                current++;
            }


            return bestSolution;
        }

        private Result CrossOver(Result parent1, Result parent2)
        {
            int[] cities;


            do
            {
                cities = new int[NO_CITIES];

                double u = DoubleNumberBetween(0, 1 + 2 * alfa);

                for (int i = 0; i < NO_CITIES; i++)
                {
                    // Calculate value using formula
                    double currentCity = (parent1.cities[i] - alfa) + u * (parent2.cities[i] - parent1.cities[i]);

                    int currentCityInt = (int)currentCity;
                    // Set bottom limit to 0
                    // And upper limit to 14 
                    if (currentCityInt < 0)
                        currentCityInt = 0;
                    else if (currentCityInt > 14)
                        currentCityInt = 14;

                    cities[i] = (int)currentCityInt;

                }

            } while (!hasUniqueValues(cities));


            Result result = new Result();

            result.cities = cities;
            result.distance = GetTotalDistance(cities);

            return result;
        }

        public List<Result> GenerateParents()
        {

            List<Result> response = new List<Result>();


            for (int i = 0; i < MAX_PARENTS; i++)
            {
                Result parent = new Result();

                var cities = Enumerable.Range(0, 15).ToArray();

                cities =  cities.Shuffle(random).ToArray<int>();

                parent.cities = cities;
                parent.distance = GetTotalDistance(cities);

                response.Add(parent);
            }

            return response;
        }

        private int GetTotalDistance(int[] solution)
        {
            var sum = 0;

            //Sum distances between two adjacent cities form 0 to n-1
            for (int i = 0; i < solution.Length - 1; i++)
            {
                sum += GetDistance(solution[i], solution[i + 1]);
            }

            // Add to that sum distance of last item and the  first one
            sum += GetDistance(solution[solution.Length - 1], solution[0]);

            return sum;
        }

        private int GetDistance(int origin, int destination)
        {
            int distance = int.Parse(neighborhood[origin][destination]);
            return distance;
        }

        private int GetLowestDistance(List<Result> results)
        {
            int lowest = results[0].distance;
            int size = results.Count;

            for(int i=1; i<size; i++)
            {
                if (results[i].distance < lowest)
                    lowest = results[i].distance;
            }

            return lowest;
        }

        private bool hasUniqueValues(int[] cities)
        {

            for(int i=0; i< NO_CITIES; i++)
            {
                for (int j = 0; j < NO_CITIES; j++)
                {
                    if (i != j && cities[i] == cities[j])
                        return false;
                }
            }

            return true;
        }

        private double DoubleNumberBetween(double minValue, double maxValue)
        {
            Random random = new Random();

            return minValue + (random.NextDouble() * (maxValue - minValue));
        }
    }

}
