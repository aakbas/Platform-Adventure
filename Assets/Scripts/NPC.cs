using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
public class NPC : MonoBehaviour
{
   
    [SerializeField] GameObject dialogueScreen;
    [SerializeField] GameObject dialogueButtonEng;
    [SerializeField] GameObject dialogueButtonTR;

    [SerializeField] public string name;
    [TextArea(3, 10)]
    [SerializeField] public string[] sentencesEng;
    [TextArea(3, 10)]
    [SerializeField] public string[] sentencesTR;

    string[] triggeredDialogue;
    DialogueManager myDialogueManager;

    private void Start()
    {
        myDialogueManager = FindObjectOfType<DialogueManager>();
        if (GameData.GetLanguageKey() == 0)
        {
            triggeredDialogue = sentencesEng;
        }
        else
        {
            triggeredDialogue = sentencesTR;
        }
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<TravelerMovement>())
        {
            if (GameData.GetLanguageKey() == 0)
            {
                dialogueButtonEng.SetActive(true);
            }
            else
            {
                dialogueButtonTR.SetActive(true);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {    


        if (collision.GetComponent<TravelerMovement>())
        {
            if (CrossPlatformInputManager.GetButtonDown("Interact"))
            {
                dialogueScreen.SetActive(true);
                TriggerDialogue();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        dialogueScreen.SetActive(false);
        dialogueButtonEng.SetActive(false);
        dialogueButtonTR.SetActive(false);
    }

    public void TriggerDialogue()
    {
        
        myDialogueManager.StartDialogue(triggeredDialogue,name);
       

    }






}
