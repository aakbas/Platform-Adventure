using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{
    bool isPaused = false;
    [SerializeField] GameObject pauseMenu;

    // Update is called once per frame
    void Update()
    {
        if (!isPaused)
        {
            if (CrossPlatformInputManager.GetButtonDown("Pause"))
            {
                isPaused = togglePause();
                pauseMenu.SetActive(true);
            }
        }
        else
        {

            if (CrossPlatformInputManager.GetButtonDown("Pause"))
            {
                isPaused = togglePause();
                pauseMenu.SetActive(false);
            }
        }
    }


    bool togglePause()
    {
        if (Time.timeScale == 0f)
        {
            Time.timeScale = 1f;
            return (false);
        }
        else
        {
            Time.timeScale = 0f;
            return (true);
        }
    }
    public void Continue()
    {
        isPaused = togglePause();
        pauseMenu.SetActive(false);
    }



}
