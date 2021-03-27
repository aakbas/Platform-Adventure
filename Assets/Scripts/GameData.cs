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

    //Gamedata Keys--->
    const string LEVEL_PROGRESS_KEY = "level progress";// set on new game

    //Language Keys-->
    const string LANGUAGE_DEFINE_KEY = "language define key";

    //Game Stat Keys-->
    const string AAKBAS_CHEESE_KEY = "aakbas";
    const string TIMER_STAT_KEY = "timer stats";
    const string DEATH_STAT_KEY = "death stats";
    


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

    //Language data GET-SET
    public static void SetLanguageKey(int language)
    {
        PlayerPrefs.SetInt(LANGUAGE_DEFINE_KEY, language);
    }
    public static int GetLanguageKey()
    {

        return PlayerPrefs.GetInt(LANGUAGE_DEFINE_KEY);
    }

    //Timer data GET-SET
    public static void SetTimerData( float[] time)
    {
        PlayerPrefsX.SetFloatArray(TIMER_STAT_KEY, time);
    }
    public static float[] GetTimerArray()
    {
        return PlayerPrefsX.GetFloatArray(TIMER_STAT_KEY);
    }

    //CHEESE KEY SET-GET
    public static void SetCheeseKey(int key)
    {
        PlayerPrefs.SetInt(AAKBAS_CHEESE_KEY,key);
    }
    public static int GetCheeseKey()
    {
        return PlayerPrefs.GetInt(AAKBAS_CHEESE_KEY);
    }
    
    //DEATH STAT KET SET-GET

    public static void SetDeathCounter(int[] death)
    {
        PlayerPrefsX.SetIntArray(DEATH_STAT_KEY, death);
    }
    public static int[] GetDeathCounter()
    {
        return PlayerPrefsX.GetIntArray(DEATH_STAT_KEY);
    }

    //Set New Game 

    public static void SetNewGameData()
    {
      float [] clearArray=new float[11] ;
        for (int i = 0; i < 11; i++)
        {
            clearArray[i] = 0f;
        }
        int[] deathArray = new int[11];
        for (int i = 0; i < 11; i++)
        {
            deathArray[i] = 0;
        }
        SetAbilityPower(3);
        SetProgressCounter(5);
        SetLevelProgress(4);
        SetTimerData(clearArray);
        SetDeathCounter(deathArray);
    }


}
