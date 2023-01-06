using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraPan : MonoBehaviour
{
    public float desiredHeight = 10f;
 
    private void Update()
    {
        Vector3 position = transform.position;
        position.y = Terrain.activeTerrain.SampleHeight(position);
        position. y += desiredHeight ;
   
        transform.position = position;
    }
}
