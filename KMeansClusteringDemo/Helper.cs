using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KMeansClusteringDemo
{
     static class Helper
    {
        public  static int MinIndex(in double[] distances)
        {
            int indexOfMin = 0;
            double smallDist = distances[0];
            for (int k = 0; k < distances.Length; ++k)
            {
                if (distances[k] < smallDist)
                {
                    smallDist = distances[k]; indexOfMin = k;
                }
            }
            return indexOfMin;
        }

        public  static void ShowMatrix(in double[][] matrix, int numRows, bool ome)
        {
            for (int i = 0; i < numRows; ++i)
            {
                Console.Write("[" + i.ToString().PadLeft(2) + "]  ");
                for (int j = 0; j < matrix[i].Length; ++j)
                    Console.Write(matrix[i][j].ToString("F1") + "  ");
                Console.WriteLine("");
            }
        }

        public static void ShowVector(in double[] vector)
        {
            for (var i = 0; i < vector.Length; ++i)
                Console.Write(vector[i] + " ");
            Console.WriteLine("");

        }
        public static void ShowVector(in int[] vector) => ShowVector( vector.Select(x=>(double)x).ToArray()); 


        public static void ShowClustering(in double[][] rawData,  int numClusters, int[] clustering)
        {
            for (int k = 0; k < numClusters; ++k) // Each cluster
            {
                for (int i = 0; i < rawData.Length; ++i) // Each tuple
                    if (clustering[i] == k)
                    {
                        for (int j = 0; j < rawData[i].Length; ++j)
                            Console.Write(rawData[i][j].ToString("F1") + " ");
                        Console.WriteLine("");
                    }
                Console.WriteLine("");
            }
        }

       public static double Distance(in double[] tuple, in double[] vector)
        {
            double sumSquaredDiffs = 0.0;
            for (int j = 0; j < tuple.Length; ++j)
                sumSquaredDiffs += Math.Pow((tuple[j] - vector[j]), 2);
            return Math.Sqrt(sumSquaredDiffs);
        }



    }
}
