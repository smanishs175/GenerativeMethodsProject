using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clouds : MonoBehaviour
{
    public GameObject Cloud1;
    public GameObject Cloud2;
    public GameObject Cloud3;
    private Vector3 scaleChange;
    private GameObject a;
    public int TerrainLength = 241;
    public int TerrainWidth = 241;

    // Start is called before the first frame update
    void Start()
    {
        // spawnTree();
        spawnClouds();
    }


    void spawnClouds(){

        int c  =0;
         for(int x=0;x< TerrainLength;x=x+50)
        {
            for(int y=0;y<TerrainWidth;y+=50)
            {
                if(Random.Range(0f, 1f)>.9f){
                    if (c%3==0){
                        a = Instantiate(Cloud1) as GameObject;
                    }
                    else if (c%3==1){
                        a = Instantiate(Cloud2) as GameObject;
                    }
                    else{
                        a = Instantiate(Cloud3) as GameObject;
                    }
                    float b = Random.Range(2f, 10f);
                    scaleChange = new Vector3(100f*b,100f*b,100f*b);
                    a.transform.position = new Vector3(x,Random.Range(50f, 65f),y);
                    a.transform.localScale = scaleChange;
                    c+=1;
                }
            }
            }

        

    }

 
}
