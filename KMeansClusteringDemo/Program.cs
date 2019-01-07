using System;
using static KMeansClusteringDemo.KMeansAlgorithm;
using static KMeansClusteringDemo.Helper;

namespace KMeansClusteringDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("\nBegin outlier data detection demo\n");
                Console.WriteLine("Loading all (height-weight) data into memory");

                string[] attributes = new string[] { "Height", "Weight" };
                double[][] rawData = new double[20][];
                
                rawData[0] = new double[] { 65.0, 220.0 };
                rawData[1] = new double[] { 73.0, 160.0 };
                rawData[2] = new double[] { 59.0, 110.0 };
                rawData[3] = new double[] { 61.0, 120.0 };
                rawData[4] = new double[] { 75.0, 150.0 };
                rawData[5] = new double[] { 67.0, 240.0 };
                rawData[6] = new double[] { 68.0, 230.0 };
                rawData[7] = new double[] { 70.0, 220.0 };
                rawData[8] = new double[] { 62.0, 130.0 };
                rawData[9] = new double[] { 66.0, 210.0 };
                rawData[10] = new double[] { 77.0, 190.0 };
                rawData[11] = new double[] { 75.0, 180.0 };
                rawData[12] = new double[] { 74.0, 170.0 };
                rawData[13] = new double[] { 70.0, 210.0 };
                rawData[14] = new double[] { 61.0, 110.0 };
                rawData[15] = new double[] { 58.0, 100.0 };
                rawData[16] = new double[] { 66.0, 230.0 };
                rawData[17] = new double[] { 59.0, 120.0 };
                rawData[18] = new double[] { 68.0, 210.0 };
                rawData[19] = new double[] { 61.0, 130.0 };


                while (true)
                {



                    Console.WriteLine("\nRaw data:\n");
                    ShowMatrix(rawData, rawData.Length, true);
                    int numAttributes = attributes.Length;
                    int numClusters = 4;
                    int maxCount = 9000;

                    Console.WriteLine("\nk = " + numClusters + " and maxCount = " + maxCount);

                    int[] clustering = Cluster(rawData, numClusters, numAttributes, maxCount);

                    Console.WriteLine("\nClustering complete");
                    Console.WriteLine("\nClustering in internal format: \n");
                    ShowVector(clustering);
                    Console.WriteLine("\nClustered data:");

                    ShowClustering(rawData, numClusters, clustering);

                    double[] outlier = Outlier(rawData, clustering, numClusters, 3);

                    Console.WriteLine("Outlier is:");
                    ShowVector(outlier);

                    Console.ReadLine();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
          
        }



    }
}

