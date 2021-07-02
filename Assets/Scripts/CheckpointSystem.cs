using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointSystem : MonoBehaviour
{

    [SerializeField] GameObject checkpointOpen;
    [SerializeField] GameObject checkpointClose;
    [SerializeField] GameObject checkpointLocation;
    [SerializeField] GameObject gameObjectToTeleport;

    bool checkpointStatus;

    int[] tempArray;

    int currentLevel;

    LevelTeleporter myLevel;

    AddManager myAds;

    // Start is called before the first frame update
    void Start()
    {
        myLevel = FindObjectOfType<LevelTeleporter>();
        currentLevel = myLevel.GetCurrentLevel();
        tempArray = GameData.GEtCheckpointProgress();
        myAds = FindObjectOfType<AddManager>();

        if (tempArray[currentLevel]!=0)
        {
            checkpointStatus = true;
            OpenCheckpoint();
            
        }
        else
        {
            CloseCheckpoint();
            checkpointStatus = false;
        }

        Debug.Log(checkpointStatus);
        Debug.Log(tempArray[currentLevel]);
    }

    private void OpenCheckpoint()
    {
        checkpointOpen.SetActive(true);
        checkpointClose.SetActive(false);
    }

    private void CloseCheckpoint()
    {
        checkpointOpen.SetActive(false);
        checkpointClose.SetActive(true);
    }


    private void UpdateCheckpoint()
    {

        tempArray[currentLevel] = 1;
        GameData.SetChecpointProgress(tempArray);
        checkpointStatus = true;
        OpenCheckpoint();
         
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<TravelerMovement>())
        {
            UpdateCheckpoint();
        }
    }


    public void TeleportObject()
    {
        if (checkpointStatus)
        {

            Debug.Log("TEst");          
            gameObjectToTeleport.transform.position = new Vector3(checkpointLocation.transform.position.x, checkpointLocation.transform.position.y, checkpointLocation.transform.position.z);


        }
    }

    public bool GetChecpointStatus()
    {
        return checkpointStatus;
    }




}
