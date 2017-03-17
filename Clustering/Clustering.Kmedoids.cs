using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clustering
{
    partial class Clustering
    {

        public static List<DataPoint> KMedoidsAlgorithm(List<DataPoint> points, int k, int maxSteps)
        {
            if (k > points.Count() || k < 1)
            {
                throw new Exception("K must be between 0 and set size");
            }


            Console.Write("Initial set:");
            foreach (DataPoint p in points)
                Console.Write(" " + p);
            Console.WriteLine();


            DataPoint[] medoids = new DataPoint[k];
            string[] resultClusterPoints = new string[k];
            DataPoint[] resultPoints = new DataPoint[points.Count];
            DataPoint[] stepmedoids = new DataPoint[k];
            Random random = new Random();
            List<int> medoidsIndexes = new List<int>();
            int randIndex = 0;

            //initilizing random different medoids
            for (int i = 0; i < k; i++)
            {
                randIndex = random.Next(0, points.Count());
                if (!medoidsIndexes.Contains(randIndex))
                {
                    stepmedoids[i] = points[randIndex];
                    medoidsIndexes.Add(randIndex);
                }
                else
                {
                    i--;
                }
            }

            int step = 0;
            double resultSumFunc = double.MaxValue;
            double stepSumFunc = 0;

            while (step < maxSteps)
            {
                Console.Write("Medoids");
                for (int i = 0; i < k; i++)
                {
                    Console.Write(" " + stepmedoids[i]);
                }
                Console.WriteLine();

                //initial clustering to medoids
                double[] clusterSumFunc = new double[k];
                string[] clusterPoints = new string[k];

                double dist = 0;
                for (int i = 0; i < points.Count(); i++)
                {
                    double minDist = double.MaxValue;
                    dist = 0;
                    for (int c = 0; c < k; c++)
                    {
                        dist = Math.Sqrt(Math.Pow(points[i].X - stepmedoids[c].X, 2) + Math.Pow(points[i].Y - stepmedoids[c].Y, 2));
                        if (dist < minDist)
                        {
                            points[i].Cluster = c;
                            minDist = dist;
                        }
                    }
                    //getting sumFunc result for all clusters
                    clusterSumFunc[points[i].Cluster] += minDist;
                    stepSumFunc += minDist;
                    if (clusterPoints[points[i].Cluster] == null)
                        clusterPoints[points[i].Cluster] = "";
                    clusterPoints[points[i].Cluster] += " " + points[i].ToString();
                }

                //printing clusters
                for (int i = 0; i < k; i++)
                {
                    Console.WriteLine("Cluster {0} Weight {1:F3}:{2}", i, clusterSumFunc[i], clusterPoints[i]);
                }

                //Console.WriteLine("Result Weight {0:F3}", stepSumFunc);

                //if result of sumFinc is better than previous, save the configuration
                if (stepSumFunc < resultSumFunc)
                {
                    Console.Write("CONFIG CHANGED TO");
                    resultSumFunc = stepSumFunc;
                    for (int i = 0; i < k; i++)
                    {
                        medoids[i] = stepmedoids[i];
                        resultClusterPoints[i] = clusterPoints[i];
                        Console.Write(" " + medoids[i]);
                    }
                    points.CopyTo(resultPoints);
                    Console.WriteLine();
                }
                stepSumFunc = 0;
                step++;

                //random swapping medoids with nonmedoids
                int[] clusterSwapRandomCost = new int[k];
                int[] indexOfSwapCandidate = new int[k];

                int randomValue = 0;

                for (int i = 0; i < points.Count(); i++)
                {
                    randomValue = random.Next();
                    if (clusterSwapRandomCost[points[i].Cluster] < randomValue && stepmedoids[points[i].Cluster] != points[i])
                    {
                        indexOfSwapCandidate[points[i].Cluster] = i;
                        clusterSwapRandomCost[points[i].Cluster] = randomValue;
                    }
                }

                for (int i = 0; i < k; i++)
                {
                    stepmedoids[i] = points[indexOfSwapCandidate[i]];
                }
            }

            Console.WriteLine("RESULT:");
            Console.Write("Medoids");
            for (int i = 0; i < k; i++)
            {
                Console.Write(" " + medoids[i]);
            }
            Console.WriteLine();

            string[] resultClusters = new string[k];

            for (int i = 0; i < k; i++)
                Console.WriteLine("Cluster {0}:{1}", i, resultClusterPoints[i]);

            return resultPoints.ToList();
        }
    }
}
