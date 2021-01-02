using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    //PlayerPref String Keys----->

    //Gameplay Keys-->
    const string ABILITY_POWER_KEY = "ability power"; // set on new game


    // Ability Power Counter Set-Get
    public static void SetAbilityPower(int abilityPower)
    {
        PlayerPrefs.SetInt(ABILITY_POWER_KEY, abilityPower);
    }
    public static int GetAbilitiyPower()
    {
        return PlayerPrefs.GetInt(ABILITY_POWER_KEY);
    }



}
