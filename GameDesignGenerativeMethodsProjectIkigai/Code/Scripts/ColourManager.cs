using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourManager : MonoBehaviour
{
   [SerializeField] 
   public TerrainType[] regions;

   [System.Serializable]
   public struct TerrainType
    {
        public string name;
        public float height;
        public Color color;
    }
}
