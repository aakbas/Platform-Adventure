using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTeleporter : MonoBehaviour
{
    [SerializeField] int levelToTeleport;
    LevelLoader myLevelLoader;

    private void Start()
    {
        myLevelLoader = FindObjectOfType<LevelLoader>();

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        myLevelLoader.LoadLevel(levelToTeleport);
    }




}
