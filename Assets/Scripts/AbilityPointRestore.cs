using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityPointRestore : MonoBehaviour
{
    //Config Parameters
    [SerializeField] int abilityAmount;
    int tempAbilityPower;

    //Read Ability amount from files and adjust
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

    //Restore players ability amount
    public int RestoreAbilityPower()
    {
        GetAbilityAmount();
        return abilityAmount;
    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Destroy coin on touch
        Destroy(gameObject);
    }


}
