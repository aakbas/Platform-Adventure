using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    AudioSource audioSource;


    //Singleton
    void Awake()
    {
        SetUpSingleton();
    }
    private void SetUpSingleton()
    {
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {        
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = GameData.GetMusicVolume();
        
    }    
    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
    }
}
