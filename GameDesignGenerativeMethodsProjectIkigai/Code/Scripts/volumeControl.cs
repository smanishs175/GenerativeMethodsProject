using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class volumeControl : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;

    void Start(){
        if(!PlayerPrefs.HasKey("GameVolume")){
            PlayerPrefs.SetFloat("GameVolume", 1);
            Load();
        }

        else{
            Load();
        }
    }
    
    public void ChangeVolume(){
        AudioListener.volume = volumeSlider.value;
        Save();
    }

    public void Load(){
        volumeSlider.value = PlayerPrefs.GetFloat("GameVolume");
    }

    public void Save(){
        PlayerPrefs.SetFloat("GameVolume", volumeSlider.value);
    }
}
