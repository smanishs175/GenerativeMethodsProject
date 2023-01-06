using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseMapFinal : MonoBehaviour
{

    public static float[,] restNoise1(int width, int height)
    {
        float maxh = -3.0f;
        float minh = 3.0f;
        float[,] noiseMap = new float[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                noiseMap[x, y] = .5f;
            }
        }

        // now move row by row throgh the map making each point restricted arround the previous point by .05f 
        float z = 0.0f;
        for (int x = 1; x < width; x++)
        {
            for (int y = 1; y < height; y++)
            {
                z = (float)(noiseMap[x - 1, y] + noiseMap[x, y - 1]) / 2;
                z += Random.Range(-.05f, 0.05f);
                minh = Mathf.Min(minh, z);
                maxh = Mathf.Max(maxh, z);
                noiseMap[x, y] = z;
            }
        }
        Debug.Log("MAX height :" + maxh);
        Debug.Log("MIN height :" + minh);
        return noiseMap;
    }


    // restricted noise version 2 - we need more slopes!!!
    public static float[,] restNoise2(int width, int height)
    {
        float maxh = -3.0f;
        float minh = 3.0f;
        float[,] noiseMap = new float[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                noiseMap[x, y] = .5f;
            }
        }

        // now move row by row throgh the map making each point restricted arround the previous point by .05f 
        float z = 0.0f;
        // if positive is choosen, +ve prob goes 3 times, if -ve then negetives goes three times

        //currect choise
        string choice = "upSlope";
        // random choice helper
        float t = 0.0f;

        for (int x = 1; x < width; x++)
        {
            for (int y = 1; y < height; y++)
            {
                z = (float)(noiseMap[x - 1, y] + noiseMap[x, y - 1]) / 2;
                if (choice == "upSlope")
                {
                    t = Random.Range(0, 6);
                    // 0-1 -> downslope ; 1-4 -> upSlope
                    if (t > 1)
                    {
                        choice = "upSlope";
                    }
                    else
                    {
                        choice = "downSlope";
                    }
                }
                else
                {
                    t = Random.Range(0, 6);
                    // 0-1 -> downslope ; 1-4 -> upSlope
                    if (t > 1)
                    {
                        choice = "downSlope";
                    }
                    else
                    {
                        choice = "upSlope";
                    }
                }
                if (choice == "upSlope")
                {
                    z += Random.Range(0.0f, 0.05f);
                }
                else
                {
                    z -= Random.Range(0.0f, 0.05f);
                }
                minh = Mathf.Min(minh, z);
                maxh = Mathf.Max(maxh, z);
                noiseMap[x, y] = z;
            }
        }
        Debug.Log("MAX height :" + maxh);
        Debug.Log("MIN height :" + minh);
        return noiseMap;
    }


    public static float getWateHeight(float[,] noiseMap, int width, int height, int waterPercent)
    {
        int totalPointsNum = height * width;
        float[] totalPoints = new float[totalPointsNum];
        int c = 0;
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                totalPoints[c] = noiseMap[x, y];
                c++;
            }
        }
       // Array.Sort(totalPoints);
        int index = (int)(waterPercent * c) / 2;
        return totalPoints[index];
    }

    public static float[,] CreatePerlinOctaves(int width, int height, float scale, int seed, int octaves, float persistance, float lacunarity, Vector2 offset)
    {
        System.Random prng = new System.Random(seed);
        Vector2[] octaveOffsets = new Vector2[octaves];
        for (int i = 0; i < octaves; i++)
        {
            float offsetX = prng.Next(-100000, 100000) + offset.x;
            float offsetY = prng.Next(-100000, 100000) + offset.y;
            octaveOffsets[i] = new Vector2(offsetX, offsetY);
        }
        if (scale <= 0)
        {
            scale = 0.0001f;
        }
        float[,] noiseMap = new float[width, height];
        //Debug.Log("0: "+noiseMap.GetLength(0));
        //Debug.Log("1: "+noiseMap.GetLength(1));
        float maxNoiseValue = float.MaxValue;
        float minNoiseValue = float.MinValue;
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                float amplitude = 1;
                float frequency = 1;
                float noiseHeight = 0;
                for (int i = 0; i < octaves; i++)
                {
                    float xCoord = (float)(x) / scale * frequency + octaveOffsets[i].x * 0.5f;
                    float yCoord = (float)(y) / scale * frequency + octaveOffsets[i].y * 0.5f;
                    float zCoord = Mathf.PerlinNoise(xCoord, yCoord) * 2 - 1;
                    zCoord += Mathf.PerlinNoise(xCoord * 0.2f, yCoord * 0.2f) * 1.2f;
                    zCoord -= Mathf.PerlinNoise(xCoord * 0.01f, yCoord * 0.01f) * 0.8f;
                    //zCoord-= Mathf.PerlinNoise(xCoord*0.4f, yCoord)*0.4f * 2 - 1;
                    noiseHeight += zCoord * amplitude;
                    amplitude *= persistance;
                    frequency *= lacunarity;
                }
                if (noiseHeight > maxNoiseValue)
                    noiseHeight = maxNoiseValue;
                else if (noiseHeight < minNoiseValue)
                    noiseHeight = minNoiseValue;
                noiseMap[x, y] = noiseHeight;
            }
        }
        return noiseMap;
        //Debug.Log("height map:" + noiseMap[0,0]);
    }

    public static float[,] CreatePerlin(int width, int height, float scale, int seed, int octaves, float persistance, float lacunarity, Vector2 offset, bool map = true, float t = 0.0f, string noiseType="perlin")
    {
        // init noiseMap
        float[,] noiseMap = new float[width, height];

        // iterating and building noiseMap(2D)

                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {

                        // if using offset and scaling -- 0-1 for terrain and otherwise for 2d canvas ( depricated ? )
                        // open simplex and perlin works on very small changes between inputs to give proper organic noise results

                        float xCoord, yCoord;
                        if (!map)
                        {
                            xCoord = (float)x / width * scale + offset.x;
                            yCoord = (float)y / height * scale + offset.y;
                        }
                        else
                        {
                            xCoord = (float)x * scale + offset.x;
                            yCoord = (float)y * scale + offset.y;
                        }

                        // seitching between different noise types 
                        noiseMap[x, y] = Mathf.PerlinNoise(xCoord*0.003f, yCoord*0.003f);
                    }

                }

        return noiseMap;
    }

    public static float[,] CreateOpenSimplex(int width, int height, float scale, int seed, int octaves, float persistance, float lacunarity, Vector2 offset, bool map = true, float t = 0.0f, string noiseType="perlin")
    {
        // init noiseMap
        float[,] noiseMap = new float[width, height];

        // iterating and building noiseMap(2D)

                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {

                        // if using offset and scaling -- 0-1 for terrain and otherwise for 2d canvas ( depricated ? )
                        // open simplex and perlin works on very small changes between inputs to give proper organic noise results

                        float xCoord, yCoord;
                        if (!map)
                        {
                            xCoord = (float)x / width * scale + offset.x;
                            yCoord = (float)y / height * scale + offset.y;
                        }
                        else
                        {
                            xCoord = (float)x * scale + offset.x;
                            yCoord = (float)y * scale + offset.y;
                        }

                        // switching between different noise types 
                        noiseMap[x, y] = OpenSimplex.Noise2_ImproveX(1,xCoord*0.03f,yCoord*0.03f);
                    }

                }

        return noiseMap;
    }

}
