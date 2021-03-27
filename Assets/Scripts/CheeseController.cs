using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheeseController : MonoBehaviour
{   
 
    private void Start()
    {


        if (GameData.GetLevelProgress() < 3)
        {
            GameData.SetLevelProgress(3);
        }


    }




}
