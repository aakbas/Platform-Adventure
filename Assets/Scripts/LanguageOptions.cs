using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageOptions : MonoBehaviour
{

    //Config Params
    int languageKey;


    //Language arrays
   [SerializeField]  GameObject []  firstLanguage;
    [SerializeField] GameObject[] secondLanguage;


    private void Start()
    {
        GetLanguage();
        
    }

    public void GetLanguage()
    {
        if (GameData.GetLanguageKey() == 0)
        {
           
            languageKey = GameData.GetLanguageKey();
            AssetChanges();
        }
        else if (GameData.GetLanguageKey()==1)
        {
            languageKey = GameData.GetLanguageKey();
            AssetChanges();
        }
        else
        {
            GameData.SetLanguageKey(0);
            languageKey = GameData.GetLanguageKey();
            AssetChanges();
        }

       
    }



    public void SetLanguage(int key)
    {
        languageKey = key;
        GameData.SetLanguageKey(languageKey);

    }

    public void firstLanguageButton()
    {
        SetLanguage(0);
        AssetChanges();
    }

    public void secondLanguageButton()
    {
        SetLanguage(1);
        AssetChanges();
    }



    public void AssetChanges()
    {

        if (languageKey==0)
        {
            foreach (var k in secondLanguage)
            {
                k.SetActive(false);
            }
            foreach (var k in firstLanguage)
            {
                k.SetActive(true);
            }
        }
        else
        {
            foreach (var k in firstLanguage)
            {
                k.SetActive(false);
            }
            foreach (var k in secondLanguage)
            {
                k.SetActive(true);
            }
        }

       }


    }















