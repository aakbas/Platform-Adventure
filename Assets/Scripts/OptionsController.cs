using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsController : MonoBehaviour
{
    [SerializeField] Slider volumeSlider; 
    [SerializeField] float defaultVolume = 0.8f;
    LanguageOptions myLanguage;

    private void Start()
    {
        myLanguage = FindObjectOfType<LanguageOptions>();
        volumeSlider.value = GameData.GetMusicVolume();  
    }

    private void Update()
    {                    
         var musicPlayer = FindObjectOfType<MusicPlayer>();
        if (musicPlayer)
        {
            musicPlayer.SetVolume(volumeSlider.value);
        }          
    }


    public void SaveAndQuit()
    {
        GameData.SetMusicVolume(volumeSlider.value);
        FindObjectOfType<LevelLoader>().LoadStartMenu();
    }

    public void Default()
    {
        volumeSlider.value = defaultVolume;
       
    }

    public void ChangeLanguageFirst()
    {
        GameData.SetLanguageKey(0);
        myLanguage.AssetChanges();

    }

    public void ChangeLanguageSecond()
    {
        GameData.SetLanguageKey(1);
        myLanguage.AssetChanges();

    }




}

