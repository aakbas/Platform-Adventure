using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsController : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;
    [SerializeField] Slider SFXSlider;
    [SerializeField] float defaultVolume = 0.8f;
    [SerializeField] float defaultSFX = 800f;
    LanguageOptions myLanguage;

    private void Start()
    {
        myLanguage = FindObjectOfType<LanguageOptions>();
        volumeSlider.value = GameData.GetMusicVolume();
        SFXSlider.value = GameData.GetSFXVolume();
    }

    private void Update()
    {                    
         var musicPlayer = FindObjectOfType<MusicPlayer>();
        if (musicPlayer)
        {
            musicPlayer.SetVolume(volumeSlider.value);
        }
        GameData.SetSFXVolume(SFXSlider.value);
    }


    public void SaveAndQuit()
    {
        GameData.SetMusicVolume(volumeSlider.value);
        GameData.SetSFXVolume(SFXSlider.value);
        FindObjectOfType<LevelLoader>().LoadStartMenu();
    }

    public void Default()
    {
        volumeSlider.value = defaultVolume;
        SFXSlider.value = defaultSFX;
       
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

