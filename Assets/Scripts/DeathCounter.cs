using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DeathCounter : MonoBehaviour
{

    [SerializeField] Text deathCounterText;
    [SerializeField] Text totalDeathCounterText;
    [SerializeField] int death;
    [SerializeField] int totalDeath=0;
    [SerializeField] int[] deathCounts;
    LevelTeleporter myLevelLoader;
    int tempDeath;

   [SerializeField] int levelIndex;
    private void Start()
    {
        deathCounts=GameData.GetDeathCounter();
        tempDeath = GameData.GetIntersititialCount();
        myLevelLoader = FindObjectOfType<LevelTeleporter>();
        levelIndex = myLevelLoader.GetCurrentLevel();
        death = deathCounts[levelIndex];
        CalculateDeath();
    }

    private void Update()
    {
        ShowDeath();
    }

    private void ShowDeath()
    {
        deathCounterText.text = "Reset : " + death.ToString();
        totalDeathCounterText.text = "Total : " + totalDeath.ToString();
    }

    public void AddDeathCount()
    {
        death++;
        deathCounts[levelIndex] = death;
        totalDeath++;
        tempDeath++;
        GameData.SetDeathCounter(deathCounts);
        GameData.SetIntersititialKey(tempDeath);

    }

  private void CalculateDeath()
    {
        for (int i = 0; i < deathCounts.Length; i++)
        {
            totalDeath += deathCounts[i];
        }

    }











}
