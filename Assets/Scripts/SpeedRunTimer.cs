using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedRunTimer : MonoBehaviour
{
    [SerializeField]  float timer;
    [SerializeField] float[] bestTimes;
    [SerializeField]  Text timerText;
    [SerializeField] Text bestTimeText ;
    float bestTimeTemp;
    LevelTeleporter myLevel;
    int levelIndex;

    private void Start()
    {
        bestTimes = GameData.GetTimerArray();
        myLevel = FindObjectOfType<LevelTeleporter>();
        levelIndex = myLevel.GetCurrentLevel();
    }

    private void Update()
    {
        timer += 1 * Time.deltaTime;

        string seconds = (timer % 60).ToString("00");
        string minutes = Mathf.Floor((timer % 3600) / 60).ToString("00");
        timerText.text = "TIME : " + minutes + " : " + seconds;
        ShowBestTime();

    }

    public void SetBestTimeArray()
    {
        if (bestTimes[levelIndex]==0)
        {
            bestTimes[levelIndex] = timer;
        }
        else
        {
            if (bestTimes[levelIndex]>timer)
            {
                bestTimes[levelIndex] = timer;
            }
        }

        GameData.SetTimerData(bestTimes);        

    }


    private void ShowBestTime()
    {
        bestTimeTemp = bestTimes[levelIndex];
        string seconds = (bestTimeTemp % 60).ToString("00");
        string minutes = Mathf.Floor((bestTimeTemp % 3600) / 60).ToString("00");
        bestTimeText.text = "BEST :  " + minutes + " : " + seconds;
    }







}
