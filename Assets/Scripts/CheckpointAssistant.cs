using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointAssistant : MonoBehaviour
{
    int[] tempArray;
    bool status;
    LevelTeleporter myLevel;
    // Start is called before the first frame update
    void Start()
    {
        myLevel = FindObjectOfType<LevelTeleporter>();
        tempArray = GameData.GEtCheckpointProgress();
        if (tempArray[myLevel.GetCurrentLevel()]!=0)
        {
            status = true;
        }
        else
        {
            status=false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!status)
        {
            gameObject.SetActive(false);
        }
    }
}
