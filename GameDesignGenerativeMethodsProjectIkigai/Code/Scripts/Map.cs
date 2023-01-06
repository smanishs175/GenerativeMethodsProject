//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;


//[ExecuteInEditMode]
//public class Map : MonoBehaviour
//{
//    public RawImage dImage;
    

//    [Header("Dimensions")]
//    public int width;
//    public int height;
//    public float scale;
//    public Vector2 offset;

//    [Header("Height Map")]
//    public float[,] heightMap;
    
//    float lastGenerateTime;
//    // Start is called before the first frame update
//    void Start()
//    {

//        CreateMap();
//    }

//    void Update(){
//        if(Time.time - lastGenerateTime > .1f){
//            lastGenerateTime = Time.time;
//            CreateMap();
//        }
//    }

//    public float[,] CreateMap(){
//        heightMap = NoiseMap.Create(width,height,scale,offset,true);


        
//        Color[] pixels = new Color[width*height];
//        int k =0;

//        for(int i =0;i<width;i++){
//            for(int j=0;j<height;j++){
//                pixels[k] = Color.Lerp(Color.blue,Color.green,heightMap[i,j]);
//                k++;
//            }
//        }

//        // Debug.Log("Done populating pixels");
//        // Debug.Log(pixels);

//        Texture2D tex = new Texture2D(width, height);
//        tex.SetPixels(pixels);
//        tex.filterMode = FilterMode.Point;
//        tex.Apply();

//        dImage.texture = tex;

//        // TerrainGenerator.GenerateTerrain(heightMap);

        
//        return heightMap;
//    }

//}
