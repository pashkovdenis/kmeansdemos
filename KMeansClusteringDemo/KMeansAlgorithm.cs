using System;
using System.Collections.Generic;
using System.Text;
using static KMeansClusteringDemo.Helper;

namespace KMeansClusteringDemo
{
    static class KMeansAlgorithm
    {
        public static int[] Cluster(in double[][] rawData,
         in int numClusters,
         in int numAttributes,
         in int maxCount)
        {
            bool changed = true;
            int ct = 0;
            int numTuples = rawData.Length;
            int[] clustering = InitClustering(numTuples, numClusters, 0);
            double[][] means = Allocate(numClusters, numAttributes);
            double[][] centroids = Allocate(numClusters, numAttributes);
            UpdateMeans(rawData, clustering, ref means);
            UpdateCentroids(rawData, clustering, means, centroids);
            while (changed == true && ct < maxCount)
            {
                ++ct;
                changed = Assign(rawData, clustering, centroids);
                UpdateMeans(rawData, clustering, ref means);
                UpdateCentroids(rawData, clustering, means, centroids);
            }
            return clustering;
        }
         
        static int[] InitClustering(in int numTuples, in int numClusters, in int randomSeed)
        {
            Random random = new Random(randomSeed);
            int[] clustering = new int[numTuples];
            for (int i = 0; i < numClusters; ++i)
                clustering[i] = i;
            for (int i = numClusters; i < clustering.Length; ++i)
                clustering[i] = random.Next(0, numClusters);
            return clustering;
        }

        static double[][] Allocate(in int numClusters, in int numAttributes)
        {
            double[][] result = new double[numClusters][];
            for (int k = 0; k < numClusters; ++k)
                result[k] = new double[numAttributes];
            return result;
        } 

        static void UpdateMeans(in double[][] rawData, in int[] clustering, ref double[][] means)
        {
            int numClusters = means.Length;
            for (int k = 0; k < means.Length; ++k)
                for (int j = 0; j < means[k].Length; ++j)
                    means[k][j] = 0.0;
            int[] clusterCounts = new int[numClusters];
            for (int i = 0; i < rawData.Length; ++i)
            {
                int cluster = clustering[i];
                ++clusterCounts[cluster];
                for (int j = 0; j < rawData[i].Length; ++j)
                    means[cluster][j] += rawData[i][j];
            }
            for (int k = 0; k < means.Length; ++k)
                for (int j = 0; j < means[k].Length; ++j)
                    means[k][j] /= clusterCounts[k];
        }

        static void UpdateCentroids(in double[][] rawData, int[] clustering, double[][] means, double[][] centroids)
        {
            for (int k = 0; k < centroids.Length; ++k)
            {
                double[] centroid = ComputeCentroid(rawData, clustering, k, means);
                centroids[k] = centroid;
            }
        }

        static double[] ComputeCentroid(double[][] rawData, in int[] clustering, int cluster, double[][] means)
        {
            int numAttributes = means[0].Length;
            double[] centroid = new double[numAttributes];
            double minDist = double.MaxValue;
            for (int i = 0; i < rawData.Length; ++i)
            {
                int c = clustering[i];
                if (c != cluster) continue;
                double currDist = Distance(rawData[i], means[cluster]);
                if (currDist < minDist)
                {
                    minDist = currDist;
                    for (int j = 0; j < centroid.Length; ++j)
                        centroid[j] = rawData[i][j];
                }
            }
            return centroid;
        }
         
        static bool Assign(in double[][] rawData, int[] clustering, in double[][] centroids)
        {
            int numClusters = centroids.Length;
            bool changed = false;
            double[] distances = new double[numClusters];
            for (int i = 0; i < rawData.Length; ++i)
            {
                for (int k = 0; k < numClusters; ++k)
                    distances[k] = Distance(rawData[i], centroids[k]);
                int newCluster = MinIndex(distances);
                if (newCluster != clustering[i])
                {
                    changed = true;
                    clustering[i] = newCluster;
                }
            }
            return changed;
        }

        public static double[] Outlier(in double[][] rawData, in int[] clustering, in int numClusters, in int cluster)
        {
            int numAttributes = rawData[0].Length;
            double[] outlier = new double[numAttributes];
            double maxDist = 0.0;
            double[][] means = Allocate(numClusters, numAttributes);
            double[][] centroids = Allocate(numClusters, numAttributes);

            UpdateMeans(rawData, clustering, ref means);
            UpdateCentroids(rawData, clustering, means, centroids);

            for (int i = 0; i < rawData.Length; ++i)
            {
                int c = clustering[i];
                if (c != cluster) continue;
                double dist = Distance(rawData[i], centroids[cluster]);
                if (dist > maxDist)
                {
                    maxDist = dist;
                    Array.Copy(rawData[i], outlier, rawData[i].Length);
                }
            }
            return outlier;
        }

  

     


    }

}
