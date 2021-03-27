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

   [SerializeField] int levelIndex;
    private void Start()
    {
        deathCounts=GameData.GetDeathCounter();
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
        GameData.SetDeathCounter(deathCounts);

    }

  private void CalculateDeath()
    {
        for (int i = 0; i < deathCounts.Length; i++)
        {
            totalDeath += deathCounts[i];
        }

    }











}
