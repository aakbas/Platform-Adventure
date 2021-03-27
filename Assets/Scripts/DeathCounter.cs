using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DeathCounter : MonoBehaviour
{

    [SerializeField] Text deathCounter;
    [SerializeField] Text totalDeathCounter;
    [SerializeField] int death;
    [SerializeField] int totalDeath;
   [SerializeField] int[] tempDeathArray;
    LevelTeleporter myLevel;
    int levelIndex;

    private void Start()
    {
        myLevel = FindObjectOfType<LevelTeleporter>();
        levelIndex = myLevel.GetCurrentLevel();
        tempDeathArray= GameData.GetDeathCounter();
        death = tempDeathArray[levelIndex];
     
    }



    private void Update()
    {
        tempDeathArray= GameData.GetDeathCounter();
        ShowDeath();
    }




    public void HandleDeathCounter()
    {
         death++;
        tempDeathArray[levelIndex] = death;
        totalDeath = TotalDeaths();
        GameData.SetDeathCounter(tempDeathArray); 
        
    }

    private void ShowDeath()
    {
        totalDeathCounter.text ="TOTAL RESET :  "+ totalDeath.ToString();
        deathCounter.text ="RESET : "+ tempDeathArray[levelIndex].ToString();
    }

    private int TotalDeaths()
    {
        totalDeath = 0;
        tempDeathArray=GameData.GetDeathCounter();
        for (int i = 0; i < tempDeathArray.Length; i++)
        {
          
            totalDeath += tempDeathArray[i];
        }

        return totalDeath;
    }








}
