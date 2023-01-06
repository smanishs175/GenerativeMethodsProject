using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(MeshFilter))]
public class MeshGenerator : MonoBehaviour
{
    Mesh mesh;
    Vector3[] vertices;
    int[] triangles;
    Color[] colors;
    //keeping mapChunkSize as a 241 as 240 it is divisble by all even numbers
    //will make it easier to reduce the resolution when mapped wrt distance when merging multiple meshes
    const int mapchunkSize = 241;
    [Range(0, 6)]
    public int levelOfDetail;
    [Header("Dimensions")]
    public int length;
    public float height;
    public int width;
    public float scale = 0.01f;
    public Vector2 offset;
    [SerializeField]
    private int octaves;
    [SerializeField]
    private float lacunarity;
    [SerializeField]
    [Range(0, 1)]
    private float persistance;
    [SerializeField]
    private int seed;
    public Gradient gradient;
    public float[,] heightMap;
    float minTerrainHeight;
    float maxTerrainHeight;
    Vector3 position;
    public AnimationCurve heightCurve;
    [SerializeField]
    public TerrainType[] regions;
    public GameObject tree;
    //private GameObject obj;
    [System.Serializable]
    public struct TerrainType
    {
        public string name;
        public float height;
        public Color color;
    }
    private void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        CreateShape();
        UpdateMesh();
        spawnTree2(vertices);
    }
    private void Update()
    {
        //CreateShape();
        UpdateMesh();
        //if(mes)
        //spawnTree();
    }


    public void spawnTree()
    {
        //float[,] heightMap = mgObj.heightMap;
        //length = mgObj.length;
        //width = mgObj.width;
        ;

        // not starting the iterations from x=0 or z=0 and ending at x=length and z=width
        // because we didnt want trees to be spawned at the edges as 
        // a part of them were going out of the mesh which was not aesthetically pleasing.
        //Al
        for (int z = 15; z < length - 30; z += 45)
        {
            for (int x = 15; x < width - 30; x += 45)
            {
                position.x = x;
                position.z = z;
                position.y = 0;
                tree.transform.position = position;
                Instantiate(tree, tree.transform.position, tree.transform.rotation);
                //print("spawned");
            }
        }
    }

    public void spawnTree2(Vector3[] vertices)
    {
        Vector3 scaleChange;
        for (int i = 0; i < vertices.Length; i += (int)UnityEngine.Random.Range(500, 800))
        {
            if(vertices[i].y==0)
            {
                tree.transform.position = vertices[i];
                scaleChange = new Vector3(0.3f, .3f, .3f);
                tree.transform.localScale = scaleChange;
                Instantiate(tree, tree.transform.position, tree.transform.rotation);
            }
            
        }
    }

    // function to get water height based on % of land under water 
    public float getWaterHeight(Vector3[] vertices, int waterPercent)
    {

        float[] totalPoints = new float[vertices.Length];

        for (int i = 0; i < vertices.Length; i++)
        {
            totalPoints[i] = vertices[i].y;
        }
        Array.Sort(totalPoints);
        int index = (int)(waterPercent * vertices.Length) / 100;
        return totalPoints[index];
    }



    void CreateShape()
    {
        vertices = new Vector3[(width + 1) * (length + 1)];
        //will decide the length of the triangles that will be plotted
        //which in turn will decide how detailed the mesh will be.
        int meshSimplificationSkip = levelOfDetail * 2;
        if (meshSimplificationSkip == 0)
            meshSimplificationSkip = 1;
        //if width=9(0 indexed) then noOfvertices=8/msv(say a skip of 2)=4+1
        int numberOfVerticesPerLine = width - 1 / meshSimplificationSkip + 1;
        if(gameObject.tag=="PerlinOctaves")
            heightMap = NoiseMapFinal.CreatePerlinOctaves(width + 1, length + 1, scale, seed, octaves, persistance, lacunarity, offset);
        if (gameObject.tag == "Perlin")
            heightMap = NoiseMapFinal.CreatePerlin(width + 1, length + 1, scale, seed, octaves, persistance, lacunarity, offset);
        if (gameObject.tag == "OpenSimplex")
            heightMap = NoiseMapFinal.CreateOpenSimplex(width + 1, length + 1, scale, seed, octaves, persistance, lacunarity, offset);
        if (gameObject.tag == "RestNoise")
            heightMap = NoiseMapFinal.restNoise1(width + 1, length + 1);
        if (gameObject.tag == "RestNoise2")
            heightMap = NoiseMapFinal.restNoise2(width + 1, length + 1);

        for (int i = 0, z = 0; z <= length; z++)
        {
            for (int x = 0; x <= width; x++)
            {
                //float y = Mathf.PerlinNoise(x * .2f, z * .2f) * 2f;
                float y = heightMap[x, z];
                vertices[i] = new Vector3(x, heightCurve.Evaluate(y) * height, z);
                if (y > maxTerrainHeight)
                    y = maxTerrainHeight;
                if (y < minTerrainHeight)
                    y = minTerrainHeight;
                i++;
            }
        }
        triangles = new int[width * length * 6];
        int vert = 0;
        int tris = 0;

        for (int z = 0; z < width; z += meshSimplificationSkip)
        {
            for (int i = 0; i < length; i += meshSimplificationSkip)
            {
                triangles[tris + 0] = vert;
                triangles[tris + 1] = vert + numberOfVerticesPerLine + 1;
                triangles[tris + 2] = vert + 1;
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + numberOfVerticesPerLine + 1;
                triangles[tris + 5] = vert + numberOfVerticesPerLine + 2;
                vert++;
                tris += 6;
            }
            vert++;
        }
        colors = new Color[vertices.Length];
        for (int i = 0, z = 0; z <= length; z += meshSimplificationSkip)
        {
            for (int x = 0; x <= width; x += meshSimplificationSkip)
            {
                //since gradient values range between 0 and 1
                //we need to normalize y values between 0 and 1 and then map it to the gradient
                //inverseLerp is the normalization function
                float height = Mathf.InverseLerp(minTerrainHeight, maxTerrainHeight, vertices[i].y);
                colors[i] = gradient.Evaluate(heightMap[x, z]);
                i++;

            }
        }
            
    }

    void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.colors = colors;
        MeshCollider meshCollider = gameObject.GetComponent<MeshCollider>();
        mesh.RecalculateBounds();
        meshCollider.sharedMesh = mesh;
        mesh.RecalculateNormals();

    }
}
