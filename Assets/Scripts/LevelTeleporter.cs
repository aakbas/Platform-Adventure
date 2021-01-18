using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelTeleporter : MonoBehaviour
{
    [SerializeField] int levelToTeleport;
    [SerializeField] Text selectedLevel;
    [SerializeField] int levelOffset=3;
    [SerializeField] GameObject levelSelectorLayout;
    LevelLoader myLevelLoader; 
    int levelProgress;
    int currrentLevel;

    private void Start()
    {
        myLevelLoader = FindObjectOfType<LevelLoader>();        
        levelProgress = GameData.GetLevelProgress();
        if (levelProgress<myLevelLoader.GetSceneIndex())
        {
            GameData.SetLevelProgress(myLevelLoader.GetSceneIndex());
        }
        currrentLevel = levelProgress - levelOffset;
        selectedLevel.text =currrentLevel.ToString();
    }
  




    public void SelectionButtonUp()
    {
        int temp =int.Parse(selectedLevel.text);
        if (temp+levelOffset<=myLevelLoader.GetSceneIndex())
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
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<TravelerMovement>())
        {
            levelSelectorLayout.SetActive(false);
        }

    }

}
