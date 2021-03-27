using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelTeleporter : MonoBehaviour
{
    //Config Parameters
    [SerializeField] int levelToTeleport;
    [SerializeField] Text selectedLevel;
    [SerializeField] int levelOffset=3;
    [SerializeField] GameObject levelSelectorLayout;
    int levelProgress;
    int currrentLevel;

    //Cached
    LevelLoader myLevelLoader; 
 
    private void Start()
    {
        myLevelLoader = FindObjectOfType<LevelLoader>();        
        levelProgress = GameData.GetLevelProgress();

        //Saving progress with current level index
        if (levelProgress<myLevelLoader.GetSceneIndex())
        {
            GameData.SetLevelProgress(myLevelLoader.GetSceneIndex());
        }
        //Adjust parameters with current scene index offset
        currrentLevel = levelProgress - levelOffset;

        //Level selector text change
        selectedLevel.text =currrentLevel.ToString();
    }
  


    
    public void SelectionButtonUp()
    {
        int temp =int.Parse(selectedLevel.text);
        if (temp+levelOffset<GameData.GetLevelProgress())
        {
            temp++;
            selectedLevel.text = temp.ToString();
        }
    }

    public void SelectionButtonDown()
    {
        int temp = int.Parse(selectedLevel.text);
        if (temp>0)
        {
            temp--;
            selectedLevel.text = temp.ToString();
        }
    }

    public void NextLevelButton()
    {
        myLevelLoader.LoadNextScene();
    }

    public void TeleportToScene()
    {
        int temp = int.Parse(selectedLevel.text);
        myLevelLoader.LoadLevel(temp + levelOffset);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<TravelerMovement>())
        {
            levelSelectorLayout.SetActive(true);
        }
        FindObjectOfType<SpeedRunTimer>().SetBestTimeArray();
    }

    public int GetCurrentLevel()
    {
        return SceneManager.GetActiveScene().buildIndex - levelOffset ;
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<TravelerMovement>())
        {
            levelSelectorLayout.SetActive(false);
        }

    }

}
