﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clustering
{
    partial class Clustering
    {

        public static List<DataPoint> KMeansAlgorithm(List<DataPoint> points, int k)
        {
            if (k < 1)
            {
                throw new Exception("K must be greater than 0");
            }

            Console.Write("Initial set:");
            foreach (DataPoint p in points)
                Console.Write(" " + p);
            Console.WriteLine();

            double minX = double.MaxValue, minY = double.MaxValue;
            double maxX = double.MinValue, maxY = double.MinValue;

            //searching for boundaries of coordinates
            for (int i = 0; i < points.Count(); i++)
            {
                if (points[i].X < minX)
                {
                    minX = points[i].X;
                }
                if (points[i].X > maxX)
                {
                    maxX = points[i].X;
                }
                if (points[i].Y < minY)
                {
                    minY = points[i].Y;
                }
                if (points[i].Y > maxY)
                {
                    maxY = points[i].Y;
                }
            }
            Console.WriteLine("Borders: xє[{0},{1}] ; yє[{2},{3}]", minX, maxX, minY, maxY);

            //initializing k centroids randomly
            DataPoint[] centroids = new DataPoint[k];
            Random random = new Random();
            double centrX, centrY = 0;
            Console.Write("Centroids ");
            for (int i = 0; i < k; i++)
            {
                centrX = random.NextDouble() * (maxX - minX) + minX;
                centrY = random.NextDouble() * (maxY - minY) + minY;
                centroids[i] = new DataPoint(centrX, centrY);
                Console.Write(centroids[i] + " ");
            }
            Console.WriteLine();

            bool centroidsChanged = true;
            int counter = 0;

            //while centroids are changing their positions
            while (centroidsChanged)
            {
                //binding each point to the nearest centroid
                for (int i = 0; i < points.Count(); i++)
                {
                    double minDist = int.MaxValue;
                    double dist = 0;
                    for (int c = 0; c < k; c++)
                    {
                        dist = Math.Sqrt(Math.Pow(points[i].X - centroids[c].X, 2) + Math.Pow(points[i].Y - centroids[c].Y, 2));
                        if (dist < minDist)
                        {
                            points[i].Cluster = c;
                            minDist = dist;
                        }
                    }
                }

                int[] clusterSize = new int[k];
                double[] xSum = new double[k];
                double[] ySum = new double[k];

                string[] clusterPoints = new string[k];

                //calculating mean point for each cluster
                for (int i = 0; i < points.Count(); i++)
                {
                    int currCluster = points[i].Cluster;
                    clusterSize[currCluster]++;
                    xSum[currCluster] += points[i].X;
                    ySum[currCluster] += points[i].Y;
                    if (clusterPoints[points[i].Cluster] == null)
                        clusterPoints[points[i].Cluster] = "";
                    clusterPoints[points[i].Cluster] += " " + points[i].ToString();
                }

                //printing clusters
                for (int i = 0; i < k; i++)
                {
                    Console.WriteLine("Cluster {0}:{1}", i, clusterPoints[i]);
                }

                int centroidsChangedCounter = 0;

                //setting new mean points as centroids
                Console.Write("Centroids ");
                for (int i = 0; i < k; i++)
                {
                    DataPoint dp = new DataPoint(xSum[i] / clusterSize[i], ySum[i] / clusterSize[i]);
                    if(double.IsNaN(dp.X) || double.IsNaN(dp.Y))
                    {
                        throw new Exception("K value is too large for this set");
                    }
                    else if (dp != centroids[i])
                    {
                        centroids[i] = dp;
                        centroidsChangedCounter++;
                    }
                    Console.Write(centroids[i] + " ");
                }
                Console.WriteLine();

                //if new centroids are the same as old - stop algorithm
                if (centroidsChangedCounter == 0)
                {
                    centroidsChanged = false;
                }

                counter++;
            }

            Console.WriteLine("RESULT:");

            string[] resultClusters = new string[k];

            for (int i = 0; i < points.Count(); i++)
                resultClusters[points[i].Cluster] += " " + points[i].ToString();

            for (int i = 0; i < k; i++)
                Console.WriteLine("Cluster {0}:{1}", i, resultClusters[i]);
            
            return points;
        }
    }
}
