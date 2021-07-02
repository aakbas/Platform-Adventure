using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheeseController : MonoBehaviour
{   
 
    private void Start()
    {
       

        if (GameData.GetLevelProgress() < 4)
        {
            GameData.SetLevelProgress(4);
        }

        if (GameData.GetCheeseKey()!=17)
        {
            float[] clearArray = new float[16];
            for (int i = 0; i < 16; i++)
            {
                clearArray[i] = 0f;
            }
            int[] deathArray = new int[16];
            for (int i = 0; i < 16; i++)
            {
                deathArray[i] = 0;
            }
            int[] checkpointArray = new int[16];
            for (int i = 0; i < 16; i++)
            {
                checkpointArray[i] = 0;
            }
            GameData.SetChecpointProgress(checkpointArray);
            GameData. SetTimerData(clearArray);
          GameData.  SetDeathCounter(deathArray);
            GameData.SetSFXVolume(800);
        }

    }




}
