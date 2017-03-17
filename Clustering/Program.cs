using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Clustering
{

    

    class Program
    {
        static void Main(string[] args)
        {

            List<DataPoint> points = new List<DataPoint>() { new DataPoint(3, 10), new DataPoint(4, 6), new DataPoint(5, 4), new DataPoint(5, 8),
                                                                new DataPoint(5, 12), new DataPoint(5, 15), new DataPoint(6, 6), new DataPoint(7, 9),
                                                                new DataPoint(7, 15), new DataPoint(8, 7), new DataPoint(8, 12), new DataPoint(9, 17),
                                                                new DataPoint(10, 12), new DataPoint(11, 14), new DataPoint(12, 6), new DataPoint(12, 9),
                                                                new DataPoint(14, 11), new DataPoint(14, 16), new DataPoint(15, 4), new DataPoint(16, 8)};

            //Clustering.KMeansAlgorithm(points, 3);

            Clustering.KMedoidsAlgorithm(points, 3, 5);


            //points.Clear();

            //Random random = new Random();
            //int size = 1024;
            //int k = 64;

            //for(int i = 0; i < size; i++)
            //{
            //    int x = random.Next(-1000, 1000);
            //    int y = random.Next(-1000, 1000);
            //    points.Add(new DataPoint(x, y));
            //}

            //Stopwatch sw = new Stopwatch();
            //sw.Start();
            //Clustering.KMeansAlgorithm(points, k);
            //sw.Stop();
            //Console.WriteLine("TIME");
            //Console.WriteLine((sw.ElapsedMilliseconds / 1000.0).ToString());

            //sw.Reset();
            //sw.Start();
            //Clustering.KMedoidsAlgorithm(points, k, 20);
            //sw.Stop();
            //Console.WriteLine("TIME");
            //Console.WriteLine((sw.ElapsedMilliseconds / 1000.0).ToString());

        }
    }
}
