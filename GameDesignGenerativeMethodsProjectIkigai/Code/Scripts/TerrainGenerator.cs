//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using Random = UnityEngine.Random;




//public class TerrainGenerator : MonoBehaviour
//{   
//    [Header("Dimensions")]
//    public int height = 1000;
//    public int depth = 200;
//    public int width = 1000;
//    public float scale = 12f;
//    public float offsetX = 100f;
//    public float offsetY = 100f;
//    public float[,] offset;
//    public float time=0.0f;

//    // Start is called before the first frame update
//    void Start()
//    {
//        offsetX = Random.Range(0f, 9999f);
//        offsetY = Random.Range(0f, 9999f);
//    }
//    void OnUpdate()
//    {
//        Terrain terrain = GetComponent<Terrain>();
//        terrain.terrainData = GenerateTerrain(terrain.terrainData);
//        time+=0.1f;
//        //  Instantiate(palmTree, new Vector3( 2.0F, 0, 0), Quaternion.identity);
//    }

//    TerrainData GenerateTerrain(TerrainData terrainData)
//    {
//        terrainData.heightmapResolution = width + 1;
//        terrainData.size = new Vector3(width, depth, height);
//        float[,] heights = GenerateHeights();
//        // Debug.Log(heights[1,1]);
//        terrainData.SetHeights(0,0,heights);
//        return terrainData; 
//    }

//    private float[,] GenerateHeights()
//    {
//        // Map map = new Map();
//        float[,] heightMap = NoiseMapFinal.CreatePerlinOctaves(width,height,scale,new Vector2(0,0),false,time);

//        float[,] heights = new float[width, height];
//        for(int x=0;x<width;x++)
//        {
//            for(int y=0;y<height;y++)
//            {
//                heights[x, y] = heightMap[x, y];
//                // heights[x,y] = CalculateHeight(x,y);
//            }
//        }
//        return heights;
//    }
//    //will generate perlin noise
//    private float CalculateHeight(int x, int y)
//    {
//        float xCoord =(float)x / width * scale + offsetX;
//        float yCoord = (float)y / height * scale + offsetY;
//        return Mathf.PerlinNoise(xCoord, yCoord);
//    }


//}

