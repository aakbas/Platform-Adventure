using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    //PlayerPref String Keys----->

    //Gameplay Keys-->
    const string ABILITY_POWER_KEY = "ability power"; // set on new game
    const string PROGRESS_COUNTER_KEY="progress counter";//set on new game

    //Audio Keys-->
    const string MUSIC_VOLUME_KEY = "music volume";
    const float MIN_MUSIC_VOLUME = 0f;
    const float MAX_MUSIC_VOLUME = 1f;

    //GamedataKeys--->
    const string LEVEL_PROGRESS_KEY = "level progress";// set on new game


    // Ability Power Counter Set-Get
    public static void SetAbilityPower(int abilityPower)
    {
        PlayerPrefs.SetInt(ABILITY_POWER_KEY, abilityPower);
    }
    public static int GetAbilitiyPower()
    {
        return PlayerPrefs.GetInt(ABILITY_POWER_KEY);
    }

    //Music Volume SET-GET
    public static void SetMusicVolume(float volume)
    {
        if (volume >= MIN_MUSIC_VOLUME && volume <= MAX_MUSIC_VOLUME)
        {
            PlayerPrefs.SetFloat(MUSIC_VOLUME_KEY, volume);
        }
        else
        {
            Debug.LogError("Master Volume is out of range");
        }
    }
    public static float GetMusicVolume()
    {
        return PlayerPrefs.GetFloat(MUSIC_VOLUME_KEY);
    }

    //Progress Counter SET-Get
    public static void SetProgressCounter(int progressCounter)
    {
        PlayerPrefs.SetInt(PROGRESS_COUNTER_KEY, progressCounter);
    }
    public static int GetProgressCounter()
    {
        return PlayerPrefs.GetInt(PROGRESS_COUNTER_KEY);
    }

    //Level Progress SET-GET
    public static void SetLevelProgress(int levelProgress)
    {
        PlayerPrefs.SetInt(LEVEL_PROGRESS_KEY, levelProgress);
    }
    public static int GetLevelProgress()
    {
        return PlayerPrefs.GetInt(LEVEL_PROGRESS_KEY);
    }

    //Set New Game 

    public static void SetNewGameData()
    {
        SetAbilityPower(3);
        SetProgressCounter(5);
        SetLevelProgress(4);
    }


}
