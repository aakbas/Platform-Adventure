using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
 
    int currentSceneIndex;

    // Start is called before the first frame update
    void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;        
    }

    // Loads next scene on ındex
    public void LoadNextScene()
    {
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    // Load Game over Scene

    public void LoadGameOverScene()
    {
        SceneManager.LoadScene("GameOver");
    }

    // Quit The Game

    public void QuitGame()
    {
        Application.Quit();
    }

    // Load Start Menu
    public void LoadStartMenu()
    {
        SceneManager.LoadScene("StartMenu");
        Time.timeScale = 1f;
    }

    //Load Options Menu 
    public void LoadOptionsMenu()
    {
        SceneManager.LoadScene("Options");  
    }

    //Load Level by level index
    public void LoadLevel(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex);
    }

    //Load New Game
    public void LoadNewGame()
    {
        GameData.SetNewGameData();
        SceneManager.LoadScene("Hub");
    }
 
    // Continue 

    public void LoadContinue()
    {
        SceneManager.LoadScene("Hub");
    }

    //Gives the scene index
    public int GetSceneIndex()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }

    //Restart Scene

    public void RestartScene()
    {
        SceneManager.LoadScene(currentSceneIndex);
    }
}
