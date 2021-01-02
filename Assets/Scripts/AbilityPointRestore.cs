using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityPointRestore : MonoBehaviour
{

    [SerializeField] int abilityAmount;
    int tempAbilityPower;

    public void GetAbilityAmount()
    {
        tempAbilityPower = GameData.GetAbilitiyPower();
        if (abilityAmount<tempAbilityPower)
        {
            abilityAmount = tempAbilityPower;
        }
        else
        {
            GameData.SetAbilityPower(abilityAmount);
        }
    }

    public int RestoreAbilityPower()
    {
        GetAbilityAmount();
        return abilityAmount;
    }




}
